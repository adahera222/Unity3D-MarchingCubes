using UnityEngine;
using System.Collections;
using com.pixelfat.noise;

public class McField : MonoBehaviour {
	
	public McValues values;
	
	public McBlock[,,] blocks;
	
	public int BlockCountX = 8;
	public int BlockCountY = 8;
	public int BlockCountZ = 8;
	
	public int BlockResX = 4;
	public int BlockResY = 4;
	public int BlockResZ = 4;
	
	public int sizeX;
	public int sizeY;
	public int sizeZ;
	
	public Vector3 mid;
	
	public Material blockMaterial;
	private bool materialsSet = false;
	
	// Use this for initialization
	void Start () {

		Init();
		
	}
	
	void Init() {
	
		int x;
		int y;
		int z;
				
		blocks = new McBlock[BlockCountX,BlockCountY,BlockCountZ];
		
		values = new McValues(this);
		
		McBlock _mcBlock;
		GameObject _mcBlockGO;
		
		sizeX = BlockCountX * BlockResX;
		sizeY = BlockCountY * BlockResY;
		sizeZ = BlockCountZ * BlockResZ;
		
		float _h = (1 / BlockCountX);
		_h += (_h*0.5f);
		
		mid = new Vector3( (1f / sizeX) / 2, (1f / sizeY) / 2, (1f / sizeZ) / 2);
		
		for(x=0;x<BlockCountX;x++) {
			for(y=0;y<BlockCountY;y++) {
				for(z=0;z<BlockCountZ;z++) {
			
					_mcBlockGO = new GameObject("MC Block: " + x + "," + y + "," + z);
					_mcBlockGO.transform.parent = transform;
					
					_mcBlock = _mcBlockGO.AddComponent<McBlock>();
					_mcBlock.mcField = this;

					blocks[x,y,z] = _mcBlock;
					
					_mcBlock.posX = x;
					_mcBlock.posY = y;
					_mcBlock.posZ = z;

					_mcBlock._dimX = BlockResX;
					_mcBlock._dimY = BlockResY;
					_mcBlock._dimZ = BlockResZ;

					_mcBlock.transform.position = new Vector3((1f / BlockCountX) * x,(1f / BlockCountY) * y,(1f / BlockCountZ) * z);
					_mcBlock.transform.position += Vector3.one * (1f / BlockCountX) * 0.5f;
					_mcBlock.transform.position -= Vector3.one * 0.5f;
					
					_mcBlock.transform.position += transform.position;
					
					_mcBlock.transform.localScale = new Vector3(1f/BlockCountX,1f/BlockCountY,1f/BlockCountZ);
					
				}
			}
		}
		
	}
	
	public void SetValue(McBlock mcBlock, int px, int py, int pz, float val) {
		
		values.SetVal(mcBlock, px, py, pz, val);

		if( pz > mcBlock.dimZ -2)
			if(mcBlock.posZ < BlockCountZ-1)
				blocks[mcBlock.posX, mcBlock.posY, mcBlock.posZ +1].RequiresUpdate = true;
		
		if( pz < 2)
			if(mcBlock.posZ > 0)
				blocks[mcBlock.posX, mcBlock.posY, mcBlock.posZ -1].RequiresUpdate = true;
				
		if( py > mcBlock.dimY-2)
			if(mcBlock.posY < BlockCountY-1)
				blocks[mcBlock.posX, mcBlock.posY +1, mcBlock.posZ].RequiresUpdate = true;
		
		if( py < 2)
			if(mcBlock.posY > 0)
				blocks[mcBlock.posX, mcBlock.posY -1, mcBlock.posZ].RequiresUpdate = true;
		
		if( px > mcBlock.dimX-2)
			if(mcBlock.posX < BlockCountX-1)
				blocks[mcBlock.posX +1, mcBlock.posY, mcBlock.posZ].RequiresUpdate = true;
		
		if( px < 2)
			if(mcBlock.posX > 0)
				blocks[mcBlock.posX -1, mcBlock.posY, mcBlock.posZ].RequiresUpdate = true;
		
		mcBlock.RequiresUpdate = true;
		
	}
	
	public void GetValue(McBlock mcBlock, int px, int py, int pz) {
		
		values.GetVal(mcBlock, px, py, pz);

	}
	
	public class McValues {
		
