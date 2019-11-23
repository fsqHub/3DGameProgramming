using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthyBar_IMGUI : MonoBehaviour
{
    
	public float curHP;
	public float lerpHP;
	public float maxHP;

	public Rect HealthBar;
	public Rect HealthUp;
	public Rect HealthDown;

	public Slider HealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        curHP = 0.1f;
        lerpHP = 0.1f;
        maxHP = 1.0f;

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

        curHP = Mathf.Lerp(curHP,lerpHP,0.05f);
        GUI.HorizontalScrollbar(HealthBar,0.0f,curHP,0.0f,maxHP);
    
        HealthSlider.value = curHP;
    }
}
