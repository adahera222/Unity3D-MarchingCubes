/// <summary>
/// GUI component controller.
/// Super simple controller for use with managing native GUI rendering in Unity
/// Not for publication or use in client projects!
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using com.pixelfat.mono;

namespace com.pixelfat.simpleGuiController {
	
	public class GuiComponentController : MonoEventHandler // mono used to recieve OnGUI() events
	{

		#region Singleton
		private static GuiComponentController _instance = null;
	    public static GuiComponentController instance
	    {
	        get
	        {
	            if (_instance == null)
	            {
					Debug.Log("GuiComponentController: Creating new instance.");
	                _instance = new GuiComponentController();

	            }
	
	            return _instance;
				
	        }
			
	    }
		#endregion
		
		public static GUISkin DefaultGuiSkin;
		
		#region Constructor
		public GuiComponentController() : base() {

			if(_instance!=null)
				Debug.LogWarning("There is already an instance of GuiComponentController, use GuiComponentController.instance to access it!");
			
		}
		#endregion
		
		#region GUI Components
		public static List<GuiComponent> components { get { return mComponents; } set {mComponents = value;} }
		private static List<GuiComponent> mComponents = new List<GuiComponent>(); // all components (active or inactive)
		
		bool skipRenderThisFrame = false;
		
		private void RegisterComponent(GuiComponent GUIComponent) {
	
	        components.Add(GUIComponent);
			
	        Debug.Log(this + ": GUI Component: " + GUIComponent + " registered");
	
	    }
	
	    private void UnregisterComponent(GuiComponent GUIComponent)
	    {
	        int componentIndex = components.IndexOf(GUIComponent);
			
			if(componentIndex==-1) {
				
				Debug.LogError("Invalid GUI component reference, the supplied component is not a registered GUI component.");
				Debug.Log (this + ": GUI Component: " + GUIComponent + " not found");
				
				return;
				
			} else {
				
				components.RemoveAt(componentIndex);
				 Debug.Log (this + ": GUI Component: " + GUIComponent + " unregistered");
				
			}
			
	    }
	
		public void Add(GuiComponent GUIComponent) {
			
			if(components.Contains(GUIComponent)) {
				
				Debug.LogError (this + ": GUI Component: " + GUIComponent + " allready added");
				return;
				
			}
			
			RegisterComponent(GUIComponent);
			
			skipRenderThisFrame = true;
			
			Debug.Log (this + ": GUI Component: " + GUIComponent + " added");
			
		}
		
		public void Remove(GuiComponent GUIComponent) {
		
			if(!components.Contains(GUIComponent)) {
				
				Debug.LogError (this + ": GUI Component: " + GUIComponent + " was not a part of the gui controller");
				return;
				
			}
			
			UnregisterComponent(GUIComponent);
			
			skipRenderThisFrame = true;
			
			//Destroy((MonoBehaviour)GUIComponent);
			
			Debug.Log (this + ": GUI Component: " + GUIComponent + " removed");
			
		}
		
		public void RemoveAll() {
			
			components.Clear();
			
			skipRenderThisFrame = true;
			
			Debug.LogWarning (this + ": All GUI components removed");
			
		}
		
//		public T New<T>() where T : GuiComponent
//		{
//			
//			T _newGUIComponent = new (T();// gameObject.AddComponent(typeof(T)) as T;
//			
//			RegisterComponent((GuiComponent)_newGUIComponent);
//			
//			return _newGUIComponent;
//			
//		}
		
		#endregion
		
		#region Rendering
		public override void MonoGUI() { OnGUI(); }
		void OnGUI() {
		
			// sort here or in Update()?
			
			if(skipRenderThisFrame)
				return;
			
			try{
				
				if(DefaultGuiSkin==null)
					DefaultGuiSkin = UnityEngine.GUI.skin;
				
				foreach(GuiComponent _component in components) {
					
					if(_component.visible) {
						
						if(_component.GuiSkin!=null)
							UnityEngine.GUI.skin = _component.GuiSkin;
						else
							UnityEngine.GUI.skin = DefaultGuiSkin;
						
						_component.Render();

					}
				}
				
			} catch {
				Debug.LogWarning("Could not render gui this frame");
			}
		}
		
		#endregion
		
		#region Updating
		public override void MonoUpdate() { Update(); }
		void Update() {
			
			try{
				foreach(GuiComponent _component in components)
					_component.Update();
			} catch {
				Debug.LogWarning("Could not update gui components this frame");	
			}
			
			skipRenderThisFrame = false;
			
		}
		
		#endregion
		
	//	void Update() {
	//	
	//		// remove old components
	//		ScrubComponents();
	//		
	//	}
	//		private void ScrubComponents() {
	//	
	//		foreach(GuiComponent _component in components) {
	//		
	//			if(_component.removeNextFrame)
	//				Remove(_component);
	//			
	//		}
	//		
	//	}
	}
	
}
