using UnityEngine;
using System.Collections;

public class GamePlayFadeScript : MonoBehaviour {

	[SerializeField]
	private Texture2D fadeTexture;

	private float fadeSpeed = 1.0f;
	private float alpha = 0.0f;
	private int fadeDirection = -1;

	void OnGUI() {

		alpha += fadeDirection * fadeSpeed * Time.deltaTime;

		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeTexture);


	}

	public float BeginFade(int direction) {
		fadeDirection = direction;
		return fadeSpeed;
	}
}
