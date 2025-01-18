using UnityEngine;

namespace Plataformas2DU.StatePattern
{
    public class ChaseState : StateBase
    {
        public GameObject Target { get; set; }

        [field: SerializeField]
        public float ChaseSpeed { get; set; } = 5f;

        [field: SerializeField]
        public float ChaseMaximumDistance { get; set; } = 5f;

        /// <summary>
        /// Minimum distance to consider whether it has reached its target.
        /// </summary>
        [field: SerializeField]
        public float TargetDistanceThreshold { get; set; } = 0.5f;

        private Vector3 rightDirectionRotation = Vector3.zero;
        private Vector3 leftDirectionRotation = new(0, 180f, 0);

        public override void EnterState() { }

        public override void UpdateState()
        {
            Chase();
        }

        public override void ExitState() { }

        private void Chase()
        {
            if (Target == null)
            {
                return;
            }

            if (Vector3.Distance(transform.position, Target.transform.position) >= TargetDistanceThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, ChaseSpeed * Time.deltaTime);
                ChangeRotation();
            }
        }

        private void ChangeRotation()
        {
            if (transform.position.x < Target.transform.position.x)
            {
                transform.eulerAngles = rightDirectionRotation;
                return;
            }

            transform.eulerAngles = leftDirectionRotation;
        }
    }
}
