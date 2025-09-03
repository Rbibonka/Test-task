using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : PoolableObject
{
    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private float moveSpeed;

    private Transform targetTransform;

    private EnemyMover enemyMover;

    public event Action<Enemy> OnDead;

    public void Initialize(Transform targetTransform)
    {
        this.targetTransform = targetTransform;

        enemyMover = new();
        enemyMover.Initialize(moveSpeed, rigidbody);

        WaitForDead().Forget();
    }

    private async UniTask WaitForDead()
    {
        await UniTask.WaitForSeconds(5f);

        Dead();
    }

    public void FixedUpdate()
    {
        if (targetTransform == null)
        {
            return;
        }

        enemyMover.MoveToTarget(targetTransform.position);
    }

    public void Dead()
    {
        OnDead?.Invoke(this);
    }
}