using UnityEngine;

public class EnemyMover
{
    private float moveSpeed;

    private Rigidbody2D rigidbody;

    public void Initialize(float moveSpeed, Rigidbody2D rigidbody)
    {
        this.moveSpeed = moveSpeed;
        this.rigidbody = rigidbody;
    }

    public void MoveToTarget(Vector2 targetPosition)
    {
        var targetDirection = (targetPosition - (Vector2)rigidbody.transform.position).normalized;

        var xVelocity = (targetDirection * moveSpeed).x;

        rigidbody.velocity = new Vector2(xVelocity, rigidbody.velocity.y);
    }
}