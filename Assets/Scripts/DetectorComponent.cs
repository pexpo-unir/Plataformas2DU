using System;
using UnityEngine;

namespace Plataformas2DU.Gameplay
{
    public class DetectorComponent : MonoBehaviour
    {
        public event Action<Collider2D> OnObjectDetected;

        public event Action<Collider2D> OnObjectDetectedEnds;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnObjectDetected?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnObjectDetectedEnds?.Invoke(other);
        }
    }
}
