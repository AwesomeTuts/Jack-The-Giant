using UnityEngine;
using System.Collections;

public class MainMenuFadeScript : MonoBehaviour {

	public Texture2D fadeOutTexture; // the texture that will overlay the screen
	public float fadeSpeed = 2.0f;  // the fading speed

	private float alpha = 1.0f;
	private int fadeDirection = -1;
	private bool fade;
	
	void Start() {
		
		int MainMenuFromGamePlay = PlayerPrefs.GetInt (GamePreferences.MainMenuOpenedFromGameplay);
		
		if (MainMenuFromGamePlay == 1) {
			fade = true;
			PlayerPrefs.SetInt(GamePreferences.MainMenuOpenedFromGameplay, 0);
		}  else {
			fade = false;
		}
		
	}
	
	void OnGUI() {
		
		if (fade) {
			
			//fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation in seconds
			alpha += fadeDirection * fadeSpeed * Time.deltaTime;
			
			// force (clamp) the number between 0 and 1 because GUI.Color uses alpha values between 0 and 1
			alpha = Mathf.Clamp01 (alpha);
			
			// set the color of GUI (in this case our texture). All color values remains the same & the Alpha is set to the alpha variable
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);		// set the alpha value

			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture); 	// draw the texture to fit the entire screen area
			
		}
	
	}
	
	// sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
	public float BeginFade(int direction) {
		fadeDirection = direction;
		return (fadeSpeed);		// return the fadeSpeed variable so its easy to time the ApplicationLoadLevel();
	}
	


}
