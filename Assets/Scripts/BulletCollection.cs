using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletCollection
{
    // public static void ScatteredBullet(GameObject Bullet)
    // {
    //     for(int i = -2; i < 3;i++)
    //     {
    //         GameObject tempBullet = Instantiate(Bullet,gameObject.transform.position,Quaternion.identity) as GameObject;
    //         Rigidbody2D tempBulletRigidBody = tempBullet.GetComponent<Rigidbody2D>();

    //         Vector2 V = getVectorDirectToPlayer();
    //         V = CounterRotateVector2(V, 15 * i);
    //         Vector3 directionToPlayer = new Vector3(V.x,V.y,0f);
            
    //         tempBulletRigidBody.AddForce(directionToPlayer * BulletForce);
    //         Destroy(tempBullet,2);
    //     }
    // }
}