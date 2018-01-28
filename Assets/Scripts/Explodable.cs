using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour {

	public Sprite[] sprites;

	private SpriteRenderer spriteRenderer;
	private int currentSprite = 0;
	private GameObject spriteGO;

	void Start () {
		spriteGO = new GameObject();
		spriteGO.transform.parent = this.transform;
		spriteGO.transform.localPosition = new Vector3(-0.5f, 0.5f, 0);
		spriteRenderer = spriteGO.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		spriteRenderer.sprite = sprites[currentSprite];
	}

	void Update() {
		if (Input.anyKeyDown) {
			CycleSprite();
		}
	}

	public void Explode() {
		SetSprite(1);
	}

	public void CycleSprite() {
		currentSprite = (currentSprite + 1) % sprites.Length;
		SetSprite(currentSprite);
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
