using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour {
    
    public List<GameObject> withinRadius = new List<GameObject>();


    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Explodable") {
            withinRadius.Add(gameObject);
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag == "Explodable") {
            withinRadius.Remove(gameObject);
        }
    }
}
