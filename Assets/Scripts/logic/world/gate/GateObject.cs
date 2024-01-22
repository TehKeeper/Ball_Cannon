using System;
using logic.behaviour.gate;
using UnityEngine;
using utilities.tools.mono;

namespace logic.world.gate
{
    [RequireComponent(typeof(BoxCollider))]
    public class GateObject : MonoBehaviourObj
    {
        private Action<Transform> _onCollision;
        [SerializeField] private float speed=1;
        [SerializeField] private Vector3 sPos;
        [SerializeField] private Vector3 ePos;
        private IGateBehaviour _behaviour;

        protected override void MakeInit()
        {
            _behaviour = new GateBounceLoop();
        }

        public void SetUp(Action<Transform> onCollision)
        {
            _onCollision = onCollision;
        }

        private void FixedUpdate()
        {
            tf.position = _behaviour.AnimatePos(sPos, ePos,speed);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _onCollision?.Invoke(collision.transform);
        }

        [ContextMenu("Set Start Pos")]
        public void SetStartPos() => sPos = transform.position;
        [ContextMenu("Set End Pos")]
        public void SetEndPos() => ePos = transform.position;
    }
}