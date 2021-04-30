using System.Collections;
using System.Collections.Generic;
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
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();

        if(shot != null) {
            if(shot.isEnemyShot != isEnemy) {
                hp -= shot.damage;
                hb.SetHealth(hp);
                Destroy(shot.gameObject);

                if(hp <= 0) {
                    Destroy(gameObject);
                }
            }
        } else if(enemy != null) {
            if (!isEnemy) {
                hp -= enemy.damage;
                hb.SetHealth(hp);
                Destroy(enemy.gameObject);

                if (hp <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
