using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    public bool gameStarted;

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


    [SerializeField]
    private GameObject _menu;


	void Start() {
        updateDamage(0);
        _menu = GameObject.Find("Menu");
	}

    public void StartGame() {
        gameStarted = true;
        _menu.SetActive(false);
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
		if (Input.GetKeyDown(KeyCode.R)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		if (lastCharacterInfected != null && !bombTriggered)
        {
            if (lastCharacterInfected.GetComponent<Character>().infectDuration < 0)
            {
                bombTriggered = true;
                Debug.Log("BOOOOOOOOOM!");

                GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("CharacterHitBox");
                foreach (GameObject gameObject in gameObjects) {
                    Character character = gameObject.GetComponent<Character>();
                    if (character != null && character.infected)
                    {
                        character.Explode();
                    }
                }
            }

        }
    }
}
