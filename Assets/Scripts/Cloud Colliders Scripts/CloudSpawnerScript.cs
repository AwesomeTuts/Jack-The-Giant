using UnityEngine;
using System.Collections;

public class CloudSpawnerScript : MonoBehaviour {
	
	[SerializeField]
	private GameObject[] clouds;	// our clouds
	
	public float distanceBetweenClouds = 3.0f; // distancate between y position of the cluds
	
	public static float minX, maxX; // min and max x for clouds
	
	private Player playerScript;  // player script
	
	public float lastCloudPositionY; // the y position of the last cloud

	[SerializeField]
	private GameObject[] collectables;

	public float lastPositionX;

	private int controllX;
	
	// Use this for initialization
	void Start () {

		controllX = 0;
		
		// the y position of the first cloud is going to start from center
		float center = Screen.width / Screen.height;

		center += 1.0f;
		
		CreateClouds(center);
		
		for(int i = 0; i < collectables.Length; i++) {
			collectables[i].SetActive(false);
		}
		
		// getting players script
		playerScript = GameObject.Find ("Player").GetComponent<Player> ();
		
		PositionThePlayer ();
		
		
	}
	
	void OnTriggerEnter2D(Collider2D target) {
		
		if (target.tag == "Deadly" || target.tag == "Cloud") {
			
			if(target.transform.position.y == lastCloudPositionY) {
				
				Vector3 temp = target.transform.position;
				Shuffle(clouds);
				Shuffle(collectables);
				
				for(int i = 0; i < clouds.Length; i++) {
					
					if(!clouds[i].activeInHierarchy) {

						if(controllX == 0) {
							
							temp.x = Random.Range(0, maxX);
							controllX = 1;
							
						} else if(controllX == 1) {
							
							temp.x = Random.Range(0, minX);
							controllX = 2;
							
						} else if(controllX == 2) {
							
							temp.x = Random.Range(1.0f, maxX);
							controllX = 3;
							
						} else if(controllX == 3) {
							
							temp.x = Random.Range(-1.0f, minX);
							controllX = 0;
						}
						
						//temp.x = Random.Range(minX, maxX);
						temp.y -= distanceBetweenClouds;
						
						lastCloudPositionY = temp.y;
						
						
						clouds[i].transform.position = temp;
						clouds[i].SetActive(true);
						
						int random = Random.Range(0, collectables.Length);
						
						if(clouds[i].tag != "Deadly") {
							
							if(!collectables[random].activeInHierarchy) {

								if(collectables[random].tag == "Life") {

									if(Player.lifeCount < 2) {
										collectables[random].SetActive(true);
										collectables[random].transform.position = new Vector3(clouds[i].transform.position.x,
										                                                      clouds[i].transform.position.y + 0.7f,
										                                                      clouds[i].transform.position.z);
									}

								} // if tag == life
								else {

									collectables[random].SetActive(true);
									collectables[random].transform.position = new Vector3(clouds[i].transform.position.x,
									                                                      clouds[i].transform.position.y + 0.7f,
									                                                      clouds[i].transform.position.z);
								} // else

							} // if collectable is not active
							
						} // if clouds.tag != Deadly
						
					} // if clouds is not activate
					
				} // loop through clouds
				
			} // if target transform position == lastCloudPosition
			
		} // if target tag == deadly or cloud	
		
	} // On Trigger enter 2D
	
	
	void CreateClouds(float positionY) {
		
		// first shuffle clouds
		Shuffle (clouds);
		
		// loop through the array and initialize the clouds
		for(int i = 0; i < clouds.Length; i++) {
			
			//	clouds[i] = Instantiate(clouds[i], Vector3.zero, Quaternion.identity) as GameObject;
			
			Vector3 temp = clouds[i].transform.position;

			if(controllX == 0) {
				
				temp.x = Random.Range(0, maxX);
				controllX = 1;
				
			} else if(controllX == 1) {
				
				temp.x = Random.Range(0, minX);
				controllX = 2;
				
			} else if(controllX == 2) {
				
				temp.x = Random.Range(1.0f, maxX);
				controllX = 3;
				
			} else if(controllX == 3) {
				
				temp.x = Random.Range(-1.0f, minX);
				controllX = 0;
			}

			temp.y = positionY;

			lastCloudPositionY = temp.y;
			
			clouds[i].transform.position = temp;
			
			positionY -= distanceBetweenClouds;
			
		}
		
		
	}
	
	void Shuffle(GameObject[] array) {
		
		// loop through the array and shuffle it
		for(int i = 0; i < array.Length; i++) {
			
			GameObject temp = array[i];
			int random = Random.Range(i, array.Length);
			array[i] = array[random];
			array[random] = temp;
			
		}
		
	}
	
	void PositionThePlayer() {
		
		// getting back clouds
		GameObject[] darkClouds = GameObject.FindGameObjectsWithTag ("Deadly");
		
		// getting clouds in game
		GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag ("Cloud");
		
		for (int i = 0; i < darkClouds.Length; i++) {
			
			if(darkClouds[i].transform.position.y == 0) {
				
				Vector3 t = darkClouds[i].transform.position;
				
				darkClouds[i].transform.position = new Vector3(cloudsInGame[0].transform.position.x,
				                                               cloudsInGame[0].transform.position.y,
				                                               cloudsInGame[0].transform.position.z);
				
				cloudsInGame[0].transform.position = t;
				
			}
			
		}
		
		Vector3 temp = cloudsInGame [0].transform.position;
		
		for (int i = 1; i < cloudsInGame.Length; i++) {
			
			if(temp.y < cloudsInGame[i].transform.position.y)
				temp = cloudsInGame[i].transform.position;
			
		}
		
		
		// positioning the player above the cloud
		playerScript.transform.position = new Vector3 (temp.x, temp.y + 0.8f, temp.z);
		
		
	}
	
	
}










































