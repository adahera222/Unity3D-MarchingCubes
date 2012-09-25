/// <summary>
/// just an example simple GUI component.
/// </summary>
using UnityEngine;

using System.Collections;
using System.Collections.Generic;

using com.pixelfat.simpleGuiController;

namespace com.pixelfat.sculptar.client.guiComponents {
	
	public class TestGuiComponent : GuiComponent
	{
	
		public override void Render() {
		
			RenderSomething();
			RenderSomethingElse();
			
		}
		
		private void RenderSomething() {
			
			GUILayout.Label("Something");
			
			if(GUILayout.Button("A button"))
				if(ExampleGuiComponentEventMethods!=null)
					ExampleGuiComponentEventMethods();
			
		}
		
		private void RenderSomethingElse() {
		
			GUILayout.Label("Something else");
			
		}
		
		#region events
		public delegate void ExampleGuiComponentEventDelegate();
		public static event ExampleGuiComponentEventDelegate ExampleGuiComponentEventMethods;
		#endregion
	
	}
	
}
