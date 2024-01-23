namespace logic.behaviour.shoot
{
    public interface IShootBehaviour
    {
        public float GetPower();

        public float SetTime();

        public void UpdatePower();
        void Reset();
    }
}