﻿using UnityEngine;
using System.Collections;

public class CloudCollectorScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target) {

		if (target.tag == "Cloud" || target.tag == "Deadly") {
			target.gameObject.SetActive(false);
		}

	}


}
