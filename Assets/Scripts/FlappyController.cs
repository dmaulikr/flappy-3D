using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyController : MonoBehaviour {

	public Transform path1;
	public Transform path2;

	Vector3 tempVector;

	Rigidbody rb;
	Animation anim;

	AudioSource audioSource;
	public AudioClip jumpClip;
	public AudioClip deadClip;

	float startX;

	bool pathFlag;
	int xForce = 10;
	int maxVelX = 2;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animation> ();
		audioSource = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		startX = transform.position.x;
		pathFlag = false;
		anim.Play ("Armature|Fly_001");
//		path1.
//		rb.AddForce (Vector3.up * 300);

	}

	// Update is called once per frame
	void Update () 
	{
//		print(UnityEditor.
//		rb.velocity= new Vector3(
		if (!Gamemanager.o.isGameOver) 
		{
			transform.position = new Vector3 (transform.position.x, Mathf.Clamp (transform.position.y, -5, 5), transform.position.z);
//			if (Input.GetKeyDown (KeyCode.A)) 
			if(Input.GetMouseButtonDown(0))
			{
				audioSource.PlayOneShot (jumpClip);
				tempVector = new Vector3 (rb.velocity.x, 0, 0);
				rb.velocity = tempVector;
				rb.AddForce (Vector3.up * 120);
			}
		}
		if (transform.position.x > startX + 124.9f)
		{
			if (pathFlag)
				path2.Translate (249.8f, 0, 0);
			else
				path1.Translate (249.8f, 0, 0);

			startX = transform.position.x;
			pathFlag = !pathFlag;
		}
		transform.eulerAngles = new Vector3 (-rb.velocity.y * 10, transform.eulerAngles.y, transform.eulerAngles.z);
//		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, rb.velocity.z * 10);
//		targetRotation = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, rb.velocity.y * 20);
//		transform.eulerAngles = Vector3.LerpUnclamped (transform.eulerAngles, targetRotation, 0.01f);
//		transform.eulerAngles= new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, rb.velocity.y * 10);
	}
	void FixedUpdate()
	{
		if (rb.velocity.x < maxVelX)
			rb.AddForce (Vector3.right * xForce);
	}

	void TapToStart()
	{
		
	}

	void OnCollisionEnter(Collision col)
	{
		if (!Gamemanager.o.isGameOver) 
		{
			maxVelX = 0;
			rb.velocity = Vector3.zero;
			float randX = Random.Range (70,120);
			float randY = Random.Range (100,150);
			float randZ = Random.Range (-100,100);
			rb.AddForce (new Vector3 (randX, randY, randZ));
			anim.Play ("Armature|Idol_fly");
			audioSource.PlayOneShot (deadClip);
			StartCoroutine (StopMe ());
			Gamemanager.o.GameOver ();
		}

	}

	IEnumerator StopMe()
	{
		yield return new WaitForSeconds (2);
//		rb.velocity = Vector3.zero;
		rb.isKinematic = true;
	}


	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "point" && !Gamemanager.o.isGameOver)
		{
			col.gameObject.SetActive (false);
			Gamemanager.o.GenerateHurdle ();
		}
	}
}
