using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public bool infected;
    public GameObject infectedPrefab;


    public void Infect() {
        if (!infected)
        {
            infected = true;
            GameObject infectedGameObject = Instantiate(infectedPrefab, transform) as GameObject;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("hit " + hit.transform.tag);
                if (hit.transform.tag == "Person")
                {
                    GameState gameState = GameObject.Find("GameState").GetComponent("GameState");
                    if (gameState == null)
                    {
                        Debug.Log("Cannot find GameState in scene");
                    }

                    if (gameState != null && gameState.FirstPersonInfected)
                    {
                        GameObject.Find("GameState").GetComponent("GameState").SendMessage("InfectFirstPerson");
                        SendMessage("Infect");
                    }
                }
            }
        }
	}
}
