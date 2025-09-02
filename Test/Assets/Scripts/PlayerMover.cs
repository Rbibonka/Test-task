using UnityEngine;

public class PlayerMover
{
    private Rigidbody2D rb;

    private float moveSpeed;
    private float jumpForce;

    public void Initialize(Rigidbody2D rb, float moveSpeed, float jumpForce)
    {
        this.rb = rb;
        this.moveSpeed = moveSpeed;
        this.jumpForce = jumpForce;
    }

    public void Move(float moveDiraction)
    {
        rb.velocity = new Vector2(moveDiraction * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
    }
}