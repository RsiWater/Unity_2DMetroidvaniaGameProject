using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderBehavior : MonoBehaviour
{
    private const string ENEMY_TAG = "Ememy";
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision.");
        if(other.gameObject.tag == ENEMY_TAG)
        {
        }
        Debug.Log(other.gameObject.tag);
    }
    void Start()
    {
        Debug.Log("start.");
    }

    void Update()
    {

    }
}