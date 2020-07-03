using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBoxBehavior : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet")
        {
            gameObject.SendMessage("TakeDamage",10f);
            Destroy(other.gameObject);
        }
    }
}