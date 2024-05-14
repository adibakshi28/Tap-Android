using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuController : MonoBehaviour {

	public AudioClip bkMusic,btnSound;
	public GameObject canvas, bkPlane, musicOnIcon,musicOffIcon,soundOffIcon,SoundOnIcon;
	public Button easyBtn,mediumBtn,hardBtn;
	public List<Text> txts;
	public List<Image> icons;

	AudioSource aud, canvasAud;
	GameController gameControllerScript;
	GPGSController gpsControllerScript;

	void Start(){
		gameControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		gpsControllerScript = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GPGSController> ();
		aud = GetComponent<AudioSource> ();
		canvasAud = canvas.GetComponent<AudioSource> ();

		Toughness (PlayerPrefs.GetInt ("Level"));

		if (PlayerPrefs.GetInt ("Music") == 1) {
			aud.clip = bkMusic;
			musicOnIcon.SetActive (true);
			musicOffIcon.SetActive (false);
			aud.Play ();
		} 
		else {
			musicOnIcon.SetActive (false);
			musicOffIcon.SetActive (true);
			aud.Stop ();
		}

		if (PlayerPrefs.GetInt ("Sound") == 1) {
			soundOffIcon.SetActive (false);
			SoundOnIcon.SetActive (true);
		} 
		else {
			soundOffIcon.SetActive (true);
			SoundOnIcon.SetActive (false);
		}
		SetTheme (false);
		GoogleSignUp ();
	}


	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit(); 
		}
	}

	public void PlayGameBtn(){
		if(PlayerPrefs.GetInt("Sound")==1){
			canvasAud.clip = btnSound;
			canvasAud.Play ();
		}
		Handheld.Vibrate ();
		SceneManager.LoadScene ("Game_Scene");
	}

	public void SoundBtn(){
		if (PlayerPrefs.GetInt ("Sound") == 1) {
			PlayerPrefs.SetInt ("Sound", 0);
			soundOffIcon.SetActive (true);
			SoundOnIcon.SetActive (false);
		} 
		else {
			PlayerPrefs.SetInt ("Sound", 1);
			canvasAud.clip = btnSound;
			soundOffIcon.SetActive (false);
			SoundOnIcon.SetActive (true);
			canvasAud.Play ();
		}
	}

	public void MusicBtn(){
		if(PlayerPrefs.GetInt("Sound")==1){
			canvasAud.clip = btnSound;
			canvasAud.Play ();
		}
		if (PlayerPrefs.GetInt ("Music") == 1) {
			PlayerPrefs.SetInt ("Music", 0);
			musicOnIcon.SetActive (false);
			musicOffIcon.SetActive (true);
			aud.Stop ();
		} 
		else {
			PlayerPrefs.SetInt ("Music", 1);
			aud.clip = bkMusic;
			musicOnIcon.SetActive (true);
			musicOffIcon.SetActive (false);
			aud.Play ();
		}
	}

	public void Toughness(int level){
		if(PlayerPrefs.GetInt("Sound")==1){
			canvasAud.clip = btnSound;
			canvasAud.Play ();
		}
		PlayerPrefs.SetInt ("Level", level);
		if (PlayerPrefs.GetInt ("Level") == 1) {
			easyBtn.interactable = false;
			mediumBtn.interactable = true;
			hardBtn.interactable = true;
		}
		if (PlayerPrefs.GetInt ("Level") == 2) {
			easyBtn.interactable = true;
			mediumBtn.interactable = false;
			hardBtn.interactable = true;
		}
		if (PlayerPrefs.GetInt ("Level") == 3) {
			easyBtn.interactable = true;
			mediumBtn.interactable = true;
			hardBtn.interactable = false;
		} 
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

	public void GoogleSignUp(){
		gpsControllerScript.Signup ();
	}

	public void ShowLeaderBoardUI(){
		if (gpsControllerScript.connectedToGooglePlaySevice) {
			gpsControllerScript.ShowLeaderBoard ();
		} 
		else {
			GoogleSignUp ();
			gpsControllerScript.ShowLeaderBoard ();
		}
	}

	public void Exit(){
		if(PlayerPrefs.GetInt("Sound")==1){
			canvasAud.clip = btnSound;
			canvasAud.Play ();
		}
		Handheld.Vibrate ();
		Application.Quit ();
	}



}
