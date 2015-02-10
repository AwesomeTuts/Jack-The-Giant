using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsSceneAnimator : MonoBehaviour {

	[SerializeField]
	private Button easyBtn;
	
	private Animator easyBtnAnimator;
	
	[SerializeField]
	private Button mediumBtn;
	
	private Animator mediumBtnAnimator;
	
	[SerializeField]
	private Button hardBtn;
	
	private Animator hardBtnAnimator;
	
	[SerializeField]
	private Button backBtn;
	
	private Animator backBtnAnimator;

	private static OptionsSceneAnimator instance;

	void Awake() {
//		if(instance)
//			DestroyImmediate(gameObject);	// delete duplicate
//		else {
//			instance = this;	//Make this object the only instance
//			DontDestroyOnLoad(gameObject);	// dont destoy it when scenes load
//		}
	}

	// Use this for initialization
	void Start () {

			GetAnimators ();
			AnimateButtonsSlideIn ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetAnimators() {
		easyBtnAnimator = easyBtn.GetComponent<Animator> ();
		mediumBtnAnimator = mediumBtn.GetComponent<Animator> ();
		hardBtnAnimator = hardBtn.GetComponent<Animator> ();
		backBtnAnimator = backBtn.GetComponent<Animator> ();
	}
	
	void AnimateButtonsSlideIn() {
		easyBtnAnimator.Play ("EasyBtnSlideIn");
		mediumBtnAnimator.Play ("MediumBtnSlideIn");
		hardBtnAnimator.Play ("HardBtnSlideIn");
		backBtnAnimator.Play ("BackBtnSlideIn");

	}
}
