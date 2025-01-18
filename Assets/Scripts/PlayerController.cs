using System.Collections;
using Plataformas2DU.DamageSystem;
using Plataformas2DU.GameCore;
using Plataformas2DU.Settings;
using Plataformas2DU.UI;
using Unity.Cinemachine;
using UnityEngine;

namespace Plataformas2DU.Player
{
    [RequireComponent(typeof(DamageComponent), typeof(DamageVFXComponent))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D rb;

        private float inputDirectionH = 0;

        [SerializeField]
        private float movementSpeed = 5f;

        [SerializeField]
        private float jumpSpeed = 25f;

        [SerializeField]
        private float jumpthreshold = 0.15f;

        [SerializeField]
        private Transform jumpOrigin;

        [SerializeField]
        private LayerMask jumpLayerMask;

        private Animator animator;

        private Vector3 rightDirectionRotation = Vector3.zero;
        private Vector3 leftDirectionRotation = new(0, 180f, 0);

        public DamageComponent DamageComponent { get; private set; }

        public DamageVFXComponent DamageVFXComponent { get; private set; }

        [Header("Combat System")]
        [field: SerializeField]
        public int DamageAmount { get; private set; }

        [SerializeField]
        private Transform attackOrigin;

        [SerializeField]
        private float attackRadius;

        [SerializeField]
        private LayerMask attackLayerMask;

        [SerializeField]
        private float attackSlowMotionScale;

        [SerializeField]
        private float attackSlowMotionDuration;

        [SerializeField]
        private GameObject attackHitParticlesPrefab;

        [SerializeField]
        private AudioSource attackAudioSource;

        [SerializeField]
        private AudioClip[] attackAudioClips;


        [SerializeField]
        private HUD hud;

        [SerializeField]
        private PauseMenu pauseMenu;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            DamageComponent = GetComponent<DamageComponent>();
            DamageVFXComponent = GetComponent<DamageVFXComponent>();
        }

        void Start()
        {
            if (DamageComponent)
            {
                DamageComponent.OnHealthChanged += HandleHealthChange;
                DamageComponent.OnDeath += HandleDeath;
            }

            pauseMenu.TogglePauseMenu();

            // TODO: Create and move to PlayerCheats?
            DamageComponent.HasImmunity = SettingsController.PlayerHasImmunity;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenu.TogglePauseMenu();
            }

            if (GameManager.Instance.IsGameplayPaused) return;

            inputDirectionH = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(inputDirectionH * movementSpeed, rb.linearVelocityY);

            if (inputDirectionH != 0)
            {
                ChangeRotation();
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                rb.AddForceY(jumpSpeed, ForceMode2D.Impulse);
                animator.SetTrigger("jump");
            }

            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("attack");
            }
        }

        private void ChangeRotation()
        {
            if (inputDirectionH > 0)
            {
                transform.eulerAngles = rightDirectionRotation;
                return;
            }

            transform.eulerAngles = leftDirectionRotation;
        }

        private bool IsGrounded() => Physics2D.Raycast(jumpOrigin.position, Vector2.down, jumpthreshold, jumpLayerMask);

        private void HandleHealthChange(int oldValue, int newValue)
        {
            if (oldValue > newValue)
            {
                // TODO: DamageComponent.HasImmunity
                DamageVFXComponent.GetDamageVFX();

                // TODO: Create and move to CameraShake?
                var brain = Camera.main.GetComponent<CinemachineBrain>();
                var activeVirtualCamera = brain.ActiveVirtualCamera as CinemachineVirtualCameraBase;
                var impulseSource = activeVirtualCamera.GetComponent<CinemachineImpulseSource>();

                float xOffset = Random.Range(-0.1f, 0.1f);
                float yOffset = Random.Range(-0.1f, 0.1f);
                impulseSource.DefaultVelocity = new Vector3(xOffset, yOffset, 0);

                impulseSource.GenerateImpulse();
            }
            else
            {
                DamageVFXComponent.GetHealingVFX();
            }

            hud.UpdateHealthBar(oldValue, newValue);
        }

        private void HandleDeath()
        {
            DamageVFXComponent.DeathVFX();

            GameManager.Instance.ReloadCurrentScene();
        }

        private void PerformAttack()
        {
            var rnd = Random.Range(0, attackAudioClips.Length);
            attackAudioSource.PlayOneShot(attackAudioClips[rnd]);

            var colliders = Physics2D.OverlapCircleAll(attackOrigin.position, attackRadius, attackLayerMask);
            foreach (var other in colliders)
            {
                var directionToOther = (other.transform.position - transform.position).normalized;

                var otherDamageComponent = other.gameObject.GetComponent<DamageComponent>();
                if (otherDamageComponent)
                {
                    otherDamageComponent.GetDamage(DamageAmount);

                    if (attackHitParticlesPrefab != null)
                    {
                        float angle = Mathf.Atan2(directionToOther.y, directionToOther.x) * Mathf.Rad2Deg;
                        var rotation = Quaternion.Euler(0, 0, angle);

                        Instantiate(attackHitParticlesPrefab, other.transform.position, rotation);
                    }

                    StartCoroutine(ApplySlowMotion(attackSlowMotionScale, attackSlowMotionDuration));
                }
            }
        }

        // TODO: Create and move to TimeController?
        private IEnumerator ApplySlowMotion(float scale, float duration)
        {
            Time.timeScale = scale;

            yield return new WaitForSecondsRealtime(duration);

            Time.timeScale = 1f;
        }

        public void OnDrawGizmos()
        {
            if (attackOrigin)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
            }
        }
    }
}

