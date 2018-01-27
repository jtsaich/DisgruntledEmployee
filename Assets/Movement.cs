using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public Vector3[] path = { new Vector3(0,0,0), new Vector3(5,0,0), new Vector3(5,5,0) };
	public float moveSpeed = 5f;
	public int nextPoint = 0;

	// Update is called once per frame
	void Update () {
		transform.position = (Vector3) Vector2.MoveTowards((Vector2) transform.position, (Vector2) path[nextPoint], moveSpeed * Time.deltaTime);

		if (transform.position == path[nextPoint]) {
			nextPoint = (nextPoint + 1) % path.Length;
		}
	}
}
