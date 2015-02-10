using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 8.0f;	// the speed by which the player moves
	public float maxVelocity = 3.0f;	// maximum velocity of the player
	
	public Vector3 boundaries; // player boundaries

	public AudioClip coinClip;	// coin sound
	public AudioClip lifeClip;	// life sound
	
	public static int lifeCount;	// life counter
	public static int coinCount;	// coin counter
	public static int scoreCount;	// score counter

	public bool countPoints;	// boolean value to control the point count, initialy it is set to true but when the player dies it will be
	// set to false to prevent score count while the player is dead
	
	public Vector3 lastPosition; // players last position for score count, when the players new position is less that this position 
	// score will increment

	private CameraScript cameraScript;	// referenpublic float speed = 8.0f;	// the speed by which the player moves
	
	private Animator animator;	// players animator for animation controlce to the camera script

	[SerializeField]
	private GameObject fader;	// fader game object reference

	private GamePlayFadeScript fadeScript; // main menu fade script

	private int countTouches; // counting our tuches, after 3rd touch we will count score

	[SerializeField]
	private GameObject ready;

	public static bool isTheGameStartedFromBegining; // a boolean to control the touching in our LateUpdate so it will not interfere with our

	private int easyDifficulty;		
	private int mediumDifficulty;
	private int hardDifficulty;

	[SerializeField]
	private GameObject endScoreBG;

	[SerializeField]
	private Text endScoreText;

	Vector3 endScoreTextPosition;

	[SerializeField]
	private Text endCoinText;

	Vector3 endCoinScorePosition;

	private AdsScript adScript;

	// Use this for initialization
	void Start () {

		endScoreBG.SetActive (false);

		endScoreTextPosition = endScoreText.rectTransform.localPosition;
		endCoinScorePosition = endCoinText.rectTransform.localPosition;

		endScoreText.rectTransform.localPosition = new Vector3 (300, 300, 5);
		endCoinText.rectTransform.localPosition = new Vector3(300, 300, 5);


		isTheGameStartedFromBegining = true;

		animator = GetComponent<Animator> ();	// getting the animator reference
		
		boundaries = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0)); // getting player boundaries

		countTouches = 0;

		fadeScript = fader.GetComponent<GamePlayFadeScript> (); // fader script reference

		adScript = GameObject.FindGameObjectWithTag ("Ads").GetComponent<AdsScript> ();
		cameraScript = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraScript> (); // camera script reference
	
		lastPosition = transform.position;	// getting the players position

		easyDifficulty = GamePreferences.GetEasyDifficultyState ();
		mediumDifficulty = GamePreferences.GetMediumDifficultyState ();
		hardDifficulty = GamePreferences.GetHardDifficultyState ();

		// check if the game was started from main manu to set initial values
		IsTheGameStartedFromMainMenu ();
		
		// check if the game was resumed after player died to continue the game
		IsTheGameResumedAfterPlayerDied ();

		Time.timeScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
			
		if (Time.timeScale == 1.0f) {
			
			PlayerWalkKeyboard ();
			
			PlayerWalkMobile ();
			
			if(countTouches > 3) {
				SetScore ();
			}
			
			
		}
			
	}

	void LateUpdate() {

		if (isTheGameStartedFromBegining) {
			
			if (Input.GetMouseButtonDown (0)) {
				
				if(adScript.bannerView != null) {
					adScript.bannerView.Hide();
					adScript.bannerView.Destroy();
					adScript.bannerView = null;
				}
				
				Time.timeScale = 1.0f;	
				ready.SetActive(false);
				countPoints = true; // score points is true initialy
				isTheGameStartedFromBegining = false;
				
			}
			
		}

		CheckBounds ();
	}

	// method for counting points
	void SetScore() {
		
		// if countPoints is true then count points
		if (countPoints) {
			
			if(transform.position.y < lastPosition.y) {
				scoreCount++;
			}
			
			lastPosition = transform.position;
		}
		
	}


	void CheckBounds() {

		// check if the players x is greather than the x of the boundaries, if its true set the players x to be boundaries x
		if (transform.position.x > boundaries.x) {
			Vector3 temp = transform.position;
			temp.x = boundaries.x;
			transform.position = temp;
		}

		// check if the players x is less than the x of the boundaries, if its true set the players x to be negative boundaries x
		if (transform.position.x < (-boundaries.x)) {
			Vector3 temp = transform.position;
			temp.x = -boundaries.x;
			transform.position = temp;
		}

	}


	void PlayerWalkMobile() {

		// force by which we will push the player
		float force = 0.0f;
		// the players velocity
		float velocity = Mathf.Abs (rigidbody2D.velocity.x);

		if (Input.touchCount > 0) {

				countTouches++;

				Touch h = Input.touches[0];
				
				Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(h.position.x, h.position.y));
				
				
				if(position.x > 0) {
					
					// if the velocity of the player is less than the maxVelocity
					if(velocity < maxVelocity)
						force = speed;
					
					// turn the player to face right
					Vector3 scale = transform.localScale;
					scale.x = 1;
					transform.localScale = scale;
					
					// animate the walk
					animator.SetInteger("Walk", 1);
					
				} else if(position.x < 0) {
					
					// if the velocity of the player is less than the maxVelocity
					if(velocity < maxVelocity)
						force = -speed;
					
					// turn the player to face right
					Vector3 scale = transform.localScale;
					scale.x = -1;
					transform.localScale = scale;
					
					// animate the walk
					animator.SetInteger("Walk", 1);
					
				}
				// add force to rigid body to move the player
				rigidbody2D.AddForce(new Vector2(force, 0));
				
				// set the idle animation
				if(h.phase == TouchPhase.Ended) 
					animator.SetInteger("Walk", 0);

			} // if Input.touchCount > 0

	} // player walk mobile

	void PlayerWalkKeyboard() {

		// force by which we will push the player
		float force = 0.0f;
		// the players velocity
		float velocity = Mathf.Abs (rigidbody2D.velocity.x);
		
		// getting the input from the player
		float h = Input.GetAxis ("Horizontal");
		
		// checking if the player is moving right
		if (h > 0) {
			
			// if the velocity of the player is less than the maxVelocity
			if(velocity < maxVelocity)
				force = speed;
			
			// turn the player to face right
			Vector3 scale = transform.localScale;
			scale.x = 1;
			transform.localScale = scale;
			
			// animate the walk
			animator.SetInteger("Walk", 1);
			
			// check if the player is moving left
		}  else if(h < 0) {
			
			// if the velocity of the player is less than the maxVelocity
			if(velocity < maxVelocity)
				force = -speed;
			
			// turn the player to face left
			Vector3 scale = transform.localScale;
			scale.x = -1;
			transform.localScale = scale;
			
			// animate the walk
			animator.SetInteger("Walk", 1);
			
		}
		
		// if the player is not moving set the animation to idle
		if(h == 0)
			animator.SetInteger("Walk", 0);
		
		// add force to rigid body to move the player
		rigidbody2D.AddForce (new Vector2 (force, 0));

	}

	// check if the game is started from main menu
	void IsTheGameStartedFromMainMenu() {
		
		int isTheGameStartedFromMainMenu = PlayerPrefs.GetInt (GamePreferences.GameStartedFromMainMenu);
		
		if (isTheGameStartedFromMainMenu == 1) {
			
			if(easyDifficulty == 1) {
				cameraScript.SetEasySpeed();
			}
			
			if(mediumDifficulty == 1) {
				cameraScript.SetMediumSpeed();
			}
			
			if(hardDifficulty == 1) {
				cameraScript.SetHardSpeed();
			}
			
			scoreCount = 0;	// score is 0
			coinCount = 0;  // coin score is 0
			lifeCount = 2;
			
			PlayerPrefs.SetInt(GamePreferences.GameStartedFromMainMenu, 0);
			
		}
		
	}
	
	// check if the game is resumed after player died
	void IsTheGameResumedAfterPlayerDied() {
		
		int gameResumedAfterPlayerDied = PlayerPrefs.GetInt (GamePreferences.GameResumedAfterPlayerDied);
		
		if (gameResumedAfterPlayerDied == 1) {
			
			scoreCount = PlayerPrefs.GetInt(GamePreferences.CurrentScore);
			coinCount = PlayerPrefs.GetInt(GamePreferences.CurrentCoinScore);
			lifeCount = PlayerPrefs.GetInt(GamePreferences.CurrentLifes);
			
			
			PlayerPrefs.SetInt(GamePreferences.GameResumedAfterPlayerDied, 0);
		}
		
	}

	void OnTriggerEnter2D(Collider2D target) {
		
		if (target.tag == "Coin") {
			coinCount++;
			scoreCount += 200;
			AudioSource.PlayClipAtPoint(coinClip, target.transform.position);
			target.gameObject.SetActive (false);
		}
		
		if (target.tag == "Life") {
			lifeCount++;
			scoreCount += 300;
			AudioSource.PlayClipAtPoint(lifeClip, target.transform.position);
			target.gameObject.SetActive (false);
			
		}
		
		if (target.tag == "Boundary") {
			cameraScript.moveCamera = false;
			countPoints = false;
			CheckGameStatus();
		}
		
		if (target.tag == "Deadly") {
			cameraScript.moveCamera = false;
			countPoints = false;
			CheckGameStatus();	
		}
		
	}  // ontrigger enter

	void CheckGameStatus() {
		
		// remove the player from scene by changing his x y position, and then decrement lifes
		Vector3 temp = transform.position;
		temp.x = 100;
		temp.y = 100;
		transform.position = temp;
		lifeCount--;
		
		// if lifes are less than 0 end the game, get the coins and score and check it with the highscore
		if(lifeCount < 0) {
			
			if(easyDifficulty == 1) {
				
				int currentHighscore = GamePreferences.GetEasyDifficultyHighscore();
				int currentCoinHighscore = GamePreferences.GetEasyDifficultyCoinScore();
				
				if(currentHighscore < scoreCount)
					GamePreferences.SetEasyDifficultyHighscore(scoreCount);
				
				if(currentCoinHighscore < coinCount) {
					GamePreferences.SetEasyDifficultyCoinScore(coinCount);
				}
				
				
			}   // easy difficulty
			
			if(mediumDifficulty == 1) {
				
				int currentHighscore = GamePreferences.GetMediumDifficultyHighscore();
				int currentCoinHighscore = GamePreferences.GetMediumDifficultyCoinScore();
				
				if(currentHighscore < scoreCount)
					GamePreferences.SetMediumDifficultyHighscore(scoreCount);
				
				if(currentCoinHighscore < coinCount) {
					GamePreferences.SetMediumDifficultyCoinScore(coinCount);
				}
				
			}   // mediumDifficulty
			
			if(hardDifficulty == 1) {
				
				int currentHighscore = GamePreferences.GetHardDifficultyHighscore();
				int currentCoinHighscore = GamePreferences.GetHardDifficultyCoinScore();
				
				if(currentHighscore < scoreCount)
					GamePreferences.SetHardDifficultyHighscore(scoreCount);
				
				if(currentCoinHighscore < coinCount) {
					GamePreferences.SetHardDifficultyCoinScore(coinCount);
				}
				
			}   // hard difficulty
			
			// set the life count to be zero so that it wont display -1 on screen
			lifeCount = 0;
			
			StartCoroutine(ReloadMainMenuAfterPlayerHasNoMoreLifesLeft());
			
			
			// the player has still lifes left to continue the game
		}   else {
			
			PlayerPrefs.SetInt(GamePreferences.CurrentScore, scoreCount);
			PlayerPrefs.SetInt(GamePreferences.CurrentCoinScore, coinCount);
			PlayerPrefs.SetInt(GamePreferences.CurrentLifes, lifeCount);
			
			PlayerPrefs.SetInt(GamePreferences.GameResumedAfterPlayerDied, 1);
			
			StartCoroutine(ReloadGame());
			
			
		}
		
	}

	// reload the game after player dies
	IEnumerator ReloadGame() {
		
		// set the fader to be active because we need it now
		fader.SetActive (true);
		
		// wait half a second before fading
		yield return new WaitForSeconds(0.5f);
		
		// fade
		float fadeTime = fadeScript.BeginFade (1);
		
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel (Application.loadedLevel);
	}
	
	// reload main menu after player has no more lifes left
	IEnumerator ReloadMainMenuAfterPlayerHasNoMoreLifesLeft() {
		
		// activate the end showing score gameobjects
		endScoreBG.SetActive (true);
		endScoreText.rectTransform.localPosition = endScoreTextPosition;
		endCoinText.rectTransform.localPosition = endCoinScorePosition;

		endScoreText.text = scoreCount.ToString();
		endCoinText.text = coinCount.ToString ();
				
		// wait 3 seconds for the player to see the score
		yield return new WaitForSeconds(3);
		
		// activate fader
		fader.SetActive (true);
		
		// set MainMenuOpenedFromGameplay to 1, so that the fader in MainMenuScene will fade smoothly
		PlayerPrefs.SetInt (GamePreferences.MainMenuOpenedFromGameplay, 1);
		
		// fade
		float fadeTime = fadeScript.BeginFade (1);
		yield return new WaitForSeconds(fadeTime);
		
		Application.LoadLevel ("Main Menu Scene");
	}

} // class































