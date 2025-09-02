using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private float moveSpeed;

    private Transform targetTransform;

    private EnemyMover enemyMover;

    public void Initialize(Transform targetTransform)
    {
        this.targetTransform = targetTransform;

        enemyMover = new();
        enemyMover.Initialize(moveSpeed, rigidbody);
    }

    public void FixedUpdate()
    {
        if (targetTransform == null)
        {
            return;
        }

        enemyMover.MoveToTarget(targetTransform.position);
    }
}