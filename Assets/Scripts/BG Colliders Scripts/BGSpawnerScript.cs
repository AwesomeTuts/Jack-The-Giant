using UnityEngine;
using System.Collections;

public class BGSpawnerScript : MonoBehaviour {

	public GameObject[] backgrounds;
	public float lastY;

	// Use this for initialization
	void Start () {

		// getting the gameobjects with tag background
		backgrounds = GameObject.FindGameObjectsWithTag ("Background");

		lastY = backgrounds [0].transform.position.y;

		for(int i = 1; i < backgrounds.Length; i++) {

			// getting the last y position of the background
			if(lastY > backgrounds[i].transform.position.y)
				lastY = backgrounds[i].transform.position.y;

		}


	}
	
	void OnTriggerEnter2D(Collider2D target) {

		if (target.tag == "Background") {
				
			if(target.transform.position.y == lastY) {

				// get the position of the target
				Vector3 temp = target.transform.position;

				// get the size y of the collider
				float height = ((BoxCollider2D)target).size.y;

				// go through the array and check for inactivate gameobjects
				for(int i = 0; i < backgrounds.Length; i++) {

					// if the game object is inactivate activate it again
					if(!backgrounds[i].activeInHierarchy) {

						temp.y -= height;
						lastY = temp.y;

						backgrounds[i].transform.position = temp;
						backgrounds[i].SetActive(true);

					} // if background is not active

				} // for int i...

			} // if target.y == lastY

		} // if target.tag == background

	} // OnTriggerEnter2D


}
