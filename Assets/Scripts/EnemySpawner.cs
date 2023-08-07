using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int minEnemyMoveSpeed;
    [SerializeField] private int maxEnemyMoveSpeed;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeToEnemySpawn;

    private ScoreManager scoreManager;

    private EnemySpawnPerimeter enemySpawnPerimeter;

    [SerializeField] private Transform leftBottomPoint;
    [SerializeField] private Transform leftTopPoint;
    [SerializeField] private Transform rightTopPoint;
    [SerializeField] private Transform rightBottomPoint;

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;
    }

    private void Awake()
    {
        enemySpawnPerimeter = new EnemySpawnPerimeter(leftBottomPoint, leftTopPoint, rightTopPoint, rightBottomPoint);
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        enemySpawnPerimeter.GetRandomTransform(out Vector3 enemySpawnPoint, out Vector3 enemyRotateTo);

        var instance = Instantiate(enemyPrefab, enemySpawnPoint, Quaternion.identity);

        var enemy = instance.GetComponent<EnemyController>();
        enemy.Init(enemyRotateTo, minEnemyMoveSpeed, maxEnemyMoveSpeed);
        enemy.Destroyed += scoreManager.OnEnemyDestroyed;

        yield return new WaitForSeconds(timeToEnemySpawn);
        StartCoroutine(SpawnEnemy());
    }
}
