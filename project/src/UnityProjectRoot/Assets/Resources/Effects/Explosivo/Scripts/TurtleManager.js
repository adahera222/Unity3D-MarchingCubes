
private var startPos:Vector3;
private var expMgr:ExplosionManager;
private var life:float;
private var lifeMax:float;
private var init = false;
private var cCount:int;
private var force:float;
private var dirRnd:float;
private var ri:int;

function Init(_expMgr:ExplosionManager, _childCount:int, _life:float, _dir:Vector3, _dirRnd:float, _force:float, _ri:int){
	//print(_childCount);
	cCount = (_childCount>1)?_childCount:0;//Random.Range(0,_childCount+1);
	expMgr = _expMgr;
	life = lifeMax = _life;
	force = _force;
	dirRnd = _dirRnd;
	startPos = transform.position;
	ri = _ri;
	transform.forward = _dir;
	transform.Rotate(0,0,Random.value*360,Space.Self);
	transform.Rotate(Random.value*_dirRnd,0,0,Space.Self);
	
	rigidbody.AddRelativeForce(0,0,_force,ForceMode.Impulse);
	
	init = true;
}

function FixedUpdate(){
	rigidbody.AddForce(expMgr.turtleGravity);
}

function Update() {
	if(!init)return;
	
	life-=Time.deltaTime;
	if(life<=0)KillMe();
	
	var lf = (cCount>0)?1.0:life/lifeMax;
	
	expMgr.Burst(transform.position, 2, Mathf.Clamp01(lf));
}

function KillMe () {
	if(ri<expMgr.maxRecursions){
		for(var i=0;i<cCount;i++){ 
			var l = (expMgr.turtleLife+Random.Range(-expMgr.turtleLifeRndPct,expMgr.turtleLifeRndPct))*expMgr.turtleLifeMulti;
			expMgr.SpawnTurtle(transform.position, rigidbody.velocity.normalized, expMgr.turtleDirRndMulti*dirRnd,cCount*expMgr.turtleCountMulti,rigidbody.velocity.magnitude*rigidbody.mass, (ri+1), l); //_pos:Vector3, _dir:Vector3, _dirRnd:float, _childCount:int
		}
	}
	Destroy(gameObject);
}