using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxBehavior : MonoBehaviour
{
    private bool ifCollider;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("detected " + other.name);
        ifCollider = true;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("exit Collision");
        ifCollider = false;
    }

    private void LaunchAttack(Collider2D other)
    {
        // Collider2D[] others = Physics2D.OverlapBox(other.bounds.center, other.bounds.extents, other.transform.z, LayerMask.GetMask("Enemy"));

    }
}