using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{

	public GameObject settingsPanel;
	public GameObject checkMarkMusic;
	public GameObject checkMarkSounds;


	AudioSource audioSource;
	int canPlayMusic;
	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource> ();
		canPlayMusic = PlayerPrefs.GetInt ("bgMusic", 1);

		if (canPlayMusic == 1)
		{
			audioSource.Play ();
			checkMarkMusic.SetActive (true);
			print ("play audio");
		}
//		checkMark
	}
	


	public void Button_Click(Button btn)
	{
		if (btn.name == "play") 
		{
			Gamemanager.LoadGame ();
		}
		else if (btn.name == "settings") 
		{
			settingsPanel.SetActive (true);
		}
		else if (btn.name == "closeSettings") 
		{
			settingsPanel.SetActive (false);
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
			print ("play audio");
		}

		PlayerPrefs.SetInt ("bgMusic", canPlayMusic);
	}
}
