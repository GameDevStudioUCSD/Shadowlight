﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneControl : MonoBehaviour {

	public List<GameObject> bookContents;
	private List<Fader> bookContentFaders;
	private int pageNumber;
	public Animator bookFrameAnim;


	//private List<GameObject> bookContents;

	// Use this for initialization
	void Start () {
		bookContentFaders = new List<Fader> ();
		// first image
		pageNumber = 0;
		// fade out everything else
		foreach (var bookContent in bookContents) {
			Fader fader = bookContent.GetComponent<Fader> ();
			bookContentFaders.Add (fader);
			fader.Hide ();
		}
		Fader firstFader = bookContentFaders[0];
		firstFader.FadeIn ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Next")) {
			if (pageNumber < 6) {
				StartCoroutine(TurnPage ());
			} else {
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}
	}

	private IEnumerator TurnPage() {
		// fade out current image
		Fader theFader = bookContentFaders[pageNumber];
		theFader.FadeOut ();
		yield return new WaitForSeconds (0.5f);

		// turn page
		pageNumber++;
		bookFrameAnim.SetTrigger ("flip");
		yield return new WaitForSeconds (1f);


		// fade in new image
		bookContentFaders[pageNumber].FadeIn();
	}
}