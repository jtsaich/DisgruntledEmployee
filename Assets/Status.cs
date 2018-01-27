using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    public bool infected;
    public GameObject infectedPrefab;

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Person")
                {
                    infected = true;
                    GameObject infectedGameObject = Instantiate(infectedPrefab, transform) as GameObject;
                }
            }
        }
	}
}
