using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform player;

    [Header("Prefabs")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject extraLifePrefab;
    [SerializeField] private GameObject invincibilityPrefab;

    [Header("Configuracion de distancia")]
    [SerializeField] private float spawnDistance = 12f;
    [SerializeField] private float minSpawnTime = 2f;
    [SerializeField] private float maxSpawnTime = 4f;

    [Header("Probabilidades")]
    [SerializeField] private float coinChance = 70f;
    [SerializeField] private float extraLifeChance = 15f;
    [SerializeField] private float invincibilityChance = 15f;

    private float nextSpawnTime;
   

    private void Start()
    {
        SetNextSpawnTime();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnRandomItem();
            SetNextSpawnTime();
        }
    }

    private void SpawnRandomItem()
    {
        if (player == null)
            return;

        GameObject selectedPrefab = SelectRandomItem();

        if (selectedPrefab == null)
            return;

        float[] possibleYPositions = { -1f, 1f, 1.5f };

        float randomY =
            possibleYPositions[
                Random.Range(0, possibleYPositions.Length)
            ];

        Vector3 spawnPosition = new Vector3(
            player.position.x + spawnDistance,
            randomY,
            0f
        );

        Instantiate(
            selectedPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    private GameObject SelectRandomItem()
    {
        float randomValue =
            Random.Range(
                0f,
                coinChance + extraLifeChance + invincibilityChance
            );

        if (randomValue < coinChance)
        {
            return coinPrefab;
        }

        randomValue -= coinChance;

        if (randomValue < extraLifeChance)
        {
            return extraLifePrefab;
        }

        return invincibilityPrefab;
    }

    private void SetNextSpawnTime()
    {
        nextSpawnTime =
            Time.time + Random.Range(minSpawnTime, maxSpawnTime);
    }
}