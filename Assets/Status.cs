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
            GameObject aoe = Instantiate(infectedPrefab, transform.parent) as GameObject;
            aoe.SetActive(true);
        }
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
                if (hit.transform == transform)
                {
                    GameState gameState = GameObject.Find("GameState").GetComponent("GameState") as GameState;
                    if (gameState == null)
                    {
                        Debug.Log("Cannot find GameState in scene");
                    }

                    if (gameState != null && !gameState.FirstPersonInfected)
                    {
                        gameState.SendMessage("InfectFirstPerson");
                        SendMessage("Infect");
                    }
                }
            }
        }
	}
}
