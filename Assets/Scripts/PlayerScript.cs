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
        bool shoot  = Input.GetButtonDown("Fire1");
             shoot |= Input.GetButtonDown("Fire2");

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

        if (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                weapon.Attack(false);
                FindObjectOfType<AudioManager>().Play("PlayerShot");
            }
        }
    }

    void FixedUpdate()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        
        rigidbodyComponent.velocity = movement;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z
        );
    }

    void OnDestroy() {
        transform.parent.gameObject.AddComponent<GameOverScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Zombie")
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            HealthScript myHp = GetComponent<HealthScript>();

            myHp.SetHp(myHp.GetHp() - enemy.damage);
            if (myHp.hb != null) { myHp.hb.SetHealth(myHp.GetHp()); }
            Destroy(enemy.gameObject);

            if (myHp.GetHp() <= 0)
            {
                Destroy(gameObject);
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bonus")
        {
            BonusScript bonus = collision.gameObject.GetComponent<BonusScript>();
            HealthScript myHp = GetComponent<HealthScript>();

            myHp.SetHp(myHp.GetHp() + bonus.hpBonus);
            if (myHp.hb != null) { myHp.hb.SetHealth(myHp.GetHp()); }
            Destroy(bonus.gameObject);
        }
        else if (collision.gameObject.tag == "Finish")
        {
            FindObjectOfType<GameManager>().EndGame(true);
        }
    }
}
