using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSController : MonoBehaviour {

	[HideInInspector]
	public bool connectedToGooglePlaySevice = false;

	void Start () {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
		Signup ();   // Attempt signup at start of the game 
	}

	public void Signup(){
		if (!connectedToGooglePlaySevice) {
			Debug.Log ("Tring to Sign user up");
			// authenticate user:
			Social.localUser.Authenticate((bool success) => {
				connectedToGooglePlaySevice=success;

		/*		if(!superAddictAchievementCalled){
					superAddictAchievementCalled=true;
					SuperAddict(); // Call the number of times launched achievement function
				}*/

			});
		}
	}

/*	public void ShowAchievementsUI(){
		// show achievements UI
		Debug.Log("Showing Achievement Login");
		Social.ShowAchievementsUI();
	}*/

	public void ShowLeaderBoard(){
		// show leaderboard UI
		Debug.Log("Showing Leader Board");
		Social.ShowLeaderboardUI();
	}


	public void PostScoreInLeaderBoard(int score){
		Debug.Log ("Posting Score in leader board");
		Social.ReportScore(score, GPGSIds.leaderboard_highscores, (bool success) => {
			// handle success or failure
		});
	}


	// Indivudial Achievements Functions

	public void NoobAchievement(){    // Done
		Social.ReportProgress(GPGSIds.achievement_noob, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void Score5Achievement(){    // Done
		Social.ReportProgress(GPGSIds.achievement_score_5, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void Score15Achievement(){    // Done
		Social.ReportProgress(GPGSIds.achievement_score_15, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void Score30Achievement(){    // Done
		Social.ReportProgress(GPGSIds.achievement_score_30, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void Score50Achievement(){    // Done
		Social.ReportProgress(GPGSIds.achievement_score_50, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void Hard15Achievement(){    // Done
		Social.ReportProgress(GPGSIds.achievement_hard_15, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void Hard30Achievement(){    // Done
		Social.ReportProgress(GPGSIds.achievement_hard_30, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void ToughEasy5Achievement(){    // Done
		Social.ReportProgress(GPGSIds.achievement_tough_easy_5, 100.0f, (bool success) => {
			// handle success or failure
		});
	}

	public void LoseCentury(){     // Done
		PlayGamesPlatform.Instance.IncrementAchievement(
			GPGSIds.achievement_lose_century,1 , (bool success) => {
				// handle success or failure
			});
	}


	// Helper Functions

	public void ScoreChecker(int score,int level,bool lost = false){
		if ((score == 0) && (lost)) {
			NoobAchievement ();
			Debug.Log ("Noob");
		}

		if (score >= 5) {
			Score5Achievement ();
			Debug.Log ("5");
			if (score >= 15) {
				Score15Achievement ();
				Debug.Log ("15");
				if (level == 3) {
					Hard15Achievement ();
					Debug.Log ("hard 15");
				}
				if (score >= 30) {
					Score30Achievement();
					Debug.Log ("30");
					if (level == 3) {
						Hard30Achievement ();
						Debug.Log ("hard 30");
					}
					if (score >= 50) {
						Score50Achievement();
						Debug.Log ("50");
					}
				}
			}
		}

		if ((score < 5)&&(lost)&&(level==1)) {
			ToughEasy5Achievement ();
			Debug.Log ("TE5");
		}
	}

}