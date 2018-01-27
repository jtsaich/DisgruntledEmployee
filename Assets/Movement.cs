using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;

public class Movement : MonoBehaviour {
	public float moveSpeed = 5f;
	public int nextPoint = 0;
	private Vector3[] path;

    private IsoTransform _isoTransform;

    void Awake() {
        _isoTransform = this.GetOrAddComponent<IsoTransform>(); //avoids polling the IsoTransform component per frame
    }

	void Start() {
		path = GetComponent<CharacterPath>().path;
		nextPoint = Mathf.RoundToInt(Random.Range(0f, path.Length - 1));
	}

	void Update () {
        int prevPoint = nextPoint - 1;
        if (prevPoint < 0)
        {
            prevPoint = path.Length - 1;
        }


        _isoTransform.Position = Vector3.MoveTowards(_isoTransform.Position, path[nextPoint], moveSpeed * Time.deltaTime);

        if (_isoTransform.Position == path[nextPoint]) {
			nextPoint = (nextPoint + 1) % path.Length;
		}
	}
}
