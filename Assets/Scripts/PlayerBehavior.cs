using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	[Header("移動速度")]
	public float speed = 0.025f;
	public GameObject player;
	public GameObject bullet;
	public Collider2D playerHitBox;
	public LayerMask groundLayer;
	public Transform groundCheck;

	[Header("主角到地板的距離")]
	[Range (0,5f)]
	public float distance;
	public Collider2D[] attackHitBoxes;
	private HitBoxBehavior hitBoxBehavior;
	private Rigidbody2D playerRigidBody;
	private float frame;
	private float FireClock;
	private bool ifFired;

	private bool isGrounded ()
	{
		Vector2 start = groundCheck.transform.position;
		Vector2 end = new Vector2(start.x,start.y - distance);
		Debug.DrawLine(start,end,Color.blue);
		return Physics2D.Linecast(start,end,groundLayer);
	}
	private void Fire()
	{
		GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
		Rigidbody tempRigidBodyBulllet = tempBullet.GetComponent<Rigidbody>();
		tempRigidBodyBulllet.AddForce(tempRigidBodyBulllet.transform.right * 10000f);
		Destroy(tempBullet, 1f);
	}
	private bool previousUpInput;
	private void Move()
	{
		playerRigidBody = player.GetComponent<Rigidbody2D>();

		float MoveSpeed_ver = 0f;
		float MoveSpeed_hor = 0f;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			Jump();
			jumpingCalcForce += (Physics2D.gravity.y);
			Debug.Log(jumpingCalcForce);
		}
		else if(!Input.GetKey(KeyCode.UpArrow) && previousUpInput)
		{
			if(jumpingCalcForce > 0)playerRigidBody.AddForce(gameObject.transform.up * -(jumpingCalcForce));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
 			MoveSpeed_ver = -this.speed;
			MoveSpeed_hor = 0f;
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			MoveSpeed_hor = -this.speed;
			MoveSpeed_ver = 0f;
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			MoveSpeed_hor = this.speed;
			MoveSpeed_ver = 0f;
		}
		previousUpInput = Input.GetKey(KeyCode.UpArrow);

		transform.Translate(MoveSpeed_hor,MoveSpeed_ver,0);
		
	}
	private bool isJumping;
	private int jumpingFrame;
	private const float jumpingForce = 300f;
	private const float jumpingAddingForce = 0f;
	private float jumpingCalcForce;
	private void Jump()
	{
		if(isGrounded() && !isJumping)
		{
			// Debug.Log("jump");
			playerRigidBody.AddForce(gameObject.transform.up * jumpingForce);
			jumpingFrame = 0;
			jumpingCalcForce = jumpingForce;
			isJumping = true;
		}
		else
		{
			jumpingFrame++;
			if(jumpingFrame < 20) 
			{
				playerRigidBody.AddForce(gameObject.transform.up * jumpingAddingForce);
				jumpingCalcForce += jumpingAddingForce;
			}
			// else Debug.Log("done");
			isJumping = false;
		}
	}
	private float attackStartTime;
	private float attackTimer;
	private float[] attackCoolDown = new float[] {0.3f,0.3f,0.5f};
	private int attackCombo = 0;
	private bool attackFlag = false;
	private UnityEngine.KeyCode attackKey = KeyCode.X;
	private void Attack(Collider2D col)
	{
		if(attackFlag)
		{
			attackTimer = Time.time - attackStartTime;
			if(attackTimer > 2)
			{		
				attackTimer = 0;
				attackCombo = 0;
				attackFlag = false;
			}
		}
		if(!Input.GetKey(attackKey)) return;

		if(!attackFlag)
		{
			attackFlag = true;
			attackTimer = attackCoolDown[0] + 1f;
			attackStartTime = Time.time;
		}
		if(attackTimer < attackCoolDown[attackCombo]) return;
		attackCombo++;
		attackStartTime = Time.time;
		
		
		Collider2D[] cols = Physics2D.OverlapBoxAll(col.bounds.center,col.bounds.extents,0f,LayerMask.GetMask("Enemy"));
		foreach(Collider2D c in cols)
		{
			if(c.transform.parent.parent == transform) continue;
			float damage;

			switch(attackCombo)
			{
				case 1:
				damage = 20;
				break;
				case 2:
				damage = 30;
				break;
				case 3:
				damage = 40;
				attackCombo = 0;
				break;
				default:
				attackCombo = 0;
				damage = 0;
				break;
			}

			// switch(c.name)
			// {
			// 	case "Cube":
			// 	damage = 20;
			// 	break;
			// 	default:
			// 	Debug.Log("Unable to identify body part, make sure the name matches the switch case");
			// 	break;
			// }

			c.SendMessage("TakeDamage",damage);
			// Debug.Log(attackCombo);
		}

	}
	private void Defend()
	{
		if(!Input.GetKey(KeyCode.C)) return;
		

	}
	// Use this for initialization
	void Start () {
		ifFired = false;
	}
	// Update is called once per frame
	void Update () {
		frame++;
		if(frame > 60)frame = 0;

		Move();
		Attack(attackHitBoxes[0]);
		Defend();

		// if(ifFired)
		// {
		// 	FireClock++;
		// 	if(FireClock > 60) 
		// 	{
		// 		FireClock = 0;
		// 		ifFired = false;
		// 	}
		// }
		// if(Input.GetKey(KeyCode.Z) && !ifFired)
		// {
		// 	Fire();
		// 	ifFired = true;
		// }
	}
}
