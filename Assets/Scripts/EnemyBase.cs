using Plataformas2DU.DamageSystem;
using Plataformas2DU.StatePattern;
using Plataformas2DU.Gameplay;
using UnityEngine;

namespace Plataformas2DU.Enemies
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(DamageComponent), typeof(DamageVFXComponent))]
    public abstract class EnemyBase : MonoBehaviour
    {
        protected StateMachine stateMachine;

        public DamageComponent DamageComponent { get; private set; }

        public DamageVFXComponent DamageVFXComponent { get; private set; }

        [field: SerializeField]
        public int DamageAmount { get; private set; }

        protected virtual void Start()
        {
            stateMachine = GetComponent<StateMachine>();
            DamageComponent = GetComponent<DamageComponent>();
            DamageVFXComponent = GetComponent<DamageVFXComponent>();

            if (DamageComponent)
            {
                DamageComponent.OnHealthChanged += HandleHealthChange;
                DamageComponent.OnDeath += HandleDeath;
            }

            var hitBox = GetComponentInChildren<HitBox>();
            if (hitBox)
            {
                hitBox.OnObjectHit += HandleObjectHit;
            }

            var detector = GetComponentInChildren<DetectorComponent>();
            if (detector)
            {
                detector.OnObjectDetected += HandleObjectDetected;
                detector.OnObjectDetectedEnds += HandleObjectDetectedEnds;
            }
        }

        protected virtual void Update() { }

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
        }

        protected virtual void HandleObjectDetected(Collider2D other) { }

        protected virtual void HandleObjectDetectedEnds(Collider2D other) { }

        private void HandleHealthChange(int oldValue, int newValue)
        {
            if (oldValue > newValue)
            {
                DamageVFXComponent.GetDamageVFX();
                return;
            }

            DamageVFXComponent.GetHealingVFX();
        }

        private void HandleDeath()
        {
            DamageVFXComponent.DeathVFX();

            Destroy(gameObject);
        }
    }
}
