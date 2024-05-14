using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelController : MonoBehaviour {

	public GameObject normalPlatform,oneRotationPlatformNeg,oneRotationPlatformPos,canvas,newHighScoreText,firstPlatform,player,cam,bkPlane;
	public Text scoreText,highScoreText,appriciationText;
	public int score = 0, scoreUpdate = 1;
	public float rodLength = 4, minAngle = 45;
	public AudioClip normalBkMusic, gameOverBkMusic,btnSound;

	public List<Material> mats;
	public List<string> appriciationMsg;
	public List<Text> txts;
	public List<Image> icons;
	[HideInInspector]
	public List<Vector3> nextPivotPosition;
	[HideInInspector]
	public List<GameObject> createdPlatforms;
	[HideInInspector]
	public List<int> matsIndex;

	Vector3 previousPlatformPosition = new Vector3 (0, 0, 0);
	bool lastPlatformRotation = false;   // Store false first rotation negitive else true 
	// This will store rotation direction of latest created platform

	Animator canvasAnim;
	AudioSource aud,canvasAud;
	Player playerScript;
	CameraFollow cameraScript;
	GameController gameControllerScript;
	GPGSController gpsControllerScript;

	void Start () {
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		gpsControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GPGSController> ();
		playerScript = player.GetComponent<Player> ();
		aud = GetComponent<AudioSource> ();
		canvasAud = canvas.GetComponent<AudioSource> ();
		canvasAnim = canvas.GetComponent<Animator> ();
		cameraScript = cam.GetComponent<CameraFollow> ();
		nextPivotPosition = new List<Vector3> ();  
		createdPlatforms = new List<GameObject> ();
		matsIndex = new List<int> ();
		createdPlatforms.Add (firstPlatform);
		int firstRandMatIndex = Random.Range ((int)0, (int)mats.Count);
		firstPlatform.GetComponent<Renderer>().material= mats[firstRandMatIndex];
		matsIndex.Add (firstRandMatIndex);
		playerScript.SetRodMat (mats [firstRandMatIndex]);
		CreatePlatform(5);
		SetTheme (false);
		scoreText.text = score.ToString ();
		highScoreText.text = "Best :"+ PlayerPrefs.GetInt ("HighScore").ToString ();
		if (PlayerPrefs.GetInt ("Music") == 1) {
			aud.clip = normalBkMusic;
			aud.Play ();
			aud.loop = true;
		}
		if (playerScript.rotSpeed > 0) {
			lastPlatformRotation = true;
		} 
		else {
			lastPlatformRotation = false;    
		}
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			MainMenuBtn ();
		}

	}

	public void CreatePlatform(int number){
		for (int i = 0; i < number; i++) {
			float maxAngle = 180 - minAngle;
			float y = (rodLength * (Mathf.Sin (Random.Range (minAngle, maxAngle) * Mathf.Deg2Rad))) + previousPlatformPosition.y;
			float x = (rodLength * (Mathf.Cos (Random.Range (minAngle, maxAngle) * Mathf.Deg2Rad))) + previousPlatformPosition.x;
			Vector3 newPlatformPosition = new Vector3 (x,y,0);
			GameObject pltform;

			if (score < 2) {
				pltform = NormalPlatform (newPlatformPosition);
			}
			else {
				int randomNum = Random.Range (1, 6);
//				Debug.Log (randomNum);
				if (randomNum % 5 == 0) {
					pltform = NormalPlatform (newPlatformPosition);
				} 
				else {
					pltform = OneRotationPlatform (newPlatformPosition);
				}
			}

			int randMatIndex = Random.Range ((int)0, (int)mats.Count);
			matsIndex.Add (randMatIndex);
			pltform.GetComponent<Renderer> ().material = mats [randMatIndex];
			previousPlatformPosition = newPlatformPosition;
			nextPivotPosition.Add (newPlatformPosition);
			createdPlatforms.Add (pltform);
			if (lastPlatformRotation) {
				lastPlatformRotation = false;
			} 
			else {
				lastPlatformRotation = true;
			}
		}
	}

	// Different cteate platforms begin

	GameObject NormalPlatform(Vector3 pos){
		GameObject pltform;
		pltform = Instantiate (normalPlatform, pos, Quaternion.identity)as GameObject;
		return pltform;
	}
	GameObject OneRotationPlatform(Vector3 pos){
		GameObject pltform;
		if (!lastPlatformRotation) {
			pltform = Instantiate (oneRotationPlatformNeg, pos, Quaternion.identity)as GameObject;
		} 
		else {
			pltform = Instantiate (oneRotationPlatformPos, pos, Quaternion.identity)as GameObject;
		}
		return pltform;
	}

	// Different cteate platforms end

	public void ScoreIncrease(){
		score = score + scoreUpdate;
		scoreText.text = score.ToString ();
		if ((score > 0) && (score % 5 == 0)) {
			appriciationText.text = appriciationMsg [Random.Range ((int)0, (int)appriciationMsg.Count)];
			canvasAnim.SetTrigger ("Appriciate");
		}
		StartCoroutine (AchievementCheck (score,PlayerPrefs.GetInt("Level"),false));
	}

	IEnumerator AchievementCheck(int sco,int lvl,bool lt = false){
		yield return null;
		gpsControllerScript.ScoreChecker (sco, lvl, lt);
	}

	public void GameOver(){
		bool newHighScore = false;
		Handheld.Vibrate ();
		cameraScript.enabled = false;
		if(PlayerPrefs.GetInt("HighScore")<score){
			PlayerPrefs.SetInt ("HighScore", score);
			newHighScore = true;
		}
		StartCoroutine (GameOver (newHighScore));
		StartCoroutine (AchievementCheck (score,PlayerPrefs.GetInt("Level"),true));
		gpsControllerScript.PostScoreInLeaderBoard (score);
		gpsControllerScript.LoseCentury ();
	}
		
	IEnumerator GameOver(bool newHighScore){
		yield return new WaitForSeconds(0.5f);
		if (PlayerPrefs.GetInt ("Music") == 1) {
			aud.clip = gameOverBkMusic;
			aud.volume = aud.volume / 1.5f;
			aud.Play ();
			aud.loop = false;
		}
		canvasAnim.SetTrigger("GameOver");
		if (newHighScore) {
			yield return new WaitForSeconds(1f);
			newHighScoreText.SetActive (true);
		}
	}

	public void RestartBtn(){
		if(PlayerPrefs.GetInt("Sound")==1){
			canvasAud.clip = btnSound;
			canvasAud.Play ();
		}
		Handheld.Vibrate ();
		SceneManager.LoadScene ("Game_Scene");
	}

	public void SetTheme(bool change = true){
		int currentTheme = PlayerPrefs.GetInt ("Theme");
		if (change) {
			if (currentTheme == (gameControllerScript.themes.Count - 1)) {
				currentTheme = 0;
			} 
			else {
				currentTheme++;
			}
			PlayerPrefs.SetInt ("Theme", currentTheme);
		}
		bkPlane.GetComponent<Renderer> ().material = gameControllerScript.themes [currentTheme].bkMat;
		for (int i = 0; i < icons.Count; i++) {
			icons [i].color = gameControllerScript.themes [currentTheme].iconColor;
		}
		for (int i = 0; i < txts.Count; i++) {
			txts [i].color = gameControllerScript.themes [currentTheme].textColor;
		}
	}

	public void MainMenuBtn(){
		if(PlayerPrefs.GetInt("Sound")==1){
			canvasAud.clip = btnSound;
			canvasAud.Play ();
		}
		Handheld.Vibrate ();
		SceneManager.LoadScene ("Main_Menu");
	}

}
