using UnityEngine;
using com.pixelfat.runStates;
using com.pixelfat.sculptar.client.guiComponents;

namespace com.pixelfat.sculptar.client.runStates {
	
	public class Sculpt : RunState
	{
		
		McBlock _mcBlock;
		
		public McField mcField;
		
		public Sculpt() {}

		public override void OnEnabled ()
		{
			
//			guiComponent = new ARConfigGuiComponent();
//		
//			guiComponent.WwwImageReplaceTrackerEventMethods += HandleGuiComponentWwwImageReplaceTrackerEventMethods;
//			guiComponent.PhotoReplaceTrackerEventMethods += HandleGuiComponentPhotoReplaceTrackerEventMethods;
//			guiComponent.ScaleEventMethods += SetScale;
			
			GameObject _mcFieldGO = new GameObject("MC Field");
			mcField = _mcFieldGO.AddComponent<McField>();
			
			mcField.blockMaterial = Resources.Load("triplanar") as Material;
			
//			ImageTargetBehaviour _tracker = GameObject.FindObjectOfType(typeof(ImageTargetBehaviour)) as ImageTargetBehaviour;
//			
//			if(_tracker!=null) {
//				
//				_mcFieldGO.transform.parent = _tracker.transform;
//				_mcFieldGO.transform.position = new Vector3(0,0,-0.5f);
//				
//			}
			
			base.OnEnabled ();
			
		}
		
		public override void OnUpdate ()
		{

			
		}
		
		void SetScale(float scale) {
			
			_mcBlock.transform.localScale = new Vector3(scale,scale,scale);
		}
		
	}
	
}