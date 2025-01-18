using Plataformas2DU.DamageSystem;
using UnityEngine;

namespace Plataformas2DU.Gameplay
{
    public class Fireball : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField]
        private float initialImpulse = 10f;

        [field: SerializeField]
        public int DamageAmount { get; private set; }

        [SerializeField]
        private float lifeTime = 10f;

        void Awake()
        {
            Destroy(gameObject, lifeTime);
        }

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * initialImpulse, ForceMode2D.Impulse);

            var hitBox = GetComponentInChildren<HitBox>();
            if (hitBox)
            {
                hitBox.OnObjectHit += HandleObjectHit;
            }
        }

        private void HandleObjectHit(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }

            var otherDamageComponent = other.gameObject.GetComponent<DamageComponent>();
            if (otherDamageComponent)
            {
                otherDamageComponent.GetDamage(DamageAmount);
            }

            Destroy(gameObject);
        }
    }
}
