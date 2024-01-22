using UnityEngine;

namespace logic.behaviour.shoot
{
    public abstract class ShootBehaviourBase : IShootBehaviour
    {
        protected float sTime;
        private bool _set = false;
        protected float CurrTime => Time.time;
        public float GetPower() => _set ? Power() : 0;
        protected abstract float Power();

        public float SetTime()
        {
            _set = true;
            return sTime = Time.time;
        }

        public void UpdatePower() => UpdPwr();

        public void Reset()
        {
            _set = false;
            sTime = 0;
        }

        protected abstract void UpdPwr();
    }
}