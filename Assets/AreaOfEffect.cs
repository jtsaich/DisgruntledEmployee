using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour {

    public float shrinkRate = 0.01F;

	void Update () {
		if (shrinkRate > float.Epsilon) {
			transform.localScale -= new Vector3(shrinkRate, shrinkRate, 0);
			if (transform.localScale.x < 0) {
				Destroy(gameObject);
			}
		}
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "CharacterHitBox")
        {
            collider.gameObject.SendMessage("Infect");
        }

		Debug.Log("Collided with " + collider.gameObject.tag);
    }
}
