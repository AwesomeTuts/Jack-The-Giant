using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenuController : MonoBehaviour {

	[SerializeField]
	private Button easyBtn;

	private Animator easyBtnAnimator;

	[SerializeField]
	private Button mediumBtn;

	private Animator mediumBtnAnimator;

	[SerializeField]
	private Button hardBtn;

	[SerializeField]
	private Image sign;

	public AudioClip touchClip;
	AudioSource audioSource; // BGMusic audio source

	// Use this for initialization
	void Start () {
		audioSource = GameObject.FindGameObjectWithTag ("BGMusic").GetComponent<AudioSource> ();
		CheckDifficulty ();
	}
	
	public void EasyButton() {

		if(!audioSource.isPlaying) {
			AudioSource.PlayClipAtPoint(touchClip, transform.position);
		}

		GamePreferences.SetEasyDifficultyState(1);
		GamePreferences.SetMediumDifficultyState(0);
		GamePreferences.SetHardDifficultyState(0);

		float positionY = easyBtn.transform.localPosition.y;
		Vector3 temp = sign.rectTransform.localPosition;
		temp.y = positionY;
		sign.rectTransform.localPosition = temp;
	}

	public void MediumButton() {

		if(!audioSource.isPlaying) {
			AudioSource.PlayClipAtPoint(touchClip, transform.position);
		}

		GamePreferences.SetEasyDifficultyState(0);
		GamePreferences.SetMediumDifficultyState(1);
		GamePreferences.SetHardDifficultyState(0);

		float positionY = mediumBtn.transform.localPosition.y;
		Vector3 temp = sign.rectTransform.localPosition;
		temp.y = positionY;
		sign.rectTransform.localPosition = temp;
	}

	public void HardButton() {

		if(!audioSource.isPlaying) {
			AudioSource.PlayClipAtPoint(touchClip, transform.position);
		}

		GamePreferences.SetEasyDifficultyState(0);
		GamePreferences.SetMediumDifficultyState(0);
		GamePreferences.SetHardDifficultyState(1);

		float positionY = hardBtn.transform.localPosition.y;
		Vector3 temp = sign.rectTransform.localPosition;
		temp.y = positionY;
		sign.rectTransform.localPosition = temp;
	}

	public void BackButton() {
		Application.LoadLevel ("Main Menu Scene");
	}

	void CheckDifficulty() {
		
		int easyDifficulty = GamePreferences.GetEasyDifficultyState ();
		int mediumDifficulty = GamePreferences.GetMediumDifficultyState ();
		int hardDifficulty = GamePreferences.GetHardDifficultyState ();
		
		if(easyDifficulty == 1) {

			float positionY = easyBtn.transform.localPosition.y;
			Vector3 temp = sign.rectTransform.localPosition;
			temp.y = positionY;
			sign.rectTransform.localPosition = temp;
		}
		
		if (mediumDifficulty == 1) {
			float positionY = mediumBtn.transform.localPosition.y;
			Vector3 temp = sign.rectTransform.localPosition;
			temp.y = positionY;
			sign.rectTransform.localPosition = temp;
		}
		
		if (hardDifficulty == 1) {
			float positionY = hardBtn.transform.localPosition.y;
			Vector3 temp = sign.rectTransform.localPosition;
			temp.y = positionY;
			sign.rectTransform.localPosition = temp;
		}
		
	} // check difficulty

} // class
