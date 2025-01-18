using UnityEngine;

namespace Plataformas2DU.StatePattern
{
    public class WhirlwindState : StateBase
    {
        public Animator Animator { get; set; }

        public override void EnterState()
        {
            Animator.SetBool("atacando", true);
        }

        public override void UpdateState() { }

        public override void ExitState()
        {
            Animator.SetBool("atacando", false);
        }
    }
}
