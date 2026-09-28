using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float destroyDistance = 10f;
    [SerializeField] private float stuckTime = 1.5f;

    private float stuckTimer;

    private Rigidbody2D rb;
    private Transform player;
    private ObstacleSpawner spawner;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    public void SetSpawner(ObstacleSpawner obstacleSpawner)
    {
        spawner = obstacleSpawner;
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(
                -speed,
                rb.linearVelocity.y
            );
        }
    }

    private void Update()
    {
        // si queda muy atrás del jugador, se destruye
        if (player != null &&
            transform.position.x <
            player.position.x - destroyDistance)
        {
            DestroyObstacle();
        }
    }
    //si se separan reinicia el temporizador
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
    
        {
            stuckTimer += Time.deltaTime;

            if (stuckTimer >= stuckTime)
            {
                DestroyObstacle();
            }
        }
        
    }

    public void TakeDamage()
    {
        // reproducir sonido cuando muere el ratón enemigo
    FindFirstObjectByType<SFXManager>()?.PlayDie();

        DestroyObstacle();
    }

    private void DestroyObstacle()
    {
        if (spawner != null)
        {
            spawner.ObstacleDestroyed(gameObject);
        }

        Destroy(gameObject);
    }
}