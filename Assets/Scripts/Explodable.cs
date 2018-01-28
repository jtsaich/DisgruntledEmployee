using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;

public class Explodable : MonoBehaviour {

	public Sprite[] sprites;
	public Vector3 spriteOffset;

	private SpriteRenderer spriteRenderer;
	private int currentSprite = 0;
	private GameObject spriteGO;
	private IsoTransform isoTransform;

	void Awake () {
		spriteGO = new GameObject();
		spriteGO.transform.parent = this.transform;
		spriteGO.transform.localPosition = spriteOffset;//new Vector3(-0.5f, 0.5f, 0);
		spriteRenderer = spriteGO.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		spriteRenderer.sprite = sprites[currentSprite];
		spriteRenderer.sortingOrder = 1;
	}

	void Update() {
		if (Input.anyKeyDown) {
			SetSprite(0);
		}
	}

	public void Explode() {
		SetSprite(1);
	}

	public void SetSprite(int index) {
		currentSprite = index;
		spriteRenderer.sprite = sprites[index];
	}

	void OnDestroy() {
		if (spriteGO != null) {
			Destroy(spriteGO);
		}
	}
}
