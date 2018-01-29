using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

	private static bool quickStart = false;

    public bool gameStarted;
	public GameObject instructions;
	public GameObject[] instructionPanes;
	public GameObject shopperMessage;

    public bool firstCharacterInfected;
	public Text text;
    public GameObject clickToReplay;
    public GameObject finalScores;
    public Font font;

    public void InfectFirstCharacter() {
		shopperMessage.SetActive(false);
        firstCharacterInfected = true;
    }

    [SerializeField]
    private GameObject lastCharacterInfected;
    public void SetLastCharacterInfected(GameObject lastCharacterInfected) {
        this.lastCharacterInfected = lastCharacterInfected;
    }

    [SerializeField]
	private int damage = 0;

    public List<GameObject> damagedObjects = new List<GameObject>();

    [SerializeField]
    private bool bombTriggered = false;

    public float endGameAfterBombTriggered = 3.0f;



    [SerializeField]
    private GameObject _menu;

	void Start() {
        updateDamage(0);
        _menu = GameObject.Find("Menu");
		if (GameState.quickStart) {
			StartGame();
		}
	}

    public void StartGame() {
        gameStarted = true;
		shopperMessage.SetActive(true);
        _menu.SetActive(false);
    }

	public void ShowInstructionsPane(int pane = 0) {
		instructions.SetActive(true);

		for (int i = 0; i < instructionPanes.Length; i++) {
			instructionPanes[i].SetActive(false);
		}
		instructionPanes[pane].SetActive(true);
	}

	public void CloseInstructions() {
		instructions.SetActive(false);
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


        if (endGameAfterBombTriggered <= 0)
        {
            if (!finalScores.activeSelf)
            {
                ShowScore();
            }

            clickToReplay.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                ResetScene(true);
            }
        }

        if (bombTriggered && endGameAfterBombTriggered > 0)
        {
            endGameAfterBombTriggered -= Time.deltaTime;
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

	public void ResetScene(bool quickStart=false) {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		GameState.quickStart = quickStart;
    }

    public void ShowScore() {
        finalScores.SetActive(true);
       
        IDictionary<Sprite, int> dict = new Dictionary<Sprite, int>();
        foreach (GameObject gameObject in damagedObjects) {
            Explodable e = gameObject.GetComponent<Explodable>();

            if (e != null)
            {
                Sprite sprite = e.sprites[1];
                if (!dict.ContainsKey(sprite))
                {
                    dict[sprite] = e.worth;
                }
                else
                {
                    dict[sprite] = dict[sprite] + e.worth;
                }


            }
        }


        float x = -300;
        float y = 120;

        float dx = 200;
        float dy = -70;


        int i = 0;
        foreach (KeyValuePair<Sprite, int> entry in dict)
        {
            if (i > 3)
            {
                i = 0;
                y = 120;
                x += dx;
            }

            GameObject scoreItem = new GameObject();
            scoreItem.transform.parent = finalScores.transform;
//            scoreItem.transform.localPosition = 
//            scoreItem.transform.localScale = new Vector3(3, 3, 1);
            RectTransform trans = scoreItem.AddComponent<RectTransform>();
            trans.localPosition = new Vector3(x, y, 0);

            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40);
            trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 40);
            Image image = scoreItem.AddComponent<Image>();
            image.sprite = entry.Key;

            GameObject text = new GameObject();
            RectTransform txtTrans = text.AddComponent<RectTransform>();
            txtTrans.parent = scoreItem.transform;
            txtTrans.localPosition = new Vector3(90, 0, 0);
            txtTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 40);
            txtTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 120);

            Text t = text.AddComponent<Text>();
            t.text = "$" + entry.Value;
            t.font = font;
            t.fontSize = 20;
            t.color = Color.black;
            t.alignment = TextAnchor.MiddleLeft;

            y += dy;

            i++;
        }
    }
}
