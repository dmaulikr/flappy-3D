using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		StartCoroutine (Shrink ());
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.tag == "player") 
		{
			StartCoroutine (Shrink ());
		}
		
	}
	float shrinkFactor = 0.05f;
	IEnumerator Shrink()
	{
		Transform child = transform.GetChild (1);

		while (child.localScale.z >= 0) 
		{
			yield return new WaitForSeconds (0.05f);
			child.localScale = new Vector3 (child.localScale.x, child.localScale.y, child.localScale.z - shrinkFactor);
			child.position = new Vector3 (child.position.x, child.position.y, child.position.z + shrinkFactor * 17.35f);
		}
		child.localScale = new Vector3 (child.localScale.x, child.localScale.y, 0);
	}
}
