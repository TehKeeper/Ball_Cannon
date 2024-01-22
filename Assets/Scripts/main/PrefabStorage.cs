using System.Collections.Generic;
using System.Linq;
using logic.world.ball;
using UnityEngine;

namespace main
{
    public class PrefabStorage
    {
        public Dictionary<Transform, SoccerBall> _ballsCollision = new();

        public Queue<SoccerBall> _ballsQueue = new();
        private SoccerBall _prefab;
        private Transform _ballStorage;

        public PrefabStorage(Transform transform, SoccerBall prefab)
        {
            _ballStorage = transform;
            _prefab = prefab;
        }

        public SoccerBall TryDequeueBall()
        {
            if (_ballsQueue.Count == 0)
            {
                
                var soccerBall = Object.Instantiate(_prefab);
                _ballsCollision[soccerBall.transform] = soccerBall;
                return soccerBall;
            }

            return _ballsQueue.Dequeue();
        }
        
        public void EnqueueBall(SoccerBall ball)
        {
            ball.Activate(false);
            ball.SetPos(_ballStorage.position);
            _ballsQueue.Enqueue(ball);
        }

        public bool HasCollision(Transform collision) => _ballsCollision.Keys.Contains(collision);

        public void DespawnCollision(Transform collision) => EnqueueBall(_ballsCollision[collision]);
    }
}