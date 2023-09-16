using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeToEnemySpawn;

    [Header("Pool")]
    [SerializeField] private int capacity;
    [SerializeField] private bool autoExpand;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform container;
    private PoolMono<Enemy> pool;

    private ScoreManager scoreManager;

    [Header("Spawn Perimeter")]
    [SerializeField] private Transform leftBottomPoint;
    [SerializeField] private Transform leftTopPoint;
    [SerializeField] private Transform rightTopPoint;
    [SerializeField] private Transform rightBottomPoint;
    private EnemySpawnPerimeter enemySpawnPerimeter;

    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;

        enemySpawnPerimeter = new EnemySpawnPerimeter(leftBottomPoint, leftTopPoint, rightTopPoint, rightBottomPoint);

        pool = new PoolMono<Enemy>(enemyPrefab, capacity, container);
        pool.autoExpand = autoExpand;

        foreach (var enemy in pool.GetAllElements())
        {
            enemy.Destroyed += scoreManager.OnEnemyDestroyed;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        enemySpawnPerimeter.GetRandomTransform(out Vector3 enemySpawnPoint, out Vector3 enemyRotateTo);

        var instance = pool.GetFreeElement();
        instance.transform.position = enemySpawnPoint;

        var enemy = instance.GetComponent<Enemy>();
        enemy.SetRotation(enemyRotateTo);

        if (pool.GetAllElements().Count > capacity)
        {
            enemy.Destroyed += scoreManager.OnEnemyDestroyed;
            capacity = pool.GetAllElements().Count;
        }

        yield return new WaitForSeconds(timeToEnemySpawn);
        StartCoroutine(SpawnEnemy());
    }
}
