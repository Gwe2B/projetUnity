using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour {
    public Vector2 speed = new Vector2(2, 2);
    public Vector2 direction = new Vector2(1, 0);
    public bool isLinkedToCamera = false;
    public bool isLooping = false;

    private List<Transform> backgroundPart;

    void Start() { 
        if(isLooping) {
            backgroundPart = new List<Transform>();

            for(int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);

                if(child.GetComponent<Renderer>() != null) {
                    backgroundPart.Add(child);
                }
            }

            backgroundPart = backgroundPart.OrderBy(
                t => t.position.x
            ).ToList();
        }
    }

    void Update()
    {
        // Mouvement
        Vector3 movement = new Vector3(
            speed.x * direction.x,
            speed.y * direction.y,
            0
        );

        movement *= Time.deltaTime;
        transform.Translate(movement);

        // D�placement cam�ra
        if (isLinkedToCamera)
        { Camera.main.transform.Translate(movement); }

        // 4 - R�p�tition
        if (isLooping) {
            // On prend le premier objet (la la liste est ordonn�e)
            Transform firstChild = backgroundPart.FirstOrDefault();

            if (firstChild != null) {
                // Premier test sur la position de l'objet
                // Cela �vite d'appeler directement IsVisibleFrom
                // qui est assez lourde � ex�cuter
                if (firstChild.position.x < Camera.main.transform.position.x) {
                    // On v�rifie maintenant s'il n'est plus visible de la cam�ra
                    if (firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false) {
                        // On r�cup�re le dernier �l�ment de la liste
                        Transform lastChild = backgroundPart.LastOrDefault();

                        // On calcule ainsi la position � laquelle nous allons replacer notre morceau
                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.GetComponent<Renderer>().bounds.max - lastChild.GetComponent<Renderer>().bounds.min);

                        // On place le morceau tout � la fin
                        // Note : ne fonctionne que pour un scorlling horizontal
                        firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

                        // On met � jour la liste (le premier devient dernier)
                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
    }
}