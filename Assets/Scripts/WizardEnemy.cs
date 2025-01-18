using Plataformas2DU.StatePattern;
using UnityEngine;

namespace Plataformas2DU.Enemies
{
    public class WizardEnemy : EnemyBase
    {
        private Animator animator;

        private IdleState idleState;

        private ThrowFireballState throwFireballState;

        private bool playerDetected = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            idleState = GetComponent<IdleState>();
            throwFireballState = GetComponent<ThrowFireballState>();
        }

        protected override void Start()
        {
            base.Start();

            throwFireballState.Animator = animator;

            idleState.AddTransition(new StateBase.StateTransition(throwFireballState, () => playerDetected));
            throwFireballState.AddTransition(new StateBase.StateTransition(idleState, () => !playerDetected));

            stateMachine.ChangeToState(idleState);
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
