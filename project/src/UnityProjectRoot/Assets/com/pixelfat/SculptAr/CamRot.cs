using UnityEngine;
using System.Collections;

public class CamRot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	Vector2 _delta;
	
	// Update is called once per frame
	void Update () {
		
		transform.RotateAround(Vector3.zero, transform.up, .1f);
		transform.RotateAround(Vector3.zero, -transform.right, .1f);
		
	}
}
