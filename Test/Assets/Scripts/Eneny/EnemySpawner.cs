using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class EnemySpawner
{
    private float spawnPointsOffset;

    private Vector2 bottomLeftCameraBorder;

    private float neccesarySpawnDistance;
    private Vector2 cameraCenter;

    private Transform targetTransform;

    private const float SpawnOffset = 0.5f;

    private EnemyObjectPool enemyObjectPool;

    private CancellationTokenSource cts;

    public void Initialize(Enemy enemyPrefab, float spawnPointsOffset, Transform targetTransform, Transform enemySpawnerParent)
    {
        cts = new();

        this.spawnPointsOffset = spawnPointsOffset;
        this.targetTransform = targetTransform;

        enemyObjectPool = new EnemyObjectPool();

        enemyObjectPool.Initialize(enemyPrefab, enemySpawnerParent);

        SetCameraInformation();

        SpawnEnemies(cts.Token).Forget();
    }

    public void Deinitialize()
    {
        cts?.Cancel();
        cts?.Dispose();
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

    private async UniTask SpawnEnemies(CancellationToken ct)
    {
        while (true)
        {
            await UniTask.WaitForSeconds(3f, cancellationToken: ct);

            var enemy = enemyObjectPool.GetFromPool();

            enemy.transform.SetParent(null);
            enemy.gameObject.SetActive(true);

            enemy.transform.position = GetPoint();
            enemy.Initialize(targetTransform);

            enemy.OnDead += OnEnemyDead;
        }
    }

    private Vector2 GetPoint()
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