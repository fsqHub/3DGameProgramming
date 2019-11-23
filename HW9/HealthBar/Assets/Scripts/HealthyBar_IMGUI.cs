using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthyBar_IMGUI : MonoBehaviour
{
    
	public float curHP;//当前血量
	public float lerpHP;//变化后的目标血量
	public float maxHP;//最大血量

	public Rect HealthBar;//血条框
	public Rect HealthUp;//加血按钮
	public Rect HealthDown;//减血按钮

	public Slider HealthSlider;
    // Start is called before the first frame update
    void Start()
    {   //相关变量初始化

        //血量相关值初始化
        curHP = 0.1f;
        lerpHP = 0.1f;
        maxHP = 1.0f;

        //条框，按钮位置初始化
        HealthBar = new Rect(25,25,300,50);
        HealthUp = new Rect(45,50,50,30);
        HealthDown = new Rect(250,50,50,30);
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (GUI.Button(HealthUp, "加血"))
        {
            lerpHP = lerpHP + 0.1f > maxHP ? maxHP : lerpHP + 0.1f;
        }
        if (GUI.Button(HealthDown, "减血"))
        {
            lerpHP = lerpHP - 0.1f < 0.0f ? 0.0f : lerpHP -0.1f;
        }

        //插值计算health值，实现血条平滑变化
        curHP = Mathf.Lerp(curHP,lerpHP,0.05f);

        //用水平滚动条的宽度作为血条的显示值
        GUI.HorizontalScrollbar(HealthBar,0.0f,curHP,0.0f,maxHP);
    
        HealthSlider.value = curHP;
    }
}
