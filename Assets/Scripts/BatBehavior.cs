using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehavior : EnemyBehavior
{
    [Header("移動速度")]
    [Range(0.005f,20f)]
    public float MovingSpeed = 0.1f;
    private float batStartAttackTime;
    private float batAttackTimer;
    private void BatAI()
    {
        getDistanceBetweenPlayer();
        Attack();
        Move();
    }
    protected override void TakeDamage()
    {

    }
    protected override void stunned(float sec)
    {
        
    }
    protected override void Attack()
    {
        if(!ifPlayerIsInTheAttackRange()) return;

        batAttackTimer = Time.time - batStartAttackTime;
        if(batAttackTimer < attackingTimeInterval) return;
        batStartAttackTime = Time.time;

        Shoot();
    }
    private void Shoot()
    {
        
    }
    protected override void Move()
    {
        Vector2 V = getVectorDirectToPlayer();
        gameObject.transform.Translate(MovingSpeed * V.x, MovingSpeed * V.y, 0);
    }
    void Start()
    {
        attackingTimeInterval = 2.5f;
        theAttackRangeRadius = 10f;
        enemyRigidbody = gameObject.GetComponent<Rigidbody2D>();
        batStartAttackTime = Time.time;
    }
    void Update()
    {
        BatAI();
    }
}