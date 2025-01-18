using Plataformas2DU.StatePattern;
using UnityEngine;

namespace Plataformas2DU.Enemies
{
    public class SlimeEnemy : EnemyBase
    {
        private Animator animator;

        private PatrolState patrolState;

        private WhirlwindState whirlwindState;

        private bool playerDetected = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            patrolState = GetComponent<PatrolState>();
            whirlwindState = GetComponent<WhirlwindState>();
        }

        protected override void Start()
        {
            base.Start();

            whirlwindState.Animator = animator;

            patrolState.AddTransition(new StateBase.StateTransition(whirlwindState, () => playerDetected));
            whirlwindState.AddTransition(new StateBase.StateTransition(patrolState, () => !playerDetected));

            stateMachine.ChangeToState(patrolState);
        }

        protected override void HandleObjectDetected(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }

            playerDetected = true;
        }

        protected override void HandleObjectDetectedEnds(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }

            playerDetected = false;
        }
    }
}
