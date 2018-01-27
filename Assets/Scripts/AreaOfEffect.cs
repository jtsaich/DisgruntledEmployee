using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour {

    public float shrinkRate = 0.01F;
	public float startScale = 5;

	void Start() {
		transform.localScale = new Vector3(startScale * 2, startScale, 1);
	}

	public void UpdateScale(float scale) {
		transform.localScale = new Vector3(scale * 2, scale, 1);
	}

	void Update () {
		if (shrinkRate > float.Epsilon) {
			transform.localScale -= new Vector3(shrinkRate * 2, shrinkRate, 0);
			if (transform.localScale.x < 0) {
				Destroy(gameObject);
			}
		}
	}

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "CharacterHitBox") {
            collider.gameObject.SendMessage("Infect");
        }

		Debug.Log("Collided with " + collider.gameObject.tag);
    }
}
