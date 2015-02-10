using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreMenuController : MonoBehaviour {

	[SerializeField]
	private Text scoreText;

	[SerializeField]
	private Text coinText;


	// Use this for initialization
	void Start () {
		CheckDifficultyAndSetScore ();
		CheckDifficultyAndSetCoinScore ();
	}
	
	public void BackButton() {
		Application.LoadLevel ("Main Menu Scene");
	}

	void CheckDifficultyAndSetCoinScore() {
		
		int easyDifficulty = GamePreferences.GetEasyDifficultyState ();
		int mediumDifficulty = GamePreferences.GetMediumDifficultyState ();
		int hardDifficulty = GamePreferences.GetHardDifficultyState ();
		
		if (easyDifficulty == 1) {
			coinText.text = GamePreferences.GetEasyDifficultyCoinScore().ToString();	
		}
		
		if (mediumDifficulty == 1) {
			coinText.text = GamePreferences.GetMediumDifficultyCoinScore().ToString();	
		}
		
		if (hardDifficulty == 1) {
			coinText.text = GamePreferences.GetHardDifficultyCoinScore().ToString();
		}
		
	}

	void CheckDifficultyAndSetScore() {
		
		int easyDifficulty = GamePreferences.GetEasyDifficultyState ();
		int mediumDifficulty = GamePreferences.GetMediumDifficultyState ();
		int hardDifficulty = GamePreferences.GetHardDifficultyState ();
		
		if (easyDifficulty == 1) {
			scoreText.text = GamePreferences.GetEasyDifficultyHighscore().ToString();	
		}
		
		if (mediumDifficulty == 1) {
			scoreText.text = GamePreferences.GetMediumDifficultyHighscore().ToString();
		}
		
		if (hardDifficulty == 1) {
			scoreText.text = GamePreferences.GetHardDifficultyHighscore().ToString();
		}
		
	}

}


