using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : PoolableObject
{
    [SerializeField]
    private Rigidbody2D rigidbody;

    public event Action<Bullet> Destroyed;

    private CancellationTokenSource cts;

    public void StartShot(float force)
    {
        if (cts == null)
        {
            cts = new();
        }

        rigidbody.AddForce(rigidbody.transform.right * force, ForceMode2D.Impulse);

        TimerToDead(3f, cts.Token).Forget();
    }

    public void Destroy()
    {
        Destroyed?.Invoke(this);

        cts?.Cancel();
        cts?.Dispose();

        cts = null;
    }

    private async UniTask TimerToDead(float time, CancellationToken ct)
    {
        await UniTask.WaitForSeconds(time, cancellationToken: ct);

        Destroy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(1);

            Destroy();
        }
    }
}