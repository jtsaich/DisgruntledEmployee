using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public float moveSpeed = 5f;
	public int nextPoint = 0;
	private Vector3[] path;

	void Start() {
		path = GetComponentInParent<CharacterPath>().path;
		nextPoint = Mathf.RoundToInt(Random.Range(0f, path.Length - 1));
	}

	void Update () {
		transform.position = (Vector3) Vector2.MoveTowards((Vector2) transform.position, (Vector2) path[nextPoint], moveSpeed * Time.deltaTime);

		if (transform.position == path[nextPoint]) {
			nextPoint = (nextPoint + 1) % path.Length;
		}
	}
}
