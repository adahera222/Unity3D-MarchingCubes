using UnityEngine;
using System.Collections;

using com.pixelfat.mono;

namespace com.pixelfat.runStates {
	
	public class RunState
	{
	
		public RunState(){
			
	        Debug.Log (this.ToString() + ": constructor fired.");
			
		}
		
		#region EventHandlers
		public virtual void OnUpdate() {}
		protected virtual void OnDestroy(){}
		public virtual void OnEnabled () {}
		public virtual void OnDisabled () {}
		
		public virtual void OnAdded () {}
		public virtual void OnRemoved () {}
		#endregion
		
	}
	
	// default first class
	public class RunState_Begin : RunState
	{
	
		public RunState_Begin() {
			
			//next = typeof(RS_GetTestConfig);
			//GoNext();
			
		}
	
		
	}
	
}