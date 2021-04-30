using UnityEngine;

public class ShotScript : MonoBehaviour {
    public int damage = 1;
    public bool isEnemyShot = false;

    /*void Start() {
        Destroy(gameObject, 20); // 20sec
    }*/

    void Update() { 
        if(GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false) {
            Destroy(gameObject);
        }
    }
}