using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform shotPoint;

    [SerializeField]
    private Bullet bullerPrefab;

    [SerializeField]
    private float bulletSpeed = 2f;

    [SerializeField]
    private float reloadTime = 0.1f;

    private BulletObjectPool bulletObjectPool;

    private bool isReoladed = false;

    private CancellationTokenSource cts;

    public void Initialize()
    {
        cts = new();

        bulletObjectPool = new();
        bulletObjectPool.Initialize(bullerPrefab, shotPoint);
    }

    public void Shot()
    {
        if (isReoladed)
        {
            return;
        }

        var bullet = bulletObjectPool.GetFromPool();

        bullet.gameObject.SetActive(true);
        bullet.transform.SetParent(null);

        bullet.transform.position = shotPoint.position;
        bullet.transform.rotation = shotPoint.rotation;

        bullet.StartShot(bulletSpeed);

        bullet.Destroyed += OnBulletDestroyed;
        bullet.Deinitialize += OnBulletDiinitialize;

        ReloadTimer(reloadTime, cts.Token).Forget();
    }

    private void OnBulletDestroyed(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);

        bulletObjectPool.SetToPool(bullet);

        bullet.Destroyed -= OnBulletDestroyed;
        bullet.Deinitialize -= OnBulletDiinitialize;
    }

    private void OnBulletDiinitialize(Bullet bullet)
    {
        bullet.Destroyed -= OnBulletDestroyed;
        bullet.Deinitialize -= OnBulletDiinitialize;
    }

    private async UniTask ReloadTimer(float time, CancellationToken ct)
    {
        isReoladed = true;

        await UniTask.WaitForSeconds(time, cancellationToken: ct);

        isReoladed = false;
    }
}