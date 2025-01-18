using UnityEngine;

namespace Plataformas2DU.StatePattern
{
    public class StateMachine : MonoBehaviour
    {
        private StateBase currentState;

        private bool paused = false;

        private void Update()
        {
            if (paused) return;

            if (currentState == null)
            {
                return;
            }

            currentState.UpdateState();

            var nextState = currentState.CheckTransitions();
            if (nextState != null)
            {
                ChangeToState(nextState);
            }
        }

        public void StartLogic() => paused = false;

        public void StopLogic() => paused = true;

        public void ChangeToState(StateBase nextState)
        {
            currentState?.ExitState();

            currentState = nextState;
            currentState?.EnterState();
        }

    }
}
