using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehavior : EnemyBehavior
{
    public GameObject Bullet;
    // [Header("攻擊速度")]
    // [Range(0.1f,5f)]
    // public float AttackingTimeInterval = 2.5f;
    [Header("移動間隔")]
    [Range(0.5f,6f)]
    public float MovingTimer = 3f;
    [Header("子彈力道")]
    [Range(50f,600f)]
    public float BulletForce = 100f;
    private float slimeJumpingTimer;
    private float slimeStartAttackTime;
    private float slimeAttackTimer;
    private const float slimeJumpingForce = 200f;
    private bool MovingMode; //if MovingMode is true, then slime is Attacking.
    
    private void slimeAI()
    {
        getDistanceBetweenPlayer();
        Attack();
        slimeJumpingTimer = Time.time - startTime;
        if(slimeJumpingTimer < MovingTimer) return;
        startTime = Time.time;
        if(MovingMode) slimeAttackMode();
        else Move();
    }
    protected override void Move()
    {
        if(ifPlayerIsAtRightSide())
        {
            enemyRigidbody.AddForce(gameObject.transform.up * slimeJumpingForce);
            enemyRigidbody.AddForce(gameObject.transform.right * ( slimeJumpingForce / 2 ));
        }
        else
        {
            enemyRigidbody.AddForce(gameObject.transform.up * slimeJumpingForce);
            enemyRigidbody.AddForce(gameObject.transform.right * -( slimeJumpingForce / 2 ));
        }
    }
    private void slimeAttackMode()
    {
        
    }
    protected override void Attack()
    {
        if(!ifPlayerIsInTheAttackRange())
        {
            MovingMode = false;
            return;
        }
        else MovingMode = true;

        slimeAttackTimer = Time.time - slimeStartAttackTime;
        if(slimeAttackTimer < attackingTimeInterval) return;
        slimeStartAttackTime = Time.time;

        Shoot();
    }
    private void Shoot()
    {   
        for(int i = -2; i < 3;i++)
        {
            GameObject tempBullet = Instantiate(Bullet,gameObject.transform.position,Quaternion.identity) as GameObject;
            Rigidbody2D tempBulletRigidBody = tempBullet.GetComponent<Rigidbody2D>();

            Vector2 V = getVectorDirectToPlayer();
            V = CounterRotateVector2(V, 15 * i);
            Vector3 directionToPlayer = new Vector3(V.x,V.y,0f);
            
            tempBulletRigidBody.AddForce(directionToPlayer * BulletForce);
            Destroy(tempBullet,2);
        }
    }

    void Start()
    {
        attackingTimeInterval = 2.5f;
        theAttackRangeRadius = 5f;
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();
        slimeStartAttackTime = Time.time;
    }
    void Update()
    {
        slimeAI();
    }

}
