namespace logic.world.ball
{
    public interface IBallDespawnLogic
    {
        void TryTouch(float t);
        void AddTime(float t);
        void Reset();
        void Update();
    }
}