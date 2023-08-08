using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Specs")]
    [SerializeField] private int minEnemyMoveSpeed;
    [SerializeField] private int maxEnemyMoveSpeed;
    [SerializeField] private float timeToEnemySpawn;

    [Header("Pool")]
    [SerializeField] private int capacity;
    [SerializeField] private bool autoExpand;
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private Transform container;
    private PoolMono<EnemyController> pool;

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
    }

    private void Awake()
    {
        enemySpawnPerimeter = new EnemySpawnPerimeter(leftBottomPoint, leftTopPoint, rightTopPoint, rightBottomPoint);

        pool = new PoolMono<EnemyController>(enemyPrefab, capacity, container);
        pool.autoExpand = autoExpand;
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

        var enemy = instance.GetComponent<EnemyController>();
        enemy.Init(enemyRotateTo, minEnemyMoveSpeed, maxEnemyMoveSpeed);
        enemy.Destroyed += scoreManager.OnEnemyDestroyed;

        yield return new WaitForSeconds(timeToEnemySpawn);
        StartCoroutine(SpawnEnemy());
    }
}
