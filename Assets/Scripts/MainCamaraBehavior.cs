using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamaraBehavior : MonoBehaviour {

	public Transform target;
	[Range(0,10f)]
	public float distanceX;
	public float cameraSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		float offsetX = gameObject.transform.position.x - target.transform.position.x;
		float offsetY = gameObject.transform.position.y - target.transform.position.y;
		if(offsetX > distanceX)
		{
			gameObject.transform.Translate(-cameraSpeed,0f,0f);
		}
		else if(offsetX < -distanceX)
		{
			gameObject.transform.Translate(cameraSpeed,0f,0f);
		}

		
		if(offsetY > 7.5f)
		{
			gameObject.transform.Translate(0f,-cameraSpeed,0f);
		}
		else if(offsetY < -7.5f)
		{
			gameObject.transform.Translate(0f,cameraSpeed,0f);
		}
	}
}
