using System.Collections;
using Plataformas2DU.Enemies;
using UnityEngine;

namespace Plataformas2DU.StatePattern
{
    public class PatrolState : StateBase
    {
        [SerializeField]
        private PatrolPath patrolPath;

        private int currentPathPoint = 0;

        /// <summary>
        /// Minimum distance to consider whether it has reached its destination.
        /// </summary>
        [SerializeField]
        private float patrolPointDistanceThreshold = 0.5f;

        [SerializeField]
        private float patrolSpeed = 5f;

        [SerializeField]
        private float waitTimeAtPoint = 0;

        [SerializeField]
        private bool faceToPathPointDirection = true;

        private bool stopPath = false;

        private Vector3 rightDirectionRotation = Vector3.zero;
        private Vector3 leftDirectionRotation = new(0, 180f, 0);

        private Coroutine waitAtPointCoroutine;

        public override void EnterState() { }

        public override void UpdateState()
        {
            if (stopPath)
            {
                return;
            }

            PatrolMovement();
        }

        public override void ExitState()
        {
            if (waitAtPointCoroutine != null)
            {
                StopCoroutine(waitAtPointCoroutine);
            }
        }

        private void PatrolMovement()
        {
            if (patrolPath == null)
            {
                return;
            }

            if (Vector3.Distance(transform.position, patrolPath[currentPathPoint].position) >= patrolPointDistanceThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, patrolPath[currentPathPoint].position, patrolSpeed * Time.deltaTime);
            }
            else
            {
                waitAtPointCoroutine = StartCoroutine(WaitAtPoint());
            }
        }

        private IEnumerator WaitAtPoint()
        {
            stopPath = true;

            yield return new WaitForSeconds(waitTimeAtPoint);

            stopPath = false;
            SetNextPointPath();
        }

        private void SetNextPointPath()
        {
            currentPathPoint = (currentPathPoint + 1) % patrolPath.Length;
            if (faceToPathPointDirection)
            {
                ChangeRotation();
            }
        }

        private void ChangeRotation()
        {
            if (transform.position.x < patrolPath[currentPathPoint].position.x)
            {
                transform.eulerAngles = rightDirectionRotation;
                return;
            }

            transform.eulerAngles = leftDirectionRotation;
        }
    }
}
