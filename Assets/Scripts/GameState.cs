﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour {

    public bool firstCharacterInfected;
	public Text text;

    public void InfectFirstCharacter() {
        firstCharacterInfected = true;
    }

    [SerializeField]
    private GameObject lastCharacterInfected;
    public void SetLastCharacterInfected(GameObject lastCharacterInfected) {
        this.lastCharacterInfected = lastCharacterInfected;
    }

	private int damage = 0;
    public bool bombTriggered = false;


	void Start() {
		updateDamage(0);
	}

	public void addDamage(int damage) {
		updateDamage(this.damage + damage);
	}

	void updateDamage(int damage) {
		this.damage = damage;
		if (text) {
			text.text = "Damage: $" + this.damage;
		}
	}

    void Update () {
        if (lastCharacterInfected != null)
        {
            if (lastCharacterInfected.GetComponent<Character>().infectDuration < 0)
            {
                if (!bombTriggered)
                {
                    bombTriggered = true;
                    Debug.Log("BOOOOOOOOOM!");
                }
            }

        }
    }
}
