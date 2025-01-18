using Plataformas2DU.StatePattern;
using UnityEngine;

namespace Plataformas2DU.Gameplay
{
    public class MovingPlatform : MonoBehaviour
    {
        private StateMachine stateMachine;

        private PatrolState patrolState;

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            patrolState = GetComponent<PatrolState>();
        }

        private void Start()
        {
            stateMachine.ChangeToState(patrolState);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }

            other.transform.SetParent(transform);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }

            other.transform.SetParent(null);
        }
    }
}
