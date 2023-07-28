using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeToEnemySpawn;

    [SerializeField] private ScoreManager scoreManager;

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        var instance = Instantiate(enemyPrefab);
        var enemy = instance.GetComponent<EnemyController>();

        yield return new WaitForSeconds(timeToEnemySpawn);
        StartCoroutine(SpawnEnemy());
    }
    
    private void OnEnemyDestroyed()
    {
        scoreManager.IncreaseScore();
    }
}
