using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    private Rigidbody2D rigidbody;
    private Vector2 playerDirection;

    void Start() {
        rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0.0f;
    }

    void Movement() {
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    
    // Update is called once per frame
    void Update() {
        Movement();
    }

    private void FixedUpdate() {
        rigidbody.velocity = new Vector2(playerDirection.x * playerSpeed, playerDirection.y * playerSpeed);
    }
}
