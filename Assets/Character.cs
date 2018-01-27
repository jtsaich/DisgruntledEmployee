using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	private LineRenderer lineRenderer;
	private Movement movementScript;

	void Start () {
		movementScript = GetComponentInChildren<Movement>();
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Update() {
		lineRenderer.positionCount = movementScript.path.Length;
		lineRenderer.SetPositions(movementScript.path);
	}
}
