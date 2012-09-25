using UnityEngine;
using System.Collections;

public class McCubeInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	McBlock _mcBlock;
	// Update is called once per frame
	void Update () {
		
		if(Input.touchCount>0 || Input.GetMouseButton(1)) {
				
			RaycastHit  hit;
			Ray ray;
			
			if(Input.touchCount>0)
				ray = Camera.main.ScreenPointToRay(Input.touches[0].position );
			else
				ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast (ray, out hit )) {
					
				if(hit.collider.gameObject.GetComponent<McBlock>() != null ) {
				
					_mcBlock = hit.collider.gameObject.GetComponent<McBlock>();
					
					Vector3 _a = _mcBlock.transform.InverseTransformPoint(hit.point) + (Vector3.one * 0.5f);
					
					McBlock.mcPoint _b = _mcBlock.getPoint( Mathf.RoundToInt(_a.x * _mcBlock.dimX), Mathf.RoundToInt(_a.y * _mcBlock.dimY), Mathf.RoundToInt(_a.z * _mcBlock.dimZ));
					
					Debug.Log("Block: " + _mcBlock.name + " was clicked at point: " + _b.px + "," + _b.py + "," + _b.pz);
					
					
				}	
			}
		}
	}
}
