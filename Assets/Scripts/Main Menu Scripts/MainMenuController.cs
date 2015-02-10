using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private GameObject musicOn; // music on button

	[SerializeField]
	private GameObject musicOff; // music off button

	public AudioClip touchClip; // click clip
	AudioSource audioSource; // BGMusic audio source

	private AdsScript ads;

	[SerializeField]
	private GameObject startBtn;
	
	private Animator startBtnAnimator;
	
	[SerializeField]
	private GameObject highscoreBtn;
	
	private Animator highscoreAnimator;
	
	[SerializeField]
	private GameObject optionsBtn;
	
	private Animator optionsAnimator;
	
	[SerializeField]
	private GameObject quitBtn;
	
	private Animator quitBtnAnimator;

	[SerializeField]
	private GameObject musicOnBtn;
	
	private Animator musicOnBtnAnimation;

	[SerializeField]
	private GameObject musicOffBtn;
	
	private Animator musicOffBtnAnimation;

	// Use this for initialization
	void Start () {

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		ads = GameObject.FindGameObjectWithTag ("Ads").GetComponent<AdsScript> ();

		audioSource = GameObject.FindGameObjectWithTag ("BGMusic").GetComponent<AudioSource> ();
		
		IsTheGamePlayedForTheFirstTime ();
		CheckMusic ();

		GetAnimators ();
		AnimateButtonsSlideIn ();

	}

	public void StartGameButton() {

		if(ads.bannerView != null) {
			ads.bannerView.Hide();
			ads.bannerView.Destroy();
			ads.bannerView = null;
		}
		
		if(ads.ReturnHowManyTimesTheGameWasPlayed() == 5) {
			
			if(ads.interstitial != null) {
				ads.interstitial.Destroy();
				ads.interstitial = null;
				
				ads.CreateAndLoadInterstitial ();
				
			}  else {
				
				ads.CreateAndLoadInterstitial ();
				
			}
			
		}
			
			PlayerPrefs.SetInt(GamePreferences.GameStartedFromMainMenu, 1);
			Application.LoadLevel("Gameplay Scene");
	}

	public void HighscoreButton() {
		if(!audioSource.isPlaying) {
			AudioSource.PlayClipAtPoint(touchClip, transform.position);
		}
		
		Application.LoadLevel ("Highscore Scene");
	}

	public void OptionsButton() {
		if(!audioSource.isPlaying) {
			AudioSource.PlayClipAtPoint(touchClip, transform.position);
		}
		
		Application.LoadLevel ("Options Scene");
	}

	public void QuitButton() {
		Application.Quit();
	}
	
	void IsTheGamePlayedForTheFirstTime() {
		
		if(!PlayerPrefs.HasKey("GamePlayed")) {
			
			// set the default difficulty medium (0 means false , 1 means true)
			GamePreferences.SetEasyDifficultyState(0);
			GamePreferences.SetMediumDifficultyState(1);
			GamePreferences.SetHardDifficultyState(0);
			
			// set the default highscore and coin score 0 for every difficulty
			GamePreferences.SetEasyDifficultyHighscore(0);
			GamePreferences.SetMediumDifficultyHighscore(0);
			GamePreferences.SetHardDifficultyHighscore(0);
			
			GamePreferences.SetEasyDifficultyCoinScore(0);
			GamePreferences.SetMediumDifficultyCoinScore(0);
			GamePreferences.SetHardDifficultyCoinScore(0);
			
			// the music is off by default
			GamePreferences.SetMusicState(0);
			
			
			// set the GamePlay arbitrary value to 1, which means next time when we check if this key exist it will be true
			// which means that the game has been opened and we dont need to set up our default values anymore
			PlayerPrefs.SetInt("GamePlayed", 1);
		}
		
	} // is the game played for the first time
	
	void CheckMusic() {
		
		// get the player prefs is the music on value
		int playMusic = GamePreferences.GetMusicState ();
		
		// if the value is 0 meaning false, check if the music is playing if its playing stop it, and deactivate MusicOn prefab button
		if (playMusic == 0) {
			
			if(audioSource.isPlaying) {
				audioSource.loop = false;
				audioSource.Stop();
				musicOn.SetActive(false);
			} else {
				musicOff.SetActive(true);
				musicOn.SetActive(false);
			}
			
		} else if(playMusic == 1) {
			
			// if play music is 1 meaning true, check if the audio source is not playing then play the music and deactivate musicOff prefab button 
			if(!audioSource.isPlaying) {
				audioSource.loop = true;
				audioSource.Play();
				musicOff.SetActive(false);
			} else {
				musicOff.SetActive(false);
				musicOn.SetActive(true);
			}
			
		}
		
	} // check music
	
	public void TurnMusicOn() {
		
		if(!audioSource.isPlaying) {
			audioSource.loop = true;
			audioSource.Play();
			musicOff.SetActive(false);
			musicOn.SetActive(true);
		}
		
		GamePreferences.SetMusicState (1);
		
	}
	
	public void TurnMusicOff() {
		
		if(audioSource.isPlaying) {
			audioSource.loop = false;
			audioSource.Stop();
			musicOn.SetActive(false);
			musicOff.SetActive(true);
		}
		
		GamePreferences.SetMusicState (0);
	}

	void GetAnimators() {
		startBtnAnimator = startBtn.GetComponent<Animator> ();
		highscoreAnimator = highscoreBtn.GetComponent<Animator> ();
		optionsAnimator = optionsBtn.GetComponent<Animator> ();
		quitBtnAnimator = quitBtn.GetComponent<Animator> ();
		musicOnBtnAnimation = musicOnBtn.GetComponent<Animator> ();
		musicOffBtnAnimation = musicOffBtn.GetComponent<Animator> ();
	}
	
	void AnimateButtonsSlideIn() {
		startBtnAnimator.Play ("StartBtnSlideIn");
		highscoreAnimator.Play ("HighscoreBtnSlideIn");
		optionsAnimator.Play ("OptionsBtnSlideIn");
		quitBtnAnimator.Play ("QuitBtnSlideIn");
		musicOnBtnAnimation.Play ("MusicOnSlideIn");
		musicOffBtnAnimation.Play ("MusicOffSlideIn");
	}

} // class
