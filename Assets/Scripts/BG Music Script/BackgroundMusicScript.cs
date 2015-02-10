using UnityEngine;
using System.Collections;

public class BackgroundMusicScript : MonoBehaviour {

	private static BackgroundMusicScript instance;

	void Start() {
		if(instance)
			DestroyImmediate(gameObject);	// delete duplicate
		else {
			instance = this;	//Make this object the only instance
			DontDestroyOnLoad(gameObject);	// dont destoy it when scenes load
		}
	}
}
