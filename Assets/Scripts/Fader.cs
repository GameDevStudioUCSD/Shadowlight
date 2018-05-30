using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour {

	public float fadeSpeed = 0.05f;

	private Image[] images; 
	private Text[] texts;
	private float alphaValue = 1.0f;

	private enum State {Normal, FadingIn, FadingOut};
	private State state = State.Normal;

	private void Start () {
		images = GetComponentsInChildren<Image> ();
		texts = GetComponentsInChildren<Text> ();
		state = State.Normal;
	}

	private void Update () {
		switch (state) {
		case State.FadingIn:
			if (alphaValue < 1.0f) {
				alphaValue += fadeSpeed;
				if (alphaValue > 1.0f) {
					alphaValue = 1.0f;
				}
			} else {
				state = State.Normal;
			}
			foreach (var image in images) {
				Color theColor = image.color;
				theColor.a = alphaValue;
				image.color = theColor;
			}
			foreach (var text in texts) {
				Color theColor = text.color;
				theColor.a = alphaValue;
				text.color = theColor;
			}
			break;

		case State.FadingOut:
			if (alphaValue > 0.0f) {
				alphaValue -= fadeSpeed;
				if (alphaValue < 0.0f) {
					alphaValue = 0.0f;
				}
			} else {
				state = State.Normal;
			}
			foreach (var image in images) {
				Color theColor = image.color;
				theColor.a = alphaValue;
				image.color = theColor;
			}
			foreach (var text in texts) {
				Color theColor = text.color;
				theColor.a = alphaValue;
				text.color = theColor;
			}
			break;

		case State.Normal:
			break;

		default:
			break;
		}
	}

	public void Hide() {
		alphaValue = 0.0f;
		foreach (var image in images) {
			Color theColor = image.color;
			theColor.a = alphaValue;
			image.color = theColor;
		}
		foreach (var text in texts) {
			Color theColor = text.color;
			theColor.a = alphaValue;
			text.color = theColor;
		}
	}

	public void FadeIn() {
		state = State.FadingIn;

	}

	public void FadeOut() {
		state = State.FadingOut;
	}
}
