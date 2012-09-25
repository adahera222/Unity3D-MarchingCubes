using UnityEngine;
using System.Collections;

namespace com.pixelfat.mono {
	
	public class MonoEventRelay : MonoBehaviour {
		
		public MonoEventController eventRelay;
		
		void Start() { eventRelay = MonoEventController.instance; }
		
		void OnGUI() { MonoEventController.instance.ThrowGuiEvent(); }
	
		void Update() { MonoEventController.instance.ThrowUpdateEvent(); }
		
	}
	
}
