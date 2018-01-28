using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject musicPrefab;
	public GameObject infectionPrefab;
  //  public GameObject ambientPrefab;

	private GameObject[] explosionGOs;
	private AudioSource[] explosionSources;
	private int numExplosions = 5;
	private int nextExplosion = 0;

	private GameObject[] infectionGOs;
	private AudioSource[] infectionSources;
	private int numInfections = 5;
	private int nextInfection = 0;

	private GameObject musicGO;
	private AudioSource musicSource;

	void Start() {
		infectionGOs = new GameObject[numInfections];
		infectionSources = new AudioSource[numInfections];

		for (int i = 0; i < numInfections; i++) {
			infectionGOs[i] = Instantiate(infectionPrefab, transform);
			infectionSources[i] = infectionGOs[i].GetComponent<AudioSource>();
		}

		explosionGOs = new GameObject[numExplosions];
		explosionSources = new AudioSource[numExplosions];

		for (int i = 0; i < numExplosions; i++) {
			explosionGOs[i] = Instantiate(explosionPrefab, transform);
			explosionSources[i] = explosionGOs[i].GetComponent<AudioSource>();
		}

		musicGO = Instantiate(musicPrefab, transform);
       // musicGO = Instantiate(ambientPrefab, transform);
    }

	public void playExplosion() {
		explosionSources[nextExplosion].pitch = Random.Range(0.9f, 1.1f);
		explosionSources[nextExplosion].Play();
		nextExplosion = (nextExplosion + 1) % numExplosions;
	}

	public void playInfection() {
		infectionSources[nextInfection].pitch = Random.Range(0.9f, 1.1f);
		infectionSources[nextInfection].Play();
		nextInfection = (nextInfection + 1) % numInfections;
	}

	void onDestroy() {
		if (musicGO) {
			Destroy(musicGO);
		}
		if (explosionGOs != null) {
			for (int i = 0; i < explosionGOs.Length; i++) {
				Destroy(explosionGOs[i]);
			}
		}
	}
}
