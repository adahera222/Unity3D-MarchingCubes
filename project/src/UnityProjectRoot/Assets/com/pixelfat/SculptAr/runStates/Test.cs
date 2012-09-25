/// <summary>
/// Initialise.
/// does loading of possible asset bundles
/// </summary>
using UnityEngine;
using System.Collections;

using com.pixelfat.runStates;
using com.pixelfat.simpleGuiController;

using com.pixelfat.sculptar.client.guiComponents;

namespace com.pixelfat.sculptar.client.runStates {
	
	public class Test : RunState
	{

		public Test() {

			TestGuiComponent _testGuiComponent = new TestGuiComponent();
		
			Debug.Log(_testGuiComponent + " added");
			
		}
		
	}
	
}