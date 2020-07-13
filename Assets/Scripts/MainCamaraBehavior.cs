using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamaraBehavior : MonoBehaviour {

	public Transform target;
	[Range(0,10f)]
	public float distanceX;
	public float cameraSpeed;
	public PlayerBehavior player;

	// Use this for initialization
	void Start () {
		Debug.Log(player.speed);
	}
	
	// Update is called once per frame
	void Update () {
		float offsetX = gameObject.transform.position.x - target.transform.position.x;
		float offsetY = gameObject.transform.position.y - target.transform.position.y;
		if(offsetX > distanceX)
		{
			if(player.getSprintMode()) gameObject.transform.Translate(-player.speed*2,0f,0f);
			else gameObject.transform.Translate(-player.speed,0f,0f);
		}
		else if(offsetX < -distanceX)
		{
			if(player.getSprintMode()) gameObject.transform.Translate(player.speed*2,0f,0f);
			else gameObject.transform.Translate(player.speed,0f,0f);
		}

		
		if(offsetY > 7.5f)
		{
			gameObject.transform.Translate(0f,-player.speed,0f);
		}
		else if(offsetY < -7.5f)
		{
			gameObject.transform.Translate(0f,player.speed,0f);
		}
	}
}
