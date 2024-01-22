using UnityEngine;

namespace logic.behaviour.gate
{
    public interface IGateBehaviour
    {
        public Vector3 AnimatePos(Vector3 sPos, Vector3 ePos, float speed = 1);
    }

    public class GateBounceLoop : IGateBehaviour
    {
        private bool _reverse;

        public Vector3 AnimatePos(Vector3 sPos, Vector3 ePos, float speed = 1)
        {
            return Vector3.Lerp(sPos, ePos,Mathf.Abs(Mathf.Sin(Time.time * speed)));
        }
    }
}