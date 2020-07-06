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
	private const UnityEngine.KeyCode FIRE_KEY = KeyCode.X;
	private const float BULLET_FORCE = 300f;
	private const float FIRE_CD = 0.3f;
	private float fireTimer;
	private void Fire()
	{
		if(!Input.GetKey(FIRE_KEY)) return;

		if(Time.time - fireTimer < FIRE_CD) return;
		fireTimer = Time.time;

		float initBulletPosX = 0f;
		if(isFaceRight) initBulletPosX = 0.2f;
		else initBulletPosX = -0.2f;

		GameObject tempBullet = Instantiate(bullet, new Vector3(gameObject.transform.position.x + initBulletPosX, gameObject.transform.position.y, gameObject.transform.position.z) ,Quaternion.identity) as GameObject;
		Rigidbody2D tempRigidBodyBulllet = tempBullet.GetComponent<Rigidbody2D>();

		// tempRigidBodyBulllet.AddForce(tempRigidBodyBulllet.transform.right * 0.01f);
		if(isFaceRight) tempRigidBodyBulllet.AddForce(tempRigidBodyBulllet.transform.right * BULLET_FORCE);
		else tempRigidBodyBulllet.AddForce(-tempRigidBodyBulllet.transform.right * BULLET_FORCE);
		
		Destroy(tempBullet, 2);
	}
	private bool previousUpInput;
	private bool isFaceRight;
	private UnityEngine.KeyCode previousInputKey;
	private float sprintTimer;
	private const float SPRINT_CD = 0.1f;
	private bool sprintMode;
	private void Move()
	{
		if(!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
		{
			return;
		}

		playerRigidBody = player.GetComponent<Rigidbody2D>();

		float MoveSpeed_ver = 0f;
		float MoveSpeed_hor = 0f;
		if(Input.GetKey(KeyCode.UpArrow))
		{
			Jump();
			jumpingCalcForce += (Physics2D.gravity.y);
			previousInputKey = KeyCode.UpArrow;
			// Debug.Log(jumpingCalcForce);
		}
		else if(!Input.GetKey(KeyCode.UpArrow) && previousUpInput)
		{
			if(jumpingCalcForce > 0)playerRigidBody.AddForce(gameObject.transform.up * -(jumpingCalcForce));
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
 			MoveSpeed_ver = -this.speed;
			MoveSpeed_hor = 0f;
			previousInputKey = KeyCode.DownArrow;
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			isFaceRight = false;
			MoveSpeed_ver = 0f;

			if((Time.time - sprintTimer) > 0.03f && (Time.time - sprintTimer) < SPRINT_CD && previousInputKey == KeyCode.LeftArrow)
			{
				sprintMode = true;
			}
			else if((Time.time - sprintTimer) >= 0.03f)
			{
				sprintMode = false;
			}

			if(sprintMode)	MoveSpeed_hor = -(this.speed * 2);
			else MoveSpeed_hor = -(this.speed);


			previousInputKey = KeyCode.LeftArrow;
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			isFaceRight = true;
			MoveSpeed_ver = 0f;

			if((Time.time - sprintTimer) > 0.03f && (Time.time - sprintTimer) < SPRINT_CD && previousInputKey == KeyCode.RightArrow)
			{
				sprintMode = true;
			}
			else if((Time.time - sprintTimer) >= 0.03f)
			{
				sprintMode = false;
			}

			if(sprintMode)	MoveSpeed_hor = (this.speed * 2);
			else MoveSpeed_hor = (this.speed);

			previousInputKey = KeyCode.RightArrow;
		}

		previousUpInput = Input.GetKey(KeyCode.UpArrow);
		sprintTimer = Time.time;

		transform.Translate(MoveSpeed_hor,MoveSpeed_ver,0);
	}
	private bool isJumping;
	private int jumpingFrame;
	private const float JUMPING_FORCE = 300f;
	private const float JUMPING_ADDING_FORCE = 0f;
	private float jumpingCalcForce;
	private void Jump()
	{
		if(isGrounded() && !isJumping)
		{
			// Debug.Log("jump");
			playerRigidBody.AddForce(gameObject.transform.up * JUMPING_FORCE);
			jumpingFrame = 0;
			jumpingCalcForce = JUMPING_FORCE;
			isJumping = true;
		}
		else
		{
			jumpingFrame++;
			if(jumpingFrame < 20) 
			{
				playerRigidBody.AddForce(gameObject.transform.up * JUMPING_ADDING_FORCE);
				jumpingCalcForce += JUMPING_ADDING_FORCE;
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
	private UnityEngine.KeyCode attackKey = KeyCode.Z;
	private Vector2 originHitBoxPosition;
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

		if(isFaceRight)
		{
			playerHitBox.offset = this.originHitBoxPosition;
		}
		else
		{
			playerHitBox.offset = new Vector2(-this.originHitBoxPosition.x, this.originHitBoxPosition.y);
		}

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
		fireTimer = Time.time;
		isFaceRight = true;
		sprintTimer = Time.time;
		sprintMode = false;
		originHitBoxPosition = playerHitBox.offset;
	}
	// Update is called once per frame
	void Update () {
		frame++;
		if(frame > 60)frame = 0;

		Move();
		Attack(attackHitBoxes[0]);
		Fire();
		Defend();
	}
}
