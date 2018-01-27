using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;

public class Movement : MonoBehaviour {

    public GameObject characterPath;
	public float moveSpeed = 5f;
	public int nextPoint = 0;

    private IsoTransform _isoTransform;
    private CharacterPath _characterPath;

    void Awake() {
        _isoTransform = this.GetOrAddComponent<IsoTransform>(); //avoids polling the IsoTransform component per frame
    }

	void Start() {
        nextPoint = Mathf.RoundToInt(Random.Range(0f, _characterPath.path.Length - 1));
	}

	void Update () {
        if (_characterPath == null)
        {
            _characterPath = characterPath.GetComponent<CharacterPath>();
        }

        int prevPoint = nextPoint - 1;
        if (prevPoint < 0)
        {
            prevPoint = _characterPath.path.Length - 1;
        }


        _isoTransform.Position = Vector3.MoveTowards(_isoTransform.Position, _characterPath.path[nextPoint], moveSpeed * Time.deltaTime);

        if (_isoTransform.Position == _characterPath.path[nextPoint]) {
            nextPoint = (nextPoint + 1) % _characterPath.path.Length;
		}
	}
}
