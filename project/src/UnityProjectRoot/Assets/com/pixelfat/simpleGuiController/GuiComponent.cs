using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.pixelfat.simpleGuiController {
	
	public class GuiComponent
	{
			
		public GUISkin GuiSkin;
		
		// visible (to render or not to render..)
		bool _visible = true;
		public bool visible { get { return _visible; } set {;} }
		public void Hide() { visible = false; }
		public void Show() { visible = true; }
		
		private bool _removeNextFrame = false;
		public bool removeNextFrame { get {return _removeNextFrame;} set {;} } // use Remove() to set to true;
		
		public List<GuiComponent> subComponents { get {return _subComponents; } }
		List<GuiComponent> _subComponents = new List<GuiComponent>(); // other gui components used by this one (alert boxes, etc)
		
		public virtual void Render(){}
		
		public virtual void Update(){ UpdateGuiComponent();}
		
		public GuiComponent() {
			
			// register with GuiCOmponentController
			GuiComponentController.instance.Add(this);
			
		}
		
		public void Remove() {
		
			_removeNextFrame = true;
			
		}
		
		void UpdateGuiComponent() {
		
			if(removeNextFrame) {
			
				GuiComponentController.instance.Remove(this as GuiComponent);
				
			}
			
		}

	}
	
}
