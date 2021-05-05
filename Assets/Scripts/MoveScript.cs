using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);

    [Range(1, 10)]
    public float fallMultiplier = 2.5f;

    private Rigidbody2D rigidbodyComponent;
    private Vector2 movement;

    private void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (rigidbodyComponent.velocity.y < 0)
        { rigidbodyComponent.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime; }

        movement = new Vector2(
          speed.x * direction.x,
          rigidbodyComponent.velocity.y
        );
    }

    void FixedUpdate(){
        rigidbodyComponent.velocity = movement;
    }
}