		McField mcField;
		
		public float [,,] values;
		
		int _x;
		int _y;
		int _z;
		
		int _rezX = mcField.BlockCountX * mcField.BlockResX;
		int _rezY = mcField.BlockCountY * mcField.BlockResY;
		int _rezZ = mcField.BlockCountZ * mcField.BlockResZ;
		
		public enum INITIAL_SHAPE { SPHERE, BOX }
		public INITIAL_SHAPE shape = INITIAL_SHAPE.SPHERE;
		
		public McValues(McField mcField) {
			
			// values are 2units bigger than the actual field, so surrounding values are zero;
			
			//PerlinNoiseI _noiseGenerator = new PerlinNoiseI(123);
			//double [,,] _dvalues = _noiseGenerator.Generate3D(_rezX+1, _rezY+1, _rezZ+1, 0, 0, 0);
			
			this.mcField = mcField;
			
			values = new float[_rezX+1, _rezY+1, _rezZ+1];
			
			for(_x=0;_x<_rezX;_x++) {
				
				for(_y=0;_y<_rezY;_y++) {
					
					for(_z=0;_z<_rezZ;_z++) {
						
						// if it's around the edge, set it to zero
						if(_x == 0 || _y == 0 || _z == 0 ||
						   _x == _rezX || _y == _rezY || _z == _rezZ )
							values[_x,_y,_z] = 0f;
						else {
							
							if(shape==INITIAL_SHAPE.SPHERE) {
								
								float _mag = (new Vector3(_x, _y, _z) - new Vector3(_rezX/2, _rezY/2, _rezZ/2)).magnitude;
								
								float _pwr = _mag / (_rezX - 2); // -2 is for the border
								
								if(_pwr < 0.4f)
									_pwr = 1.0f;
								
								values[_x,_y,_z] = 1-_pwr; // reverse value
								
								// values[_x,_y,_z] += Random.Range(.0f,.01f);
								
							} else {
							
								values[_x,_y,_z] = Random.Range(.5f,1f);
								
							}
							
						}
						
					}
				
				}
				
			}
			
		}
		
		public void SetVal(McBlock mcBlock, int pointPosX, int pointPosY, int pointPosZ, float val) {
			
			_x = mcField.BlockResX * mcBlock.posX;
			_x += pointPosX;
			
			_y = mcField.BlockResY * mcBlock.posY;
			_y += pointPosY;
			
			_z = mcField.BlockResZ * mcBlock.posZ;
			_z += pointPosZ;
			
			values[_x,_y,_z] = val;
			
		}
		
		public float GetVal(McBlock mcBlock, int pointPosX, int pointPosY, int pointPosZ) {

			_x = mcField.BlockResX * mcBlock.posX;
			_x += pointPosX;
			
			_y = mcField.BlockResY * mcBlock.posY;
			_y += pointPosY;
			
			_z = mcField.BlockResZ * mcBlock.posZ;
			_z += pointPosZ;
			
			return values[_x,_y,_z];
			
		}
		
	}
	
	McBlock _mcBlock;
	void Update () {
		
		if(materialsSet == false && blockMaterial!=null) {
			
			foreach(McBlock _block in blocks) 
				if(_block.renderer!=null) {
				
				_block.renderer.material = blockMaterial;
				materialsSet = true;
				
			}
		}
		
		if(Input.touchCount>0 || Input.GetMouseButton(0)) {
				
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
					
					//Debug.Log("Block: " + _mcBlock.name + " was clicked at point: " + _b.px + "," + _b.py + "," + _b.pz + " - "+ _b.i ());
					
					int _x = Mathf.RoundToInt(_a.x * _mcBlock.dimX);
					int _y = Mathf.RoundToInt(_a.y * _mcBlock.dimY);
					int _z = Mathf.RoundToInt(_a.z * _mcBlock.dimZ);
					
					SetValue(_mcBlock, _x, _y, _z, _b.i () * 0.98f);
					
					Debug.DrawLine(Camera.main.transform.position, hit.point);
					
					GameObject _exlplodePrefab = Resources.Load("Explosivo/Explosion") as GameObject;
					GameObject.Instantiate(_exlplodePrefab, hit.point, Quaternion.identity);
					//_mcBlock.RequiresUpdate = true;
					
				}	
			}
		}
	}
	
}
