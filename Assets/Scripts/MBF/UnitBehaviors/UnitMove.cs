using UnityEngine;

namespace MBF.UnitBehaviors
{
    public class UnitMove : MonoBehaviour
    {
        private Vector3 _velocity = Vector3.zero;

        public void MoveBy(Vector3 velocity)
        {
            _velocity = velocity;
        }

        public void SetPos(Vector3 pos)
        {
            transform.position = pos;
            _velocity = Vector3.zero;
        }
        
        private void FixedUpdate()
        {
            transform.position += _velocity;
            _velocity = Vector3.zero;
        }
    }
}