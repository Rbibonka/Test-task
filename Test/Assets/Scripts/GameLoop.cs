using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

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

    private Player playerInstance;

    private CancellationTokenSource ctsGameLoop;

    private void Awake()
    {
        ctsGameLoop = new CancellationTokenSource();

        GameProcess(ctsGameLoop.Token).Forget();
    }

    private async UniTask GameProcess(CancellationToken ct)
    {
        await UniTask.WaitForSeconds(2f);

        playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        var enemy = Instantiate(enemyPrefab, Vector3.one, Quaternion.identity);

        enemy.Initialize(playerInstance.transform);
    }
}