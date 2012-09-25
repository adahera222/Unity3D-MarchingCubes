// used so classes that don't inherit from monobehavior can still do tasks 
// during mono events by inheriting from MonoEventHandler
using UnityEngine;
using System.Collections;

namespace com.pixelfat.mono {

	public class MonoEventController {
		
		public static GameObject gameObject;
		public static MonoEventRelay eventRelayObject;
		
		private static MonoEventController _instance;
		
		public static MonoEventController instance
	    {
	        get
	        {
	            if (_instance == null)
	            {
					Debug.Log("MonoEventController: Creating new instance.");
					_instance = new MonoEventController();
	            }
				
	            return _instance;
				
	        }
	    }
		
		public MonoEventController() {
			
			Debug.Log(this + ": Creating game object to relay mono events.");
			
			gameObject = new GameObject("Mono Event Reciever");
			
			eventRelayObject = gameObject.AddComponent(typeof(MonoEventRelay)) as MonoEventRelay;
			
		}
		
		//public void ThrowStartEvent() { if(Start!=null){ Start();} }
		public void ThrowUpdateEvent() { if(Update!=null){ Update();} }
		public void ThrowGuiEvent() { if(OnGUI!=null){ OnGUI();} }
		
		public delegate void MonoEventDelegate();
		
		//public static event MonoEventDelegate Start;
		public static event MonoEventDelegate Update;
		public static event MonoEventDelegate OnGUI;
		
		public void RemoveMonoEventListeners() {
			
		}
		
	}

	// inherit from this class to recieve mono events
	public class MonoEventHandler {
	
		MonoEventController monoEventController;
		
		private bool enabled = false;

		public MonoEventHandler() {		
			//MonoEventController controller = MonoEventController.Instance; 
			AddMonoEventListeners();
		}
		
		public void AddMonoEventListeners() {
			
			monoEventController = MonoEventController.instance;
			
			if (enabled==false && monoEventController!=null){
				MonoEventController.OnGUI+=MonoGUI;
				MonoEventController.Update+=MonoUpdate;
			}
			
			enabled = true;
			
		}
		
		public void RemoveMonoEventListeners() {
			
			monoEventController = MonoEventController.instance;
			
			if (enabled == true && monoEventController!=null){
				MonoEventController.OnGUI-=MonoGUI;
				MonoEventController.Update-=MonoUpdate;
			}
			
			enabled = false; 
			
		}
		
		public bool IsEnabled(){
			return enabled;		
		}
		
		public void enable(){
			OnEnable();
			AddMonoEventListeners();
		}
				
		public void disable(){
			OnDisable();
			RemoveMonoEventListeners();
		}
		
		public void ApplyOnEnable(){
			OnEnable(); 
		}
		
		public void ApplyOnDisable(){
			OnDisable(); 
		}
		
		protected virtual void OnEnable(){;}
		protected virtual void OnDisable(){;}
			
		public virtual void MonoGUI(){;}
		public virtual void MonoUpdate(){;}
		
	}
	
}