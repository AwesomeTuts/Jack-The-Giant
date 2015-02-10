using UnityEngine;
using System.Collections;

public class PauseElementsController : MonoBehaviour {

	private AudioSource audioSource;
	
	private bool audioSourceWasPlayingBefore;
	
	private float continueSong;

	[SerializeField]
	private GameObject pauseBg;

	private Animator pauseBgAnimator;

	[SerializeField]
	private GameObject resumeBtn;

	private Animator resumeBtnAnimator;

	[SerializeField]
	private GameObject quitBtn;

	private Animator quitBtnAnimator;


	[SerializeField]
	private GameObject ready;

	private float waitTime = 1.5f;


	// Use this for initialization
	void Start () {
		audioSource = GameObject.FindGameObjectWithTag ("BGMusic").GetComponent<AudioSource> ();

		pauseBgAnimator = pauseBg.GetComponent<Animator> ();
		resumeBtnAnimator = resumeBtn.GetComponent<Animator> ();
		quitBtnAnimator = quitBtn.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PauseButton() {

		if(audioSource != null) {
			
			if(audioSource.isPlaying) {
				continueSong = audioSource.time;
				audioSource.Stop();
				audioSourceWasPlayingBefore = true;
			}
			
		}

		if (Time.timeScale != 0.0f) {

			pauseBgAnimator.Play ("PauseBGSlideIn");
			resumeBtnAnimator.Play ("ResumeSlideIn");
			quitBtnAnimator.Play ("QuitSlideIn");
			
			Time.timeScale = 0.0f;		

		}
		

		
	} // start button
	
	public void ResumeButton() {

		if (audioSource != null) {
			
			if(audioSourceWasPlayingBefore) {
				audioSource.time = continueSong;
				audioSource.Play();
				audioSourceWasPlayingBefore = false;
			}
			
		}
	
				pauseBgAnimator.Play ("PauseBGSlideOut");
				resumeBtnAnimator.Play ("ResumeSlideOut");
				quitBtnAnimator.Play ("QuitSlideOut");
	
	
				Time.timeScale = 1.0f;

//		StartCoroutine (ResumeGame ());


	} // resume button

	public void QuitButton() {
		
		if (audioSource != null) {
			
			if(audioSourceWasPlayingBefore) {
				audioSource.time = continueSong;
				audioSource.Play();
				audioSourceWasPlayingBefore = false;
			}
			
		}
		
		Application.LoadLevel("Main Menu Scene");
		
	} // quit button

	IEnumerator ResumeGame() {

		yield return new WaitForSeconds(2);

		Time.timeScale = 1.0f;

//		if (!ready.activeInHierarchy) {
//			Debug.Log("In if statement");
//			ready.SetActive(true);
//			Player.isTheGameStartedFromBegining = true;
//		}


	}

} // class

















































