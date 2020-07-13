using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticValueControl : MonoBehaviour
{
    public Image currentHealthBar;
    public Text ratioText;

    public float maxinumHP = 100f;
    private float currentHP;

    private void Start()
    {
        currentHP = maxinumHP;
        lastTime = Time.time;
        UpdateHealthBar();
    }
    private void Update()
    {

    }
    private void UpdateHealthBar()
    {
        float ratio = currentHP / maxinumHP;
        currentHealthBar.rectTransform.localScale = new Vector2(ratio,1);
        ratioText.text = currentHP + " / " + maxinumHP;
    }

    private const float playerInvincibleTime = 1f;
    private void TakeDamage(float damage)
    {
        if(gameObject.name == "Player")
        {
            if(InvincibleCD(playerInvincibleTime)) currentHP -= damage;
        }
        else
        {
            currentHP -= damage;
        }
        if(currentHP <= 0)
        {
            Debug.Log(gameObject.name+" is dead");
            ObjectDead();
        }
        else UpdateHealthBar();
    }

    private float lastTime;
    private bool InvincibleCD(float CDtime)
    {
        float timeBetweenLastDamaged = Time.time - lastTime;
        lastTime = Time.time;
        if(timeBetweenLastDamaged < CDtime) return false;
        else return true;
    }
    private void ObjectDead()
    {
        Destroy(gameObject);
    }
}