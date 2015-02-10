using System;
using UnityEngine;
using System.Collections;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

public class AdsScript : MonoBehaviour {
	
	private string bannerId = "ca-app-pub-4889382687991550/7486631429";
	private string interstitialId = "ca-app-pub-4889382687991550/7067829028";
	
	public BannerView bannerView;
	public InterstitialAd interstitial;
	
	// script instance for singleton
	private static AdsScript instance = null;
	
	// track how many times the game has been played
	private int HowManyTimesTheGameWasPlayed;
	
	private bool isTheGamePlaying;

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

	void Awake() {
		if(instance)
			DestroyImmediate(gameObject);	// delete duplicate
		else {
			instance = this;	//Make this object the only instance
			DontDestroyOnLoad(gameObject);	// dont destoy it when scenes load
		}
		
	}
	
	void Start() {

//		GetAnimators ();
//		AnimateButtonsSlideIn ();
		
		CheckHowManyTimesTheGameHasBeenPlayed ();
		
		CreateLoadAndShowBanner ();

	}

	void Update() {

	}
	
	void OnDestroy() {
		//	bannerView.Destroy ();
	}
	
	void OnApplicationQuit() {
		//	bannerView.Destroy ();
	}

	public int ReturnHowManyTimesTheGameWasPlayed() {
		return this.HowManyTimesTheGameWasPlayed;
	}
	
	void OnLevelWasLoaded(int level) {
		
		if (Application.loadedLevelName == "Main Menu Scene") {
			
			int hasThePlayerReturnedFromGamePlay = PlayerPrefs.GetInt(GamePreferences.ShowBannerAfterGameplay);

			// check if the player has returned from gameplay
			if(hasThePlayerReturnedFromGamePlay == 1) {

				// if the game has been opened 5 times
				if(HowManyTimesTheGameWasPlayed == 5) {

					// show the interstitials if its created, if its not created create and interstitial
					if(interstitial != null) {
						StartCoroutine(WaitToShowAd(0.1f));
						StartCoroutine(DestroyInterstitialAfterShowing());
					}  else {
						CreateAndLoadInterstitial();
					}
					
				}  // if the game has been opened 5 times

				// if the banner is null create the banner again
				if(bannerView == null) {
					CreateLoadAndShowBanner();
				}
				
				PlayerPrefs.SetInt(GamePreferences.ShowBannerAfterGameplay, 0);
				
			}  // if the player returned from gameplay
			
		}

		// if the game has been loaded
		if (Application.loadedLevelName == "Gameplay Scene") {

			// remove the banner if its active
			if(bannerView != null) {
				bannerView.Hide();
				bannerView.Destroy();
				bannerView = null;
			}

			PlayerPrefs.SetInt(GamePreferences.ShowBannerAfterGameplay, 1);
		}
		

	}
	
	void CreateLoadAndShowBanner() {
		// create banner
		bannerView = new BannerView (bannerId, new AdSize(250, 50), AdPosition.Top);
		
		// load add
		bannerView.LoadAd (createAdRequest ());
		
		// handle ad loaded event
		bannerView.AdLoaded += HandleBannerAdLoaded;
	//	bannerView.AdFailedToLoad += HandleBannerAdFailedToLoad;
	}

	void HandleBannerAdFailedToLoad (object sender, AdFailedToLoadEventArgs e)
	{
		//reload the banner
		
		// create banner
		bannerView = new BannerView (bannerId, new AdSize(250, 50), AdPosition.Top);
		
		// load add
		bannerView.LoadAd (createAdRequest ());
	}
	
	// handle add loaded event
	void HandleBannerAdLoaded (object sender, EventArgs e)
	{
		if (Application.loadedLevelName == "Gameplay Scene") {
			
			bannerView.Hide();
			bannerView.Destroy();
			bannerView = null;
			
		}  else {

			if(bannerView != null) {
				bannerView.Show ();
			}

		}
	}

	void CheckHowManyTimesTheGameHasBeenPlayed() {
		
		HowManyTimesTheGameWasPlayed = PlayerPrefs.GetInt (GamePreferences.HowManyTimesTheGameWasPlayed);
		HowManyTimesTheGameWasPlayed++;
		
		if (HowManyTimesTheGameWasPlayed > 5) {
			HowManyTimesTheGameWasPlayed = 5;
		}
		
		PlayerPrefs.SetInt (GamePreferences.HowManyTimesTheGameWasPlayed, HowManyTimesTheGameWasPlayed);
	}
	
	IEnumerator WaitToShowAd(float time) {
		yield return new WaitForSeconds (time);
		ShowInterstitial();
	}
	
	IEnumerator DestroyInterstitialAfterShowing() {
		yield return new WaitForSeconds (3);
		interstitial.Destroy ();
		interstitial = null;
	}
	
	public void CreateAndLoadInterstitial() {
		interstitial = new InterstitialAd (interstitialId);
		interstitial.LoadAd (createAdRequest ());
	}
	
	private void ShowInterstitial()
	{
		if (interstitial.IsLoaded())
		{
			interstitial.Show();
			PlayerPrefs.SetInt(GamePreferences.HowManyTimesTheGameWasPlayed, 0);
			HowManyTimesTheGameWasPlayed = 0;
		}
		else
		{
			//interstitial.AdLoaded += HandleInterstitialAdLoaded;
		}
	}
	
	void HandleInterstitialAdLoaded (object sender, EventArgs e)
	{
		interstitial.Show ();
	}
	
	// Returns an ad request with custom ad targeting.
	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder()
			.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
				.AddKeyword("game")
				.SetGender(Gender.Male)
				.SetBirthday(new DateTime(1985, 1, 1))
				.TagForChildDirectedTreatment(false)
				.AddExtra("color_bg", "9B30FF")
				.Build();
		
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