using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPath : MonoBehaviour {
	
	public Vector3[] path = { new Vector3(0,0,0), new Vector3(5,0,0), new Vector3(5,5,0) };
	//private LineRenderer lineRenderer;

	void Start () {
		//lineRenderer = GetComponent<LineRenderer>();
	}

	void Update() {
		//lineRenderer.positionCount = path.Length;
		//lineRenderer.SetPositions(path);
	}
}
