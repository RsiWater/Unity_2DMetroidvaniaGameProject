using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour{

    public Image attackIcon;
    public Image attackIconBG;

    void Start()
    {
        UnityEngine.Color tempC = this.attackIcon.color;
        for(int i = 0;i < (4 - 1); i++)
        {
            tempC[i] *= 0.5f;
        }
        attackIconBG.color = tempC;
    }
    private int frame;
    void Update()
    {
        if(frame >= 60) frame = 0;

        IconCD((float)frame / 60f);

        frame++;
    }
    private void IconCD(float ratio)
    {
        attackIcon.rectTransform.localScale = new Vector2(1f, ratio);
    }
}