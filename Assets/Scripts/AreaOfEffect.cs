using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour {
    
	private float scale;
    private float rate;

	public void Initialize(float scale, float duration) {
        transform.localScale = new Vector3(scale * 2, scale, 1);
        rate = scale / duration;
	}

	void Update () {
        float shrinkRate = rate * Time.deltaTime;
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
		}
    }
}
