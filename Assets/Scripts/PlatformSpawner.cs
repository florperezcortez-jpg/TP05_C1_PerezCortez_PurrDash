using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform player;
    [SerializeField] private GameObject platformPrefab;

    [Header("Configuracion")]
    [SerializeField] private float spawnDistance = 12f;
    [SerializeField] private float minSpawnTime = 3f;
    [SerializeField] private float maxSpawnTime = 5f;

    [Header("Alturas")]
    [SerializeField] private float[] possibleHeights = { 0.5f, 1f, 1.3f };

    private float nextSpawnTime;

    private void Start()
    {
        SetNextSpawnTime();
    }

    private void Update()
    {
        if (player == null || platformPrefab == null)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnPlatform();
            SetNextSpawnTime();
        }
    }

    private void SpawnPlatform()
    {
        if (possibleHeights.Length == 0)
            return;

        float randomY =
            possibleHeights[
                Random.Range(0, possibleHeights.Length)
            ];

        Vector3 spawnPosition = new Vector3(
            player.position.x + spawnDistance,
            randomY,
            0f
        );

        Instantiate(
            platformPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    private void SetNextSpawnTime()
    {
        nextSpawnTime =
            Time.time +
            Random.Range(minSpawnTime, maxSpawnTime);
    }
}
