using UnityEngine;


public class EnemyScript : MonoBehaviour {
    public int damage = 1;

    private bool hasSpawn;

    private Rigidbody2D rigidbody;
    private MoveScript moveScript;
    private Collider2D coliderComponent;
    private SpriteRenderer rendererComponent;

    void Awake() {
        // Retrieve scripts to disable when not spawn
        rigidbody = GetComponent<Rigidbody2D>();
        moveScript = GetComponent<MoveScript>();
        coliderComponent = GetComponent<Collider2D>();
        rendererComponent = GetComponent<SpriteRenderer>();
    }

    // 1 - Disable everything
    void Start() {
        hasSpawn = false;

        // Disable everything
        rigidbody.Sleep();
        coliderComponent.enabled = false;
        moveScript.enabled = false;
    }

    void Update() {
        // 2 - Check if the enemy has spawned.
        if (hasSpawn == false) {
            if (rendererComponent.IsVisibleFrom(Camera.main))
            { Spawn(); }
        }
        else {
            if (rendererComponent.IsVisibleFrom(Camera.main) == false)
            { Destroy(gameObject); }
        }
    }

    // 3 - Activate itself.
    private void Spawn() {
        hasSpawn = true;

        // Enable everything
        rigidbody.WakeUp();
        coliderComponent.enabled = true;
        moveScript.enabled = true;
    }
}
