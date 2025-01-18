using System.Collections;
using Plataformas2DU.Gameplay;
using UnityEngine;

namespace Plataformas2DU.StatePattern
{
    public class ThrowFireballState : StateBase
    {
        public Animator Animator { get; set; }

        [SerializeField]
        private float timeBetweenAttack = 2f;

        [SerializeField]
        private Transform fireballSpawnPoint;

        [SerializeField]
        private Fireball fireballPrefab;

        private Coroutine throwFireballCoroutine;

        public override void EnterState()
        {
            throwFireballCoroutine = StartCoroutine(ThrowFireballRoutine());
        }

        public override void UpdateState() { }

        public override void ExitState()
        {
            if (throwFireballCoroutine != null)
            {
                StopCoroutine(throwFireballCoroutine);
            }
        }

        private IEnumerator ThrowFireballRoutine()
        {
            while (true)
            {
                InitiateAttack();
                yield return new WaitForSeconds(timeBetweenAttack);
            }
        }

        private void InitiateAttack()
        {
            Animator.SetTrigger("atacar");
        }

        private void PerformAttack()
        {
            Instantiate(fireballPrefab, fireballSpawnPoint.position, transform.rotation);
        }
    }
}
