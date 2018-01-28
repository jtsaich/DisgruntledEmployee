using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;

public class Explodable : MonoBehaviour {

	public Sprite[] sprites;
	public int worth;

	private SpriteRenderer spriteRenderer;
	private int currentSprite = 0;
	private GameObject spriteGO;
	private IsoTransform isoTransform;
	private GameState _gameState;

	void Awake () {
		spriteGO = new GameObject();
		spriteGO.transform.parent = this.transform;
		spriteGO.transform.localPosition = Vector3.zero;
		spriteRenderer = spriteGO.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		spriteRenderer.sprite = sprites[currentSprite];
		spriteRenderer.sortingOrder = 1;
		_gameState = GameObject.Find("GameState").GetComponent<GameState>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.F)) {
			SetSprite(0);
		}
	}

	public void Explode() {
		if (currentSprite != 1) {
			SetSprite(1);
			if (_gameState) {
				_gameState.addDamage(worth);
			}
		}
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
