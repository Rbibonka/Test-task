using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for full game loop.
/// </summary>
[DefaultExecutionOrder(0)]
public class GameLoop : MonoBehaviour
{
    [SerializeField]
    private Player playerPrefab;

    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    private Transform enemySpawnerParent;

    [SerializeField]
    private ScreenFiller screenFiller;

    private Player playerInstance;

    EnemySpawner enemySpawner;

    private CancellationTokenSource ctsGameLoop;

    private void Awake()
    {
        ctsGameLoop = new CancellationTokenSource();

        enemySpawner = new();

        GameProcess(ctsGameLoop.Token).Forget();
    }

    private void OnDestroy()
    {
        ctsGameLoop.Cancel();
        ctsGameLoop.Dispose();

        enemySpawner.Deinitialize();
    }

    private async UniTask GameProcess(CancellationToken ct)
    {
        playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        playerInstance.Iniilize();

        await screenFiller.Fade(1, 0, ctsGameLoop.Token);

        enemySpawner.Initialize(enemyPrefab, 2f, playerInstance.transform, enemySpawnerParent);

        await UniTask.WaitUntil(() => playerInstance.isDead);

        await screenFiller.Fade(0, 1, ctsGameLoop.Token);

        playerInstance.Deinitialize();

        SceneManager.LoadScene(0);
    }
}