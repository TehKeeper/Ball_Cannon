using System;
using UnityEngine;
using utilities;

namespace logic.world.ball
{
    public class BallDespawnLogic : IBallDespawnLogic
    {
        private bool _touched;
        private readonly SoccerBall _ball;
        private readonly Action<SoccerBall> _despawnHandler;
        private float _despawnTimer;
        private readonly Rigidbody _rb;

        public BallDespawnLogic(SoccerBall ball, Action<SoccerBall> despawnHandler, Rigidbody rb)
        {
            _ball = ball;
            _despawnHandler = despawnHandler;
            _rb = rb;
        }

        public void TryTouch(float t)
        {
            if(_touched)
                return;
            
            _touched = true;
            AddTime(3);
        }

        public void AddTime(float t) => _despawnTimer = Time.time + t;

        public void Reset()
        {
            _despawnTimer = 0;
            _touched = false;
        }

        public void Update()
        {
            if (_touched && _despawnTimer < Time.time && !_despawnTimer.Approx(0))
            {
                Debug.Log($"[BC] Despawn Timer check, despawn time: {_despawnTimer}, Time: {Time.time}");
                if (_rb.velocity.sqrMagnitude < 4f)
                    _despawnHandler?.Invoke(_ball);
                else
                {
                    AddTime(1);
                }
            }
        }
    }
}