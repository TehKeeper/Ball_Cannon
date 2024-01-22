using System;
using UnityEngine;
using utilities;
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
        private bool _touch;
        private float _despawnTimer;
        private Action<SoccerBall> _returnToPool;

        protected override void MakeInit()
        {
            _go = gameObject;
            _rb = GetComponent<Rigidbody>();
            _effect = new MagnusEffect(radius, airDensity);
            _isActive = true;
        }

        private void FixedUpdate()
        {
            if (_isActive)
            {
                if (applyEffect)
                {
                    _effect.ApplyEffect(_rb);
                }

                if (_touch && _despawnTimer < Time.time && !_despawnTimer.Approx(0))
                {
                    Debug.Log($"[BC] Despawn Timer check, despawn time: {_despawnTimer}, Time: {Time.time}");
                    if (_rb.velocity.sqrMagnitude < 4f)
                        _returnToPool?.Invoke(this);
                    else
                    {
                        _despawnTimer = Time.time + 1;
                    }
                }
            }
        }

        public void Activate(bool b)
        {
            _rb.isKinematic = !b;
            _isActive = b;
            _go.SetActive(b);
            _touch = false;
            _despawnTimer = 0;
        }

        public void OnDespawn(Action<SoccerBall> handler) => _returnToPool = handler;

        public void Shoot(float cannonForce, Vector3 direction)
        {
            _rb.AddForce(direction * cannonForce);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("[BC] Ball Collision Enter");
            if (_isActive && _touch)
                return;

            _touch = true;
            _despawnTimer = Time.time + 3;
        }


        public void SetPos(Vector3 launchPointPosition) => tf.position = launchPointPosition;
    }
}