using UnityEngine;

public class PlayerMover
{
    private Rigidbody2D rb;

    private float moveSpeed;

    public void Initialize(Rigidbody2D rb, float moveSpeed)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
    }

    public void Move(Vector2 moveDiraction)
    {
        rb.velocity = new Vector2(moveDiraction.x * moveSpeed, moveDiraction.y * moveSpeed);
    }
}