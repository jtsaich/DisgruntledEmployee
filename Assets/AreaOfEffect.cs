using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour {

    public float shrinkRate = 0.01F;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale -= new Vector3(shrinkRate, 0, shrinkRate);
        if (transform.localScale.x < 0)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter (Collision collision) {
        
    }
}
