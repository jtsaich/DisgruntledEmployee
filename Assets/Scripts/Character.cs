using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;

public class Character: MonoBehaviour {
	public GameObject AreaOfEffectPrefab;
	public float moveSpeed = 2f;
	public float infectRadius = 5f;
	public float infectShrink = 0.01f;
	public bool infected = false;

	private int nextPoint = 0;
	private Vector3[] path;
    private IsoTransform _isoTransform;
	private GameObject aoeGO = null;
	private AreaOfEffect aoeScript;

    void Awake() {
        _isoTransform = this.GetOrAddComponent<IsoTransform>();
    }

	void Start() {
		path = GetComponentInParent<CharacterPath>().path;
		//nextPoint = Mathf.RoundToInt(Random.Range(0f, path.Length - 1));
		_isoTransform.Position = path[nextPoint];

		if (infected) {
			this.Infect(true);
		}
	}

	void Update () {
        _isoTransform.Position = Vector3.MoveTowards(_isoTransform.Position, path[nextPoint], moveSpeed * Time.deltaTime);

        if (_isoTransform.Position == path[nextPoint]) {
			nextPoint = (nextPoint + 1) % path.Length;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit " + hit.transform.tag);
                if (hit.transform.tag == "CharacterHitBox" && hit.transform == transform)
                {
                    GameState gameState = GameObject.Find("GameState").GetComponent("GameState") as GameState;
                    if (gameState == null)
                    {
                        Debug.Log("Cannot find GameState in scene");
                    }

                    if (gameState != null && !gameState.firstPersonInfected)
                    {
                        gameState.SendMessage("InfectFirstPerson");
                        SendMessage("Infect", false);
                    }
                }
            }
        }
	}

	public void Infect(bool force=false) {
		if (force || !infected) {
			infected = true;
			aoeGO = GameObject.Instantiate(AreaOfEffectPrefab);
			aoeScript = aoeGO.GetComponent<AreaOfEffect>();
			aoeScript.Initialize(infectRadius, infectShrink);
			aoeGO.transform.parent = this.transform;
			aoeGO.transform.localPosition = new Vector3(0, -0.5f, 0);
		}
	}

	void OnDestroy() {
		if (aoeGO != null) {
			Destroy(aoeGO);
			aoeGO = null;
		}
	}
}
