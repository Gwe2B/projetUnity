using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {
    public Vector2 speed = new Vector2(10, 10);
    public Vector2 direction = new Vector2(-1, 0);

    private Rigidbody2D rigidbodyComponent;
    private Vector2 movement;

    private void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update() {
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y
        );
    }

    void FixedUpdate(){
        rigidbodyComponent.velocity = movement;
    }
}
