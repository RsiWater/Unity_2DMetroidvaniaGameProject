using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaticValue : MonoBehaviour
{
    public float healthPoint;
    public float magicPoint;
    void start()
    {
        healthPoint = 100f;
        magicPoint = 100f;
    }

    void update()
    {

    }
}