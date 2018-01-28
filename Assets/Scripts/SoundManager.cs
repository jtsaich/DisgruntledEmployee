using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public GameObject explosionPrefab;
	public GameObject musicPrefab;
  //  public GameObject ambientPrefab;

	private GameObject[] explosionGOs;
	private AudioSource[] explosionSources;
	private int numExplosions = 5;
	private int nextExplosion = 0;

	private GameObject musicGO;
	private AudioSource musicSource;

	void Start() {
		
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
		explosionSources[nextExplosion].pitch = Random.Range(0.95f, 1.05f);
		explosionSources[nextExplosion].Play();
		nextExplosion = (nextExplosion + 1) % numExplosions;
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
