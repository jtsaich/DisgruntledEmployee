﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

    public bool firstCharacterInfected;

    public void InfectFirstCharacter() {
        firstCharacterInfected = true;
    }

    [SerializeField]
    private GameObject lastCharacterInfected;
    public void SetLastCharacterInfected(GameObject lastCharacterInfected) {
        this.lastCharacterInfected = lastCharacterInfected;
    }


    public bool bombTriggered = false;

    void Update () {
        if (lastCharacterInfected != null)
        {
            if (lastCharacterInfected.GetComponent<Character>().infectDuration < 0)
            {
                bombTriggered = true;
            }

        }
    }
}
