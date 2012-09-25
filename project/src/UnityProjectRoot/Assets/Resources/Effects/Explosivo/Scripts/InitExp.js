var expPrefab:Transform;

function Update () {
	if(Input.GetMouseButtonDown(0)){
		var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Instantiate (expPrefab, ray.direction * 7, Quaternion.identity);
	}
}