using UnityEngine;

namespace MBF
{
    public class UnitRemover : MonoBehaviour
    {
        private float _duration = 1f;

        public void SetDuration(float duration)
        {
            _duration = duration;
        }

        private void Update()
        {
            _duration -= Time.deltaTime;
            if (_duration <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}