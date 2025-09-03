using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemySpawner
{
    private Enemy enemyPrefab;

    private float spawnPointsOffset;

    private Vector2 bottomLeftCameraBorder;

    private float neccesarySpawnDistance;
    private Vector2 cameraCenter;

    private Transform targetTransform;

    private const float SpawnOffset = 0.5f;

    private EnemyObjectPool enemyObjectPool;

    public void Initialize(Enemy enemyPrefab, float spawnPointsOffset, Transform targetTransform, Transform enemySpawnerParent)
    {
        this.enemyPrefab = enemyPrefab;
        this.spawnPointsOffset = spawnPointsOffset;
        this.targetTransform = targetTransform;

        enemyObjectPool = new EnemyObjectPool();

        enemyObjectPool.Initialize(enemyPrefab, enemySpawnerParent);

        SetCameraInformation();

        SpawnEnemies().Forget();
    }

    private void SetCameraInformation()
    {
        var camera = Camera.main;

        float width = camera.pixelWidth;
        float height = camera.pixelHeight;

        bottomLeftCameraBorder = camera.ScreenToWorldPoint(new Vector2(0, 0));

        cameraCenter = (Vector2)Camera.main.transform.position;
        neccesarySpawnDistance = (cameraCenter - bottomLeftCameraBorder).magnitude + SpawnOffset;
    }

    private async UniTask SpawnEnemies()
    {
        while (true)
        {
            await UniTask.WaitForSeconds(0.5f);

            var enemy = enemyObjectPool.GetFromPool();

            enemy.transform.SetParent(null);
            enemy.gameObject.SetActive(true);

            enemy.transform.position = Point();
            enemy.Initialize(targetTransform);

            enemy.OnDead += OnEnemyDead;
        }
    }

    private Vector2 Point()
    {
        Vector2 xPosition = Vector2.zero;

        float distance;

        do
        {
            xPosition = Random.insideUnitCircle * (neccesarySpawnDistance + spawnPointsOffset);

            distance = (xPosition - cameraCenter).magnitude;

        } while (distance < neccesarySpawnDistance);

        return xPosition;
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.OnDead -= OnEnemyDead;

        enemy.gameObject.SetActive(false);
        enemyObjectPool.SetToPool(enemy);
    }
}