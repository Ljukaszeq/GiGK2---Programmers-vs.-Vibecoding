using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [Header("Ustawienia Fali")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public Transform[] waypoints;

    public float timeBetweenWaves = 5f;
    private float countdown;
    private int waveIndex = 0;
    private bool isSpawning = false;

    void Start()
    {
        countdown = timeBetweenWaves;
    }

    void Update()
    {
        if (isSpawning) return;

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves; // Reset odliczania dopiero po zakończeniu fali
            return;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;
        waveIndex++;
        Debug.Log($"Rozpoczyna się fala nr: {waveIndex}");

        int enemiesInWave = waveIndex * 3;

        for (int i = 0; i < enemiesInWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); // Odstęp między wrogami
        }

        isSpawning = false;
    }

    void SpawnEnemy()
    {
        GameObject enemyGO = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        EnemyPathing pathing = enemyGO.GetComponent<EnemyPathing>();
        if (pathing != null)
        {
            pathing.waypoints = waypoints;
        }
    }
}