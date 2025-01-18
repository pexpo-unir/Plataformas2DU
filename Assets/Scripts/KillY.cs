using Plataformas2DU.DamageSystem;
using UnityEngine;

namespace Plataformas2DU.Gameplay
{
    public class KillY : MonoBehaviour
    {
        /// <summary>
        /// Damage dealt to an leaving entity.
        /// </summary>
        private int DamageAmount = 10000;

        void OnTriggerEnter2D(Collider2D other)
        {
            var otherDamageComponent = other.gameObject.GetComponent<DamageComponent>();
            if (otherDamageComponent)
            {
                otherDamageComponent.HasImmunity = false;
                otherDamageComponent.GetDamage(DamageAmount);
                return;
            }

            Destroy(other.gameObject);
        }
    }
}
