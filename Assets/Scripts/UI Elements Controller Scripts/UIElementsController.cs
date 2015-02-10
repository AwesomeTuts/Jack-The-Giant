using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIElementsController : MonoBehaviour {

	[SerializeField]
	private Text lifeText;

	[SerializeField]
	private Text coinText;

	[SerializeField]
	private Text scoreText;

	// Use this for initialization
	void Start () {
		UpdateScore ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScore ();
	}

	void UpdateScore() {
		lifeText.text = "x" + Player.lifeCount;
		coinText.text = "x" + Player.coinCount;
		scoreText.text = "" + Player.scoreCount;
	}
}
