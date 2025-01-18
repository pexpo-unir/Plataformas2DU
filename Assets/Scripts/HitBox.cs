using System;
using UnityEngine;

namespace Plataformas2DU.Gameplay
{
    public class HitBox : MonoBehaviour
    {
        public event Action<Collider2D> OnObjectHit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnObjectHit?.Invoke(other);
        }
    }
}
