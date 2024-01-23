using System;

namespace logic.world.ball.despawn
{
    public interface IBallDespawnLogic
    {
        void TryTouch(float t);
        void AddTime(float t);
        void Reset();
        void Update();
        void SetHandler(Action<SoccerBall> handler);
    }
}