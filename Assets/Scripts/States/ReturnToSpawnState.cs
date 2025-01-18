using UnityEngine;

namespace Plataformas2DU.StatePattern
{
    public class ReturnToSpawnState : StateBase
    {
        public Vector3 SpawnPoint { get; set; }

        [field: SerializeField]
        public float ReturnSpeed { get; set; } = 5f;

        /// <summary>
        /// Minimum distance to consider whether it has reached the spawn point.
        /// </summary>
        [field: SerializeField]
        public float SpawnDistanceThreshold { get; set; } = 0.5f;

        private Vector3 rightDirectionRotation = Vector3.zero;
        private Vector3 leftDirectionRotation = new(0, 180f, 0);

        public override void EnterState() { }

        public override void UpdateState()
        {
            if (Vector3.Distance(transform.position, SpawnPoint) >= SpawnDistanceThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, SpawnPoint, ReturnSpeed * Time.deltaTime);
                ChangeRotation();
            }
        }

        public override void ExitState() { }

        private void ChangeRotation()
        {
            if (transform.position.x < SpawnPoint.x)
            {
                transform.eulerAngles = rightDirectionRotation;
                return;
            }

            transform.eulerAngles = leftDirectionRotation;
        }
    }
}
