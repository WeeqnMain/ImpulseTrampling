using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] enemyDeathSounds;


    public void Init(ScoreManager scoreManager)
    {
        this.scoreManager = scoreManager;

        enemySpawnPerimeter = new EnemySpawnPerimeter(leftBottomPoint, leftTopPoint, rightTopPoint, rightBottomPoint);

        pool = new PoolMono<EnemyController>(enemyPrefab, capacity, container);
        pool.autoExpand = autoExpand;

        foreach (var enemy in pool.GetAllElements())
        {
            enemy.Destroyed += scoreManager.OnEnemyDestroyed;
            enemy.Destroyed += OnEnemyDestroy;
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

        var enemy = instance.GetComponent<EnemyController>();
        enemy.SetRotation(enemyRotateTo);

        if (pool.GetAllElements().Count > capacity)
        {
            enemy.Destroyed += scoreManager.OnEnemyDestroyed;
            enemy.Destroyed += OnEnemyDestroy;
            capacity = pool.GetAllElements().Count;
        }

        yield return new WaitForSeconds(timeToEnemySpawn);
        StartCoroutine(SpawnEnemy());
    }

    private void OnEnemyDestroy(int value)
    {
        var clip = enemyDeathSounds[Random.Range(0, enemyDeathSounds.Length)];
        audioSource.PlayOneShot(clip, 1);
    }
}
