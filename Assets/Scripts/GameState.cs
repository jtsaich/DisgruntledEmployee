using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public bool firstPersonInfected;

    [SerializeField]
    private GameObject lastPersonInfected;
    public GameObject LastPersonInfected { get; set; }

    public void InfectFirstPerson() {
        firstPersonInfected = true;
    }

    public bool bombTriggered = false;

    public void TriggerBomb() {
        bombTriggered = true;
    }
}
