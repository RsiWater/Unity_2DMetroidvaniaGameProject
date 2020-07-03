using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour {
    
    public GameObject player;
    [Header("攻擊速度")]
    [Range(0.1f,10f)]
    public float attackingTimeInterval;

    protected Rigidbody2D enemyRigidbody;
    protected float theDistanceBetweenPlayerX = 0;
    protected float theDistanceBetweenPlayerY = 0;
    protected float theAttackRangeRadius = 10f;
    public float movingSpeed = 0.5f;
    protected float startTime;
    protected bool ifPlayerIsAtRightSide()
    {
        if(theDistanceBetweenPlayerX < 0) return true;
        else return false;
    }
    protected bool ifPlayerIsInTheAttackRange()
    {
        if(Vector2.Distance(gameObject.transform.position, player.transform.position) < theAttackRangeRadius) return true;
        else return false;
    }
    protected void getDistanceBetweenPlayer()
    {
        theDistanceBetweenPlayerX = gameObject.transform.position.x - player.transform.position.x;
        theDistanceBetweenPlayerY = gameObject.transform.position.y - player.transform.position.y;
    }
    protected Vector2 CounterRotateVector2(Vector2 v, float d)
    {
        float sin = Mathf.Sin(d * Mathf.Deg2Rad);
        float cos = Mathf.Cos(d * Mathf.Deg2Rad);

        float temp_x = v.x;
        float temp_y = v.y;
        v.x = (cos * temp_x) - (sin * temp_y);
        v.y = (sin * temp_x) + (cos * temp_y);
        return v;
    }
    protected Vector2 getVectorDirectToPlayer()
    {
        Vector2 v = new Vector2(player.transform.position.x - gameObject.transform.position.x, player.transform.position.y - gameObject.transform.position.y);
        v.Normalize();
        return v;
    }
    protected abstract void Attack();
    protected abstract void Move();
    void Start () 
    {
        startTime = Time.time;
    }	
    void Update()
    {
      
    }
}
