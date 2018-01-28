using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;

public class Character: MonoBehaviour {
	public GameObject AreaOfEffectPrefab;
    public GameObject BlastPrefab;
	public GameObject ExplosionPrefab;
    public GameObject InfectedSignPrefab;

	public float moveSpeed = 2f;
    public float infectDuration = 10f;
	public float infectRadius = 5f;
	public bool infected = false;

	private int nextPoint = 0;
	private Vector3[] path;
    private IsoTransform _isoTransform;
    private GameState _gameState;
	private SoundManager soundManager;
	private GameObject aoeGO = null;
	private AreaOfEffect aoeScript;
    private Blast _blast;
    private GameObject blastGO;
    private GameObject infectedSign;

    private bool startDelayExplode = false;
    private float delayExplode = 0;

    [SerializeField]
    private int _direction;

    private Animator animator;

    void Awake() {
        _isoTransform = this.GetOrAddComponent<IsoTransform>();
        _gameState = GameObject.Find("GameState").GetComponent<GameState>();
		soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

	void Start() {
        animator = this.GetComponent<Animator>();

        path = GetComponentInParent<CharacterPath>().path;

		blastGO = Instantiate(BlastPrefab, transform);
		_blast = blastGO.AddComponent(typeof(Blast)) as Blast;
		blastGO.SetActive(false);
		//nextPoint = Mathf.RoundToInt(Random.Range(0f, path.Length - 1));
		_isoTransform.Position = path[nextPoint];


		if (infected) {
			this.Infect(true);
		}
	}

	void Update () {
        if (startDelayExplode)
        {
            delayExplode -= Time.deltaTime;
            if (delayExplode <= 0)
            {
                Explode();
                return;
            }
        }

		if (Input.GetKeyDown(KeyCode.I)) {
			Infect(true);
		}

        _isoTransform.Position = Vector3.MoveTowards(_isoTransform.Position, path[nextPoint], moveSpeed * Time.deltaTime);
        if (_isoTransform.Position == path[nextPoint]) {
            Vector3 direction = path[(nextPoint + 1) % path.Length] - path[nextPoint];
            if (direction.x < 0)
            {
                _direction = 0;
                animator.SetInteger("Direction", 0);
            }
            else if (direction.z < 0)
            {
                _direction = 3;
                animator.SetInteger("Direction", 3);
            }
            else if (direction.x > 0)
            {
                _direction = 2;
                animator.SetInteger("Direction", 2);
            }
            else if (direction.z > 0)
            {
                _direction = 1;
                animator.SetInteger("Direction", 1);
            }


			nextPoint = (nextPoint + 1) % path.Length;
        }

        if (infected && infectDuration > 0)
        {
            infectDuration -= Time.deltaTime;
        }

        if (_gameState.gameStarted && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit " + hit.transform.tag);
                if (hit.transform.tag == "CharacterHitBox" && hit.transform == transform)
                {
                    if (_gameState == null)
                    {
                        Debug.Log("Cannot find GameState in scene");
                    }

                    if (_gameState != null && !_gameState.firstCharacterInfected)
                    {
                        _gameState.SendMessage("InfectFirstCharacter");
//                        SendMessage("Infect", false);
                        Infect();
                    }
                }
            }
        }
	}

	public void Infect(bool force=false) {
		if (force || !infected) {
            infectedSign = Instantiate(InfectedSignPrefab, transform);


			infected = true;
			blastGO.SetActive(true);
            aoeGO = Instantiate(AreaOfEffectPrefab, transform);
			aoeScript = aoeGO.GetComponent<AreaOfEffect>();
            aoeScript.Initialize(infectRadius, infectDuration);

            if (_gameState != null)
            {
                _gameState.SendMessage("SetLastCharacterInfected", gameObject);
            }
		}
	}

    public void DelayExplode(float time) {
        startDelayExplode = true;
        delayExplode = time;
    }

    public void Explode() {
        if (infected)
        {
            foreach (GameObject gameObject in _blast.withinRadius) {
                gameObject.GetComponent<Explodable>().Explode();
            }

			GameObject go = Instantiate(ExplosionPrefab);
			go.transform.position = transform.position;
				
			if (soundManager) {
				soundManager.playExplosion();
			}
			Destroy(this.gameObject);
        }
    }
        

	void OnDestroy() {
		if (aoeGO != null) {
			Destroy(aoeGO);
			aoeGO = null;
		}
		if (blastGO) {
			Destroy(blastGO);
			blastGO = null;
		}

        if (infectedSign)
        {
            Destroy(infectedSign);
            infectedSign = null;
        }
	}
}
