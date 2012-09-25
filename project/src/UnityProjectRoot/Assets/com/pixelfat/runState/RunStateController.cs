using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using com.pixelfat.mono;

namespace com.pixelfat.runStates {
	
	public class RunStateController : MonoEventHandler {
	
		public static RunStateController instance;

		public RunState[] runStates { get { return mRunStates.ToArray(); } }
		private List<RunState> mRunStates = new List<RunState>();
		
		public RunState nextRunState;
		public RunState previousRunState;
		public RunState currentRunState;
	
		#region Constructor
		public RunStateController(){ 
			
			instance = this;
	
			RunStateChangeEventMethods += ReportStateChange;
			
			//GoNext(new RunState_Begin());
			
		}
		#endregion
	
		public static RunStateController GetInstance(){
			if (instance==null)
				instance = new RunStateController();
					
			return instance;
		}
		
		public override void MonoUpdate ()
		{
			base.MonoUpdate ();
			if(currentRunState!=null)
				currentRunState.OnUpdate();
		}
		
		public void AddState(RunState runstate) {
			
			if(mRunStates.Contains(runstate)) {
			
				Debug.LogError("Runstate already added: " + runstate);
				return;
				
			}
			
			mRunStates.Add(runstate);
			
			runstate.OnAdded();
			
			Debug.Log("Run state added: " + runstate);
			
		}
		
		public void RemoveState(RunState runstate) {
			
			if(!mRunStates.Contains(runstate)) {
				
				Debug.LogError("Runstate not added: " + runstate);
				return;
				
			}
			
			mRunStates.Remove(runstate);

			runstate.OnRemoved();
			
			Debug.LogError("Runstate removed: " + runstate);
			
		}
		
		public bool GoNext(RunState runstate) {
			
//			if(!mRunStates.Contains(runstate))
//				AddState(runstate);
			
			nextRunState = runstate;
			
			if(nextRunState!=null) {
			
				return DoNextState();
				
			} else {
			
				Debug.LogError("No runstate to go to!");
				return false;
				
			}
			
		}
		
	    private bool GoNext()
	    {
			
			if(nextRunState!=null) {
			
				return DoNextState();
				
			} else {
			
				Debug.LogError("No runstate to go to!");
				return false;
				
			}
			
	
	    }

		private bool DoNextState() {
			
			if (currentRunState == nextRunState){
			    Debug.LogError(this.ToString() + ": ERROR: Already in state " + currentRunState.ToString());
				return false;				
			}
			
			if (nextRunState != null)
	        {
				Debug.Log("CHANGING TO: " + nextRunState);
				Debug.Log("CHANGING FROM: " + currentRunState);
				
				previousRunState =  currentRunState; 
				currentRunState = nextRunState;

				nextRunState = null;
				
				if (previousRunState!=null)
					previousRunState.OnDisabled();
				
				// fire disable event
				if(RunStateDisabledMethods!=null)
					RunStateDisabledMethods(previousRunState);
				
				currentRunState.OnEnabled();
				
				// fire enable event
				if(RunStateEnabledMethods!=null)
					RunStateEnabledMethods(currentRunState);
				
				// fire change event
				if(RunStateChangeEventMethods!= null)
					RunStateChangeEventMethods(currentRunState);
				
				
				
//				// report to consol (this may be delayed and NOT read out in order!)
//				if(previousRunState!=null)
//					Debug.Log (this.ToString() + ": RunState changed from " + previousRunState.ToString() + " to " + currentRunState.GetType().ToString());
//				else
//					Debug.Log (this.ToString() + ": RunState changed from null to " + currentRunState.GetType().ToString());
				
				return true;
				
			} else {

	            Debug.LogError(this.ToString() + ": ERROR: The next Runstate has not been defined!");
				
				return false;
				
	        }

		}
		
		public bool DoPreviousRunState() {
		
			if(previousRunState!=null) {
				return GoNext(previousRunState);
			} else {
				Debug.Log (this.ToString() + ": ERROR: No previous Runstate defined!");
				return false;
			}
			
		}

		private void ReportStateChange(RunState newRunState) {
			
//			if(previousRunState!=null)
//				Debug.Log (this.ToString() + ": State changed from " + previousRunState.ToString() + " to " + newRunState.GetType().ToString());
//			else
//				Debug.Log (this.ToString() + ": State changed to " + newRunState.GetType().ToString());
			
			Debug.Log("CURRENT RUNSTATE: " + currentRunState);
			
		}
		
		#region events
		public delegate void RunStateEventDelegate(RunState runState); // why not have them all inherit from this?
				
		public event RunStateEventDelegate RunStateChangeEventMethods;
				
		public event RunStateEventDelegate RunStateEnabledMethods;
		public event RunStateEventDelegate RunStateDisabledMethods;
		
		//public event RunStateEventDelegate UpdateRunStateMethods;
		#endregion events
		
	}
	
}