using Plataformas2DU.StatePattern;
using UnityEngine;

namespace Plataformas2DU.Enemies
{
    public class EyeBatEnemy : EnemyBase
    {
        private PatrolState patrolState;

        private ChaseState chaseState;

        private ReturnToSpawnState returnToSpawnState;

        private Vector3 spawnPoint;

        private void Awake()
        {
            patrolState = GetComponent<PatrolState>();
            chaseState = GetComponent<ChaseState>();
            returnToSpawnState = GetComponent<ReturnToSpawnState>();
        }

        protected override void Start()
        {
            base.Start();

            spawnPoint = gameObject.transform.position;

            returnToSpawnState.SpawnPoint = spawnPoint;

            patrolState.AddTransition(new StateBase.StateTransition(chaseState, () => chaseState.Target != null));

            chaseState.AddTransition(new StateBase.StateTransition(returnToSpawnState, () => chaseState.Target == null));
            chaseState.AddTransition(new StateBase.StateTransition(returnToSpawnState, () => Vector3.Distance(transform.position, spawnPoint) >= chaseState.ChaseMaximumDistance));

            returnToSpawnState.AddTransition(new StateBase.StateTransition(patrolState, () => Vector3.Distance(transform.position, spawnPoint) <= returnToSpawnState.SpawnDistanceThreshold));

            stateMachine.ChangeToState(patrolState);
        }

        protected override void HandleObjectDetected(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }

            chaseState.Target = other.gameObject;
        }

        protected override void HandleObjectDetectedEnds(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                return;
            }

            chaseState.Target = null;
        }
    }
}
