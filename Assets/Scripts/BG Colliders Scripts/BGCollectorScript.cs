using UnityEngine;
using System.Collections;

public class BGCollectorScript : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D target) {

		if (target.tag == "Background") {
			// if the target is background , deactivate it
			target.gameObject.SetActive(false);
		}

	}

}
