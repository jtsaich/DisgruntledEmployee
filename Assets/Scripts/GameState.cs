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

    [SerializeField]
	private int damage = 0;

    [SerializeField]
    private bool bombTriggered = false;

    public float endGameAfterBombTriggered = 3.0f;



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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

		if (Input.GetKeyDown(KeyCode.R)) {
            ResetScene();
		}

        if (bombTriggered)
        {
            endGameAfterBombTriggered -= Time.deltaTime;
            if (endGameAfterBombTriggered <= 0)
            {
                ResetScene();
            }
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
                        character.DelayExplode(Random.Range(0f, 1f));
                    }
                }
            }

        }
    }

    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
