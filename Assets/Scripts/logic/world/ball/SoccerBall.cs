using System;
using Unity.VisualScripting;
using UnityEngine;
using utilities.tools.mono;

namespace logic.world.ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class SoccerBall : MonoBehaviourObj
    {
        [SerializeField] private float radius = 0.5f;
        [SerializeField] private float airDensity = 0.12f;
        [SerializeField] private bool applyEffect = true;
        private Rigidbody _rb;

        private bool _isActive;

        private MagnusEffect _effect;

        private GameObject _go;

        //private bool _touched;
        //private float _despawnTimer;
        private Action<SoccerBall> _returnToPool;
        private IBallDespawnLogic _despawnLogic;

        protected override void MakeInit()
        {
            _go = gameObject;
            _rb = GetComponent<Rigidbody>();
            _effect = new MagnusEffect(radius, airDensity);
            _isActive = true;
            _despawnLogic = new BallDespawnLogic(this, _returnToPool, _rb);
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                if (applyEffect)
                {
                    _effect.ApplyEffect(_rb);
                }

                _despawnLogic.Update();
            }
        }

        public void Activate(bool b)
        {
            _rb.isKinematic = !b;
            _isActive = b;
            _go.SetActive(b);

            _despawnLogic.Reset();
        }

        public void OnDespawn(Action<SoccerBall> handler) => _returnToPool = handler;

        public void Shoot(float cannonForce, Vector3 direction)
        {
            _rb.AddForce(direction * cannonForce);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("[BC] Ball Collision Enter");
            if (_isActive)
                return;

            _despawnLogic.TryTouch(3);
        }


        public void SetPos(Vector3 launchPointPosition) => tf.position = launchPointPosition;
    }
}