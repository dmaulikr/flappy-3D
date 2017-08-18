using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform player;
	Vector3 difference;
	Vector3 currentPos;

	// Use this for initialization
	void Start ()
	{
		//        currentPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		currentPos= transform.position;
		difference = transform.position - player.position;
	}

	// Update is called once per frame
	void Update () 
	{
		currentPos.x = player.position.x + difference.x;
		currentPos.y = player.position.y + 1;
//		currentPos.z = player.position.z + difference.y;
//		transform.position =Vector3.Lerp(transform.position, currentPos, 0.05f);
		transform.position= currentPos;

		transform.LookAt (new Vector3 (player.position.x, player.position.y, 0));
	}
}
