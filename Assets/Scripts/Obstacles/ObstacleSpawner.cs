using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject obstaclePrefab;

    [Header("Referencias")]
    [SerializeField] private Transform player;

    [Header("Configuracion")]
    [SerializeField] private float spawnDistance = 12f;
    [SerializeField] private float minSpawnTime = 2.5f;
    [SerializeField] private float maxSpawnTime = 4f;

    private float nextSpawnTime;
    private GameObject currentObstacle;

    private void Start()
    {
        SetNextSpawnTime();
    }

    private void Update()
    {
        if (currentObstacle == null && Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefab == null || player == null)
            return;

        Vector3 spawnPosition = new Vector3(
            player.position.x + spawnDistance,
            transform.position.y,
            0f
        );

        currentObstacle = Instantiate(
            obstaclePrefab,
            spawnPosition,
            Quaternion.identity
        );

        MouseEnemy mouse = currentObstacle.GetComponent<MouseEnemy>();

        if (mouse != null)
        {
            mouse.SetSpawner(this);
        }
    }

    public void ObstacleDestroyed(GameObject obstacle)
    {
        if (currentObstacle == obstacle)
        {
            currentObstacle = null;
            SetNextSpawnTime();
        }
    }

    private void SetNextSpawnTime()
    {
        nextSpawnTime =
            Time.time + Random.Range(
                minSpawnTime,
                maxSpawnTime
            );
    }
}