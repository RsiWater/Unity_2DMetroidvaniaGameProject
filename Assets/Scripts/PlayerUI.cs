using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour{

    public Image attackIcon;
    public Image attackIconBG;
	public PlayerBehavior player;
    

    void Start()
    {
        this.attackOnCD = false;
        //
        UnityEngine.Color tempC = this.attackIcon.color;
        for(int i = 0;i < (4 - 1); i++)
        {
            tempC[i] *= 0.5f;
        }
        attackIconBG.color = tempC;
        //
    }
    private bool attackOnCD;
    private float startAttackTime;
    void Update()
    {
        this.attackCD();
    }
    private void attackCD()
    {
        if(Input.GetKey(player.getAttackKey()) && !this.attackOnCD) 
        {
            this.attackOnCD = true;
            this.startAttackTime = Time.time;
        }
        if(this.attackOnCD && player.getAttackCombo() != 0)
        {
            Debug.Log(player.getAttackCombo());
            
            float ratio = (Time.time - this.startAttackTime) / (player.getAttackCD()[player.getAttackCombo() - 1]);
            attackIconReload(ratio);
            if(ratio >= 1) this.attackOnCD = false;
        }
    }
    private void attackIconReload(float ratio)
    {
        attackIcon.rectTransform.localScale = new Vector2(1f, ratio);
    }
}