using UnityEngine;

public class HealthScript : MonoBehaviour {
    public int maxHp = 4;
    public bool isEnemy = false;

    public HealthBar hb;

    private int hp;

    private void Start() {
        hp = maxHp;
        
        if(hb != null) { hb.SetMaxHealth(maxHp); }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        ShotScript shot = collision.gameObject.GetComponent<ShotScript>();

        if(shot != null && isEnemy && (collision.gameObject.tag == "Bullet")) {
            hp -= shot.damage;
            if (hb != null) { hb.SetHealth(hp); }
            Destroy(shot.gameObject);

            if(hp <= 0) {
                Destroy(gameObject);
            }
        }
    }

    public void SetHp(int newhp)
    {
        if (newhp <= maxHp) { hp = newhp; }
    }

    public int GetHp() { return hp; }
}
