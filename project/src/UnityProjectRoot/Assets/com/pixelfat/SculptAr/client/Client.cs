using UnityEngine;
using System.Collections;

using com.pixelfat.runStates;
using com.pixelfat.sculptar.client.runStates;

public class Client : MonoBehaviour {
	
	public RunStateController _runStateController;
	
	// Use this for initialization
	void Start () {

		RunStateController _runStateController = new RunStateController();
		
		_runStateController.GoNext(new Sculpt());
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
