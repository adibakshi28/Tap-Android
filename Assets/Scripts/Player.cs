using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject leftPivotRod, rightPivotRod,levelDataController,cam;
	public float rotSpeed = 100,followSpeed = 2;
	public AudioClip successClip, failClip;

	bool rightRotate = false, canShift = false, gameOver = false;
	int currentPlatformCount = 0;
	Transform rodTransform;
	BoxCollider2D leftRodCC,rightRodCC;
	Rigidbody2D rb;
	AudioSource aud;
	Renderer leftRodRendrer,rightRodRendrer;
	LevelController levelController;
	CameraFollow cameraScript;

	void Start () {
		levelController = levelDataController.GetComponent<LevelController> ();
		cameraScript = cam.GetComponent<CameraFollow> ();
		leftRodCC = leftPivotRod.GetComponent<BoxCollider2D> ();
		rightRodCC = rightPivotRod.GetComponent<BoxCollider2D> ();
		aud = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody2D> ();
		leftRodRendrer = leftPivotRod.GetComponent<Renderer> ();
		rightRodRendrer = rightPivotRod.GetComponent<Renderer> ();


		rotSpeed = rotSpeed * PlayerPrefs.GetInt ("Level");

		   // Start with left rotation
		rightRotate = false;
		rightRodCC.enabled = false;
		leftRodCC.enabled = true;
		canShift = false;
		rotSpeed = (-1) * rotSpeed;     // Reverse rotation direction
		rightPivotRod.transform.DetachChildren ();
		rodTransform = leftPivotRod.transform;
		rightPivotRod.transform.parent = leftPivotRod.transform;
		leftPivotRod.transform.parent = this.gameObject.transform;
		cameraScript.player = leftPivotRod;
		cameraScript.StartFollow ();
	}

	void Update () {
		if ((Input.GetMouseButtonDown (0))&&(!gameOver)) {
		//	Debug.Log ("Mouse clicked");
			if (rightRotate) {
				if (canShift) {
				//	Debug.Log ("Left rotatr() called");
					LeftRodRotate ();
					levelController.ScoreIncrease ();
				} 
				else {
				   // GameOver
					GameOver();
				}
			}
			else {
				if (canShift) {
			//		Debug.Log ("Right rotatr() called");
					RightRodRotate ();
					levelController.ScoreIncrease ();
				} 
				else {
				  // GameOver
					GameOver();
				}

			}
		}
			
	//	Debug.Log (canShift);
	}

	void LateUpdate(){
		rodTransform.Rotate(0,0,rotSpeed * Time.deltaTime);
		rotSpeed = rotSpeed * 1.00015f;
	}

	public void SetRodMat(Material mat){
		    rightRodRendrer.material = mat;
			leftRodRendrer.material = mat;
	}

	void RightRodRotate(){
		rightRotate = true;
		leftRodCC.enabled = false;
		rightRodCC.enabled = true;
		canShift = false;
		SetRodMat(levelController.mats [levelController.matsIndex [currentPlatformCount]]);
		rotSpeed = (-1) * rotSpeed;     // Reverse rotation direction
		leftPivotRod.transform.DetachChildren ();
		rodTransform = rightPivotRod.transform;
		leftPivotRod.transform.parent = rightPivotRod.transform;
		rightPivotRod.transform.parent = this.gameObject.transform;

	//	rodTransform.position = Vector3.Slerp(rodTransform.position,levelController.nextPivotPosition[currentPlatformCount] , followSpeed * Time.deltaTime);
		Vector3 pos = rodTransform.position;
		pos = levelController.nextPivotPosition[currentPlatformCount];
		rodTransform.position = pos;
		Destroy (levelController.createdPlatforms [currentPlatformCount]);
		rightRodRendrer.material = levelController.mats [levelController.matsIndex [currentPlatformCount+1]];
		leftRodRendrer.material = levelController.mats [levelController.matsIndex [currentPlatformCount+1]];

		currentPlatformCount++;
		cameraScript.player = rightPivotRod;
		levelController.CreatePlatform (1);
		if (PlayerPrefs.GetInt ("Sound") == 1) {
			aud.clip = successClip;
			aud.Play ();
		}
	}

	void LeftRodRotate(){
		rightRotate = false;
		rightRodCC.enabled = false;
		leftRodCC.enabled = true;
		canShift = false;
		SetRodMat(levelController.mats [levelController.matsIndex [currentPlatformCount]]);
		rotSpeed = (-1) * rotSpeed;     // Reverse rotation direction
		rightPivotRod.transform.DetachChildren ();
		rodTransform = leftPivotRod.transform;
		rightPivotRod.transform.parent = leftPivotRod.transform;
		leftPivotRod.transform.parent = this.gameObject.transform;


	//	rodTransform.position = Vector3.Slerp(rodTransform.position,levelController.nextPivotPosition[currentPlatformCount] , followSpeed * Time.deltaTime);
		Vector3 pos = rodTransform.position;
		pos = levelController.nextPivotPosition[currentPlatformCount];
		rodTransform.position = pos;
		Destroy (levelController.createdPlatforms [currentPlatformCount]);
		rightRodRendrer.material = levelController.mats [levelController.matsIndex [currentPlatformCount+1]];
		leftRodRendrer.material = levelController.mats [levelController.matsIndex [currentPlatformCount+1]];

		currentPlatformCount++;
		cameraScript.player = leftPivotRod;
		levelController.CreatePlatform (1);
		if (PlayerPrefs.GetInt ("Sound") == 1) {
			aud.clip = successClip;
			aud.Play ();
		}
	}

	void GameOver(){
		gameOver=true;
		levelController.GameOver ();
		if (PlayerPrefs.GetInt ("Sound") == 1) {
			aud.clip = failClip;
			aud.Play ();
		}
		rotSpeed = 0;    //Stop rotation
		rb.isKinematic = false;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Platform") {
			canShift = true;
			SetRodMat(levelController.mats [levelController.matsIndex [currentPlatformCount+1]]);
		}
		if (col.gameObject.tag == "Enemy") {
			GameOver ();
		}
	}
	void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.tag == "Platform") {
			canShift = true;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Platform") {
			canShift = false;
			SetRodMat(levelController.mats [levelController.matsIndex [currentPlatformCount]]);
		}
	}

}
