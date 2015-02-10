using UnityEngine;
using System.Collections;

public class GamePreferences : MonoBehaviour {
	
	public static string CurrentScore = "CurrentScore";	// static variable for saving highscore in players preffs (score is saved as int)
	
	public static string CurrentCoinScore = "CurrentCoinScore";	// static variable for saving coins in players preffs (coins are saved as int)
	
	public static string CurrentLifes = "CurrentLifes";	// variable to save game lifes when the player dies or the game is paused
	// lifes will be saved as an int
	
	public static string GameStartedFromMainMenu = "GameStartedFromMainMenu"; 	// static variable to check if the game is started from main menu
	// the state will be saved as an int(0 = false, 1 = true)
	
	public static string GameResumed = "GameResumed"; // static variable to check if the game is resumed after its being played	
	// the state will be saved as an int(0 = false, 1 = true)
	
	public static string GameResumedAfterPlayerDied = "GameResumedAfterPlayerDied"; // static variable to check if the game was resumed after the
	// player died. state will be saved as an int(0 = false, 1 = true)
	
	public static string EasyDifficulty = "EasyDifficulty"; // variable for easy difficulty
	
	public static string MediumDifficulty = "MediumDifficulty"; // variable for medium difficulty
	
	public static string HardDifficulty = "HardDifficulty"; // variable for hard difficulty
	
	public static string EasyDifficultyHighScore = "EasyDifficultyHighScore"; // variable to save highscore for easy difficulty
	
	public static string MediumDifficultyHighScore = "MediumDifficultyHighScore"; // variable to save highscore for medium difficulty
	
	public static string HardDifficultyHighScore = "HardDifficultyHighScore"; // variable to save highscore for hard difficulty
	
	public static string EasyDifficultyCoinScore = "EasyDifficultyCoinScore"; // variable to save coin score for easy difficulty
	
	public static string MediumDifficultyCoinScore = "MediumDifficultyCoinScore"; // variable to save coin score for medium difficulty
	
	public static string HardDifficultyCoinScore = "HardDifficultyCoinScore"; // variable to save coin score for hard difficulty
	
	public static string IsTheMusicOn = "IsTheMusicOn";	// variable to store music preference
	
	public static string CurrentCameraSpeed = "CurrentCameraSpeed"; // variable to keep track of the current camera speed when the player dies
	
	public static string MainMenuOpenedFromGameplay = "MainMenuOpenedFromGameplay"; // variable to check if the Main Menu is opened from 
	// gameplay scene
	
	public static string HowManyTimesTheGameWasPlayed = "HowManyTimesTheGameWasPlayed"; // track how many times the games has been played to
	// display ads
	
	public static string ShowBannerAfterGameplay = "ShowBannerAfterGameplay"; // track when the player goes to game play to turn off the banner 
	// and when he returns to show the banner
	
	// a method for getting the music state - On or Off
	public static int GetMusicState() {
		return PlayerPrefs.GetInt(GamePreferences.IsTheMusicOn);
	}
	
	// a method for setting the music state - On or Off
	public static void SetMusicState(int state) {
		PlayerPrefs.SetInt (GamePreferences.IsTheMusicOn, state);
	}
	
	// method for getting easy difficulty state
	public static int GetEasyDifficultyState() {
		return PlayerPrefs.GetInt(GamePreferences.EasyDifficulty);
	}
	
	// method for setting easy difficulty state
	public static void SetEasyDifficultyState(int state) {
		PlayerPrefs.SetInt (GamePreferences.EasyDifficulty, state);
	}
	
	// method for getting medium difficulty state
	public static int GetMediumDifficultyState() {
		return PlayerPrefs.GetInt(GamePreferences.MediumDifficulty);
	}
	
	// method for setting medium difficulty state
	public static void SetMediumDifficultyState(int state) {
		PlayerPrefs.SetInt (GamePreferences.MediumDifficulty, state);
	}
	
	// method for getting hard difficulty state
	public static int GetHardDifficultyState() {
		return PlayerPrefs.GetInt(GamePreferences.HardDifficulty);
	}
	
	// method for setting hard difficulty state
	public static void SetHardDifficultyState(int state) {
		PlayerPrefs.SetInt (GamePreferences.HardDifficulty, state);
	}
	
	// method for getting easy difficulty highscore
	public static int GetEasyDifficultyHighscore() {
		return PlayerPrefs.GetInt(GamePreferences.EasyDifficultyHighScore);
	}
	
	// method for setting easy difficulty highscore
	public static void SetEasyDifficultyHighscore(int score) {
		PlayerPrefs.SetInt (GamePreferences.EasyDifficultyHighScore, score);
	}
	
	// method for getting medium difficulty highscore
	public static int GetMediumDifficultyHighscore() {
		return PlayerPrefs.GetInt(GamePreferences.MediumDifficultyHighScore);
	}
	
	// method for setting medium difficulty highscore
	public static void SetMediumDifficultyHighscore(int score) {
		PlayerPrefs.SetInt (GamePreferences.MediumDifficultyHighScore, score);
	}
	
	// method for getting hard difficulty highscore
	public static int GetHardDifficultyHighscore() {
		return PlayerPrefs.GetInt(GamePreferences.HardDifficultyHighScore);
	}
	
	// method for setting hard difficulty highscore
	public static void SetHardDifficultyHighscore(int score) {
		PlayerPrefs.SetInt (GamePreferences.HardDifficultyHighScore, score);
	}
	
	// method for getting easy difficulty coin score
	public static int GetEasyDifficultyCoinScore() {
		return PlayerPrefs.GetInt(GamePreferences.EasyDifficultyCoinScore);
	}
	
	// method for setting easy difficulty coin score
	public static void SetEasyDifficultyCoinScore(int score) {
		PlayerPrefs.SetInt (GamePreferences.EasyDifficultyCoinScore, score);
	}
	
	// method for getting medium difficulty coin score
	public static int GetMediumDifficultyCoinScore() {
		return PlayerPrefs.GetInt(GamePreferences.MediumDifficultyCoinScore);
	}
	
	// method for setting medium difficulty coin score
	public static void SetMediumDifficultyCoinScore(int score) {
		PlayerPrefs.SetInt (GamePreferences.MediumDifficultyCoinScore, score);
	}
	
	// method for getting hard difficulty coin score
	public static int GetHardDifficultyCoinScore() {
		return PlayerPrefs.GetInt(GamePreferences.HardDifficultyCoinScore);
	}
	
	// method for setting hard difficulty coin score
	public static void SetHardDifficultyCoinScore(int score) {
		PlayerPrefs.SetInt (GamePreferences.HardDifficultyCoinScore, score);
	}
	
}

















































