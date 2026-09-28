using UnityEngine;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private HeartsUI heartsUI;
    [SerializeField] private TMP_Text invincibilityTimerText;
    [SerializeField] private TMP_Text coinsText;

    private Rigidbody2D rb;
    private Animator animator;

    [Header("Control de vidas")]
    private int currentLives;
    private int maxLives = 5;

    [Header("Control de monedas")]
    private int coinsCount = 0;

    [Header("Doble salto")]
    private int maxJumps = 2;
    private int jumpsRemaining;

    [Header("Configuracion de Rebote y Colision")]
    [SerializeField] private float bounceForce = 8f;
    [SerializeField] private float knockbackForce = 6f;
    [SerializeField] private float knockbackUpForce = 3f;

    [Header("Power up Invencibilidad")]
    [SerializeField] private ParticleSystem shieldParticles;
    private bool isInvincible = false;
    private Coroutine invincibilityCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentLives = playerData.lives;
        jumpsRemaining = maxJumps;

        UpdateLivesUI();
        UpdateCoinsUI();

        if (invincibilityTimerText != null)
        {
            invincibilityTimerText.gameObject.SetActive(false);
        }

        if (shieldParticles != null)
        {
            shieldParticles.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Run();
        Jump();
    }

    private void Run()
    {
        rb.linearVelocity = new Vector2(
            playerData.speed,
            rb.linearVelocity.y
        );

        if (animator != null)
        {
            animator.SetBool("IsRunning", playerData.speed != 0);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                playerData.jumpForce
            );
            jumpsRemaining--;

            // reproduce el sonido de salto desde el SFXManager
            SFXManager sfx = FindFirstObjectByType<SFXManager>();
            if (sfx != null)
            {
                sfx.PlayJump();
            }

            if (animator !=null)
            {
                animator.SetBool("Jump", true);
                animator.SetBool("Land", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
            if (animator !=null)
            {
                animator.SetBool("Jump", false);
                animator.SetBool("Land", true);
            }
        }

        // Detección de colisión con el obstáculo / ratón
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector2 contactNormal = collision.contacts[0].normal;

            // Si salta encima del ratón
            if (contactNormal.y > 0.5f)
            {
                MouseEnemy mouse = collision.gameObject.GetComponent<MouseEnemy>();
                if (mouse != null)
                {
                    mouse.TakeDamage();
                }
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
                jumpsRemaining = 1;
            }
            else // Impacto lateral o frontal
            {
                if (isInvincible)
                {
                    // Si está invencible, destruye o elimina al obstáculo/ratón inmediatamente
                    MouseEnemy mouse = collision.gameObject.GetComponent<MouseEnemy>();
                    if (mouse != null)
                    {
                        mouse.TakeDamage();
                    }
                    else
                    {
                        Destroy(collision.gameObject);
                    }
                }
                else
                {
                    // Empuja al gato y quita vida
                    rb.linearVelocity = new Vector2(-knockbackForce, knockbackUpForce);
                    LoseLife();
                }
            }
        }
    }
    private void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (animator !=null)
            {
                animator.SetBool("Land", false);
            }
        }
    }

    private void LoseLife()
    {
        if (isInvincible) return;

        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        // reproducir sonido de muerte del personaje
        FindFirstObjectByType<SFXManager>()?.PlayGameOver();
        gameObject.SetActive(false);
    }

    private void UpdateLivesUI()
    {
        if (heartsUI != null)
        {
            heartsUI.UpdateHearts(currentLives);
        }
    }

    public void AddCoins(int amount)
    {
        coinsCount += amount;
        UpdateCoinsUI();
    }

    private void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = coinsCount.ToString();
        }
    }

    // Método público para llamar desde el PowerUp
    public void ActivateInvincibility(float duration)
    {
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
        }
        invincibilityCoroutine = StartCoroutine(InvincibilityRoutine(duration));
    }



    private IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;

        // Activa el efecto de partículas
        if (shieldParticles != null)
        {
            shieldParticles.gameObject.SetActive(true);
            shieldParticles.Play();
        }

        // Activa y gestiona el UI del tiempo
        if (invincibilityTimerText != null)
        {
            invincibilityTimerText.gameObject.SetActive(true);
        }

        float timeLeft = duration;

        while (timeLeft > 0f)
        {
            if (invincibilityTimerText != null)
            {
                invincibilityTimerText.text = "INVINCIBLE: " + Mathf.CeilToInt(timeLeft);
            }

            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        // Desactiva efectos al terminar
        if (shieldParticles != null)
        {
            shieldParticles.Stop();
            shieldParticles.gameObject.SetActive(false);
        }

        if (invincibilityTimerText != null)
        {
            invincibilityTimerText.gameObject.SetActive(false);
        }

        isInvincible = false;

        invincibilityCoroutine = null;
    }

    public void AddLife(int amount)
    {
        currentLives += amount;
        if (currentLives > maxLives)
        {
            currentLives = maxLives;
        }
        UpdateLivesUI();
    }
}