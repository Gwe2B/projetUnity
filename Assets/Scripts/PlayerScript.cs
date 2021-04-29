using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public Vector2 speed = new Vector2(20, 20);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    private void Start() {
        if (rigidbodyComponent == null) {
            rigidbodyComponent = GetComponent<Rigidbody2D>();
        }
    }

    void Update() {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(
            speed.x * inputX,
            rigidbodyComponent.velocity.y + speed.y * inputY
        );
    }

    void FixedUpdate() {
        rigidbodyComponent.velocity = movement;
    }
}
