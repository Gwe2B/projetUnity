using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {
    public int hp = 1;
    public bool isEnemy = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        ShotScript shot = collision.gameObject.GetComponent<ShotScript>();

        if(shot != null) {
            if(shot.isEnemyShot != isEnemy) {
                hp -= shot.damage;
                Destroy(shot.gameObject);

                if(hp <= 0) {
                    Destroy(gameObject);
                }
            }
        }
    }
}
