using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private float speed = 1.0f;
	private float acceleration = 0.2f;
	private float maxSpeed = 3.2f;
	private float smooth = 0.1f;

	private float easySpeed = 2.9f;
	private float mediumSpeed = 3.4f;
	private float hardSpeed = 3.9f;

	public bool moveCamera;

	private int targetWidth = 480;
//	private int targetHeight = 800;
	
	private float pixelToUnits = 100.0f;

	void Awake() {

		ScaleByWidth ();
		
		Vector3 t = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, Camera.main.transform.position.z));
		
		CloudSpawnerScript.maxX = t.x - 0.5f;
		CloudSpawnerScript.minX = -t.x + 0.5f;
	}

	// Use this for initialization
	void Start () {

		moveCamera = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
	//	MoveCameraByLerp ();

		if (moveCamera) {
			MoveCamera ();	
		}

	}

	public void SetEasySpeed() {
		this.maxSpeed = easySpeed;
	}

	public void SetMediumSpeed() {
		this.maxSpeed = mediumSpeed;
	}

	public void SetHardSpeed() {
		this.maxSpeed = hardSpeed;
	}

	void MoveCameraByLerp() {

		Vector3 temp = transform.position;

		temp.y = Mathf.Lerp (temp.y, temp.y - (speed * Time.deltaTime), smooth);

		transform.position = temp;
		
		speed += acceleration * Time.deltaTime;
		
		if (speed > maxSpeed)
			speed = maxSpeed;


	}

	void MoveCamera() {

		Vector3 temp = transform.position;

		float oldY = temp.y;

		float newY = temp.y - (speed * Time.deltaTime);

		temp.y = Mathf.Clamp (temp.y, oldY, newY);

		transform.position = temp;

		speed += acceleration * Time.deltaTime;

		if (speed > maxSpeed)
				speed = maxSpeed;

	}

	void ScaleByWidth() {
		
		int height = Mathf.CeilToInt (targetWidth / (float)Screen.width * Screen.height);
		
		camera.orthographicSize = height / pixelToUnits / 2;

		if (camera.orthographicSize < 3.6)
						camera.orthographicSize = 3.6f;

	}

}
