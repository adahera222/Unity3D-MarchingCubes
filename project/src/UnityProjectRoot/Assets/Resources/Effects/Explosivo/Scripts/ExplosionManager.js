
//turtle vars
public var turtleCount = 5;
public var turtleCountMulti = .5;
public var maxRecursions = 3;
public var turtleLife = 2.0;
public var turtleLifeRndPct = 0.25;
public var turtleLifeMulti = 1.0;
public var turtleForce = 10.0;
public var turtleDirection = Vector3.up;
public var tutleDirectionRndAngle = 90.0;
public var turtleDirRndMulti = .5;
public var turtleGravity = Vector3(0,-1,0);
public var pSizeMin = .3;
public var pSizeMax = .8;

//particle vars

//private vars
private var turtleIndex = 0;
private var pLife:float;

function Awake () {
	
	pLife = particleEmitter.minEnergy;
	
	
	for(var i=0;i<turtleCount;i++){
		//var dir = turtleDirection
		//Quaternion
		SpawnTurtle(transform.position, turtleDirection, tutleDirectionRndAngle,turtleCount*turtleCountMulti, turtleForce,1, turtleLife+Random.Range(-turtleLifeRndPct,turtleLifeRndPct));
	}
	
	CheckParticles();
	
}

function Update () {
	//Burst (transform.position, 1, 1);
}

function Burst (_pos:Vector3, _pCount:int, _lifeFactor:float) {
	
	for(var i=0;i<_pCount;i++){
		//if(_lifeFactor<=0 || _lifeFactor>1)print(_lifeFactor);
		var vel = Vector3(Random.Range(-1.0,1.0),Random.Range(-1.0,1.0),Random.Range(-1.0,1.0)).normalized*_lifeFactor*0.2;
		//var size = Random.Range(0.3,0.8);
		var size = Random.Range(pSizeMin,pSizeMax) * (_lifeFactor+.2);
		particleEmitter.Emit(_pos, vel, size, pLife, Color.white);
	}
	
}

function CheckParticles(){
	yield new WaitForSeconds(1);
	
	while(particleEmitter.particleCount>0){
		yield;
	}
	Destroy(gameObject);
	/*
	yield new WaitForSeconds(){
		
	}
	*/	
}
function SpawnTurtle (_pos:Vector3, _dir:Vector3, _dirRnd:float, _childCount:int, _force:float, _ri:int, _life:float) {
	var turtle:GameObject = new GameObject("turtle"+turtleIndex);
	//turtle.tag = "Turtle";
	turtle.transform.position = _pos;
	turtle.AddComponent(Rigidbody);
	turtle.rigidbody.drag = 0.5;
	//turtle.rigidbody.mass = 0.3;
	/*
	turtle.AddComponent(SphereCollider);
	var cols = GameObject.FindGameObjectsWithTag("Turtle");
	for(var col:GameObject in cols){
		if(col!=turtle)Physics.IgnoreCollision(turtle.collider, col.collider);	
	}
	*/
	turtle.rigidbody.useGravity = false;
	InitTurtle(turtle,_dir, _dirRnd,_childCount, _force, _ri, _life);
	turtleIndex++;
}
function InitTurtle(_turtle:GameObject, _dir:Vector3, _dirRnd:float, _childCount:int, _force:float, _ri:int, _life:float){
	yield; // !m_DidAwake fix
	var turtleMgr:TurtleManager = _turtle.AddComponent("TurtleManager");
	turtleMgr.Init(transform.GetComponent("ExplosionManager"),_childCount, _life, _dir, _dirRnd, _force, _ri);
}