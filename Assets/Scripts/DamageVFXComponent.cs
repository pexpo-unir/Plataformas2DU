using System.Collections;
using UnityEngine;

namespace Plataformas2DU.DamageSystem
{
    public class DamageVFXComponent : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Color noEffectColor = Color.white;

        [SerializeField]
        private Color damageEffectColor = Color.red;

        [SerializeField]
        private Color healEffectColor = Color.green;

        [SerializeField]
        private float colorEffectDuration = 0.333f;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void GetDamageVFX()
        {
            StartCoroutine(HealthChangeVFX(damageEffectColor));
        }

        public void GetHealingVFX()
        {
            StartCoroutine(HealthChangeVFX(healEffectColor));
        }

        private IEnumerator HealthChangeVFX(Color colorChange)
        {
            spriteRenderer.color = colorChange;

            yield return new WaitForSeconds(colorEffectDuration);

            spriteRenderer.color = noEffectColor;
        }

        public void DeathVFX()
        {
            // TODO: Implement effect.
        }
    }
}
