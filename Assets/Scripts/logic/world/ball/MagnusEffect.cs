using UnityEngine;

namespace logic.world.ball
{
    
    public class MagnusEffect 
    {
        private float _radius;
        private float _airDensity;
        
        public MagnusEffect(float radius, float airDensity)
        {
            _radius = radius;
            _airDensity = airDensity;
        }

        public void ApplyEffect(Rigidbody rb)
        {
            var direction = Vector3.Cross(rb.angularVelocity, rb.velocity);
            var magnitude = 4 / 3f * Mathf.PI * _airDensity * Mathf.Pow(_radius, 3);
            rb.AddForce(magnitude * direction);
        }
    }
}