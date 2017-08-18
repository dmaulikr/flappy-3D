using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour 
{
	public GameObject hurdlePrefab;
	public List<GameObject> listOfHurdles = new List<GameObject> ();
	[Space]
	public GameObject gameoverPanel;
	public GameObject pausePanel;
	public GameObject startPanel;
	public GameObject iapPanel;
	public GameObject checkMarkMusic;
	public Transform swirling;

	public FlappyController flappy;
	public Text scoreText;

	AudioSource audioSource;

	int canPlayMusic;
	int score;
	int maxHurdles;
	public bool isGameOver;
	int currentCam;
	int hurdleCount;
	int lastXPos;
	int isPurchased;

	public static Gamemanager o;
	// Use this for initialization
	void Awake () 
	{
		
		if (o == null)
			o = this;
		else
			Destroy (gameObject);

		isPurchased = PlayerPrefs.GetInt ("isPurchased");
//		if (isPurchased != 1) 
//		{
//			Instantiate ((GameObject)Resources.Load ("AdMob"));
//		}

		audioSource = GetComponent<AudioSource> ();
		canPlayMusic = PlayerPrefs.GetInt ("bgMusic", 1);

		if (canPlayMusic == 1)
		{
			audioSource.Play ();
			checkMarkMusic.SetActive (true);
		}

		currentCam = 0;
		lastXPos = -2;
		maxHurdles = 10;
		score = -10;
		scoreText.text = score + "";
		isGameOver = false;
		GameObject obj;
//		for (int i = 0; i < maxHurdles; i++)
//		{
//			obj = Instantiate (hurdlePrefab);
//			listOfHurdles.Add (obj);
//		}

		for (int i = 0; i < 5; i++) 
		{
			obj = Instantiate (listOfHurdles [i]);
			listOfHurdles.Add (obj);
		}


		for (int i = 0; i < 10; i++) 
		{
			GenerateHurdle ();
		}
	}

	public void GenerateHurdle()
	{
		listOfHurdles [hurdleCount].SetActive (true);
		listOfHurdles [hurdleCount].transform.position = new Vector3 (lastXPos, Random.Range (-1.5f, 1.5f), 0);
		lastXPos += 4;
		hurdleCount = (hurdleCount + 1) % maxHurdles;

		score++;
		scoreText.text = score + "";
	}


	public void StartGame()
	{
		flappy.GetComponent<Rigidbody> ().isKinematic = false;
		startPanel.SetActive (false);
		scoreText.gameObject.SetActive (true);
		flappy.enabled = true;
	}



	int gamesPlayed;
	public void GameOver()
	{
//		gamesPlayed = PlayerPrefs.GetInt ("gamesPlayed");
//		gamesPlayed++;
//		PlayerPrefs.SetInt ("gamesPlayed", gamesPlayed);

		isGameOver = true;
		swirling.gameObject.SetActive (true);
		Camera.main.GetComponent<CameraFollow> ().enabled = false;
		int highest = PlayerPrefs.GetInt ("highest");
		if (score > highest) 
		{
			highest = score;
			PlayerPrefs.SetInt ("highest", highest);
		}
		scoreText.gameObject.SetActive (false);
		gameoverPanel.transform.GetChild (0).GetComponent<Text> ().text = score + "";
		gameoverPanel.transform.GetChild (1).GetComponent<Text> ().text = "Best: " + highest;
		gameoverPanel.SetActive (true);
		ShowAd ();
	}

	void Update()
	{
		if (isGameOver)
		{
			swirling.Rotate (0, 5, 0);
		}
	}

	void ShowAd()
	{
		if (isPurchased != 1) 
		{
			int ad = PlayerPrefs.GetInt ("ad");
			if (ad % 2 == 0) 
			{
				AdMob.o.ShowVideo ();
			}
			ad++;
			PlayerPrefs.SetInt ("ad", ad);
		}
	}

	public void Button_Click(Button btn)
	{
		if (btn.name == "home") 
		{
			SceneManager.LoadScene ("main");
		}
		else if (btn.name == "replay") 
		{
			SceneManager.LoadScene ("game");
		}
		else if (btn.name == "exit") 
		{
			Application.Quit ();
		}


	}

	public void BG_Music()
	{
		canPlayMusic = 1 - canPlayMusic;
		if (canPlayMusic == 0) 
		{
			audioSource.Stop ();
			checkMarkMusic.SetActive (false);
		}
		else
		{
			audioSource.Play ();
			checkMarkMusic.SetActive (true);
		}

		PlayerPrefs.SetInt ("bgMusic", canPlayMusic);
	}


	public void IAP(Button btn)
	{
		if (btn.name == "removeAds") 
		{
			iapPanel.SetActive (true);
		}
		else if (btn.name == "close") 
		{
			iapPanel.SetActive (false);
		}

	}

	public void OnPurchaseComplete()
	{
		PlayerPrefs.SetInt ("isPurchased", 1);
		iapPanel.SetActive (false);
//		Destroy (AdMob.o.gameObject);
	}
	public void CamSwitch()
	{
		if (currentCam == 0) 
		{
			currentCam = 1;
			Camera.main.transform.position = flappy.transform.GetChild (currentCam).position;
			Camera.main.transform.rotation = flappy.transform.GetChild (currentCam).rotation;
		}
		else
		{
			currentCam = 0;
			Camera.main.transform.position = flappy.transform.GetChild (currentCam).position;
			Camera.main.transform.rotation = flappy.transform.GetChild (currentCam).rotation;
		}	
	}
	public static void LoadGame()
	{
		SceneManager.LoadScene ("game");
	}

}
