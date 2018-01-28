using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour {

    private float shrinkRate;
	private float scale;

	public void Initialize(float scale, float shrink) {
		shrinkRate = shrink;
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
			collider.gameObject.GetComponent<Character>().Infect();
		} else if (collider.gameObject.tag == "Explodable") {
			collider.gameObject.GetComponent<Explodable>().Explode();
		}
    }
}
