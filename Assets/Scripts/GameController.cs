using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public struct Theme{
	public Material bkMat;
	public Color iconColor;
	public Color textColor;
}

public class GameController : MonoBehaviour {

	public int version;
	public GameObject bkPlane;
	public List<Theme> themes;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("Played") == 0) {
		   // Playing for the first time
			PlayerPrefs.SetInt("Played",10);
			PlayerPrefs.SetInt ("Version", 1);
			PlayerPrefs.SetInt ("Music", 1);
			PlayerPrefs.SetInt ("Sound", 1);
			PlayerPrefs.SetInt ("HighScore", 0);
			PlayerPrefs.SetInt ("Level", 2);
		}
		if (version != PlayerPrefs.GetInt ("Version")) {
		  // All player prefs in new versions
			PlayerPrefs.SetInt("Version",version);
			PlayerPrefs.SetInt ("Theme", 0);
		}
		SetBkTheme ();
		DontDestroyOnLoad (this.gameObject);
		SceneManager.LoadScene ("Main_Menu");
	}


	public int SetBkTheme(bool change = false){
		int currentTheme = PlayerPrefs.GetInt ("Theme");
		if (change) {
			if (currentTheme == (themes.Count - 1)) {
				currentTheme = 0;
			} 
			else {
				currentTheme++;
			}
			PlayerPrefs.SetInt ("Theme", currentTheme);
		}
		bkPlane.GetComponent<Renderer> ().material = themes [currentTheme].bkMat;
		return currentTheme;
	}
	
}
