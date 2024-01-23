using System;
using UnityEngine;
using utilities;

namespace logic.world.ball.despawn
{
    public class BallDespawnLogic : IBallDespawnLogic
    {
        private bool _touched;
        private readonly SoccerBall _ball;
        private Action<SoccerBall> _despawnHandler;
        private float _despawnTimer;
        private readonly Rigidbody _rb;

        public BallDespawnLogic(SoccerBall ball, Rigidbody rb)
        {
            _ball = ball;
            _rb = rb;
        }

        public void TryTouch(float t)
        {
            if (_touched)
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
                /*Debug.Log(
                    $"[BC] Despawn Timer check, despawn time: {_despawnTimer}, Time: {Time.time}, velocity: {_rb.velocity.sqrMagnitude}");*/
                if (_rb.velocity.sqrMagnitude < 4f)
                    _despawnHandler?.Invoke(_ball);
                else
                {
                    AddTime(1);
                }
            }
        }

        public void SetHandler(Action<SoccerBall> handler) => _despawnHandler = handler;
    }
}