using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public bool firstPersonInfected = false;
    public bool FirstPersonInfected
    {
        get;
    }

    public void InfectFirstPerson() {
        firstPersonInfected = true;
    }
}
