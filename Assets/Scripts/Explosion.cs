using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	private Animator animator;
	private Animation animation;

	void Start () {
		animator = GetComponent<Animator>();
		animation = GetComponent<Animation>();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.L)) {
			Explode();
		}
	}

	public void Explode() {
		//animation.Play("ExplosionAnimation");
		animation.Play();
		Debug.Log("Playing animation");
		Destroy(gameObject, animation.clip.length + 1);
	}
}
