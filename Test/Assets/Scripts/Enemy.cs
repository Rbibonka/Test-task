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

    [SerializeField]
    private int health;

    private int currentHealth;

    private Transform targetTransform;

    private EnemyMover enemyMover;

    public event Action<Enemy> OnDead;

    public void Initialize(Transform targetTransform)
    {
        this.targetTransform = targetTransform;

        currentHealth = health;

        enemyMover = new();
        enemyMover.Initialize(moveSpeed, rigidbody);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Dead();
        }
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