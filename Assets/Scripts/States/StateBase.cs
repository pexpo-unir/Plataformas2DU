using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plataformas2DU.StatePattern
{
    public abstract class StateBase : MonoBehaviour
    {
        public class StateTransition
        {
            public StateBase NextState { get; }
            public Func<bool> Condition { get; }

            public StateTransition(StateBase nextState, Func<bool> condition)
            {
                NextState = nextState;
                Condition = condition;
            }
        }

        private readonly List<StateTransition> stateTransitions = new();

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public void AddTransition(StateTransition transition)
        {
            stateTransitions.Add(transition);
        }

        public StateBase CheckTransitions()
        {
            foreach (var transition in stateTransitions)
            {
                if (transition.Condition())
                {
                    return transition.NextState;
                }
            }

            return null;
        }
    }
}
