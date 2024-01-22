using logic.behaviour.shoot;
using UnityEngine;

namespace logic.behaviour
{
    public class LinearPower : ShootBehaviourBase
    {
        private readonly float _strenght;

        public LinearPower(float strength)
        {
            _strenght = strength;
        }

        protected override float Power() => Mathf.Clamp01((CurrTime - sTime) * _strenght);

        protected override void UpdPwr()
        {
        }
    }
}