using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRadius = 12f;

    private float timer;

    private void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * spawnRadius;
        Vector3 spawnPosition = player.position + offset;
        Instantiate(Prefab, spawnPosition, Quaternion.identity);
    }
}