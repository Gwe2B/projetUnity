using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(20, 20);

    [Range(1, 10)]
    public float jumpVelocity = 2f;

    [Range(1, 10)]
    public float fallMultiplier = 2.5f;

    [Range(1, 10)]
    public float lowJumpMultiplier = 2f;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && rigidbodyComponent.velocity.y == 0)
        { rigidbodyComponent.velocity = Vector2.up * jumpVelocity; }
        else if (rigidbodyComponent.velocity.y > 0 && !Input.GetButton("Jump"))
        { rigidbodyComponent.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime; }
        if (rigidbodyComponent.velocity.y < 0)
        { rigidbodyComponent.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; }

        movement = new Vector2(
            speed.x * inputX,
            rigidbodyComponent.velocity.y
        );

        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z
        );
    }

    void FixedUpdate()
    {
        rigidbodyComponent.velocity = movement;
    }
}
