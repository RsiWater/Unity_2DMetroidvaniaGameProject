using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxBehavior : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerBullet")
        {
            gameObject.SendMessage("TakeDamage",10f);
            Destroy(other.gameObject);
        }
    }
}