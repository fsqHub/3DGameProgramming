using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

public class UserGUI : MonoBehaviour
{
    public int round;
    public int total;
    public int reset;
    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        total = 0;
        reset = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle
        {
            border = new RectOffset(10, 10, 10, 10),
            fontSize = 30,
            fontStyle = FontStyle.BoldAndItalic,
        };
        // normal:Rendering settings for when the component is displayed normally.
        style.normal.textColor = new Color(200 / 255f, 180 / 255f, 150 / 255f);    // 需要除以255，因为范围是0-1
        if(reset == 1)
        {
            if(GUI.Button(new Rect(250, 300, 100, 50), "Reset"))
            {
                Director.getInstance().currentSceneController.loadResources();
                reset = 0;
            }
        }


        style.normal.textColor = new Color(255 / 255f, 0 / 255f, 0 / 255f);
        GUI.Label(new Rect(140, 150, 200, 80), "Round : " + round.ToString(), style);
        GUI.Label(new Rect(140, 200, 200, 80), "Total : " + total.ToString(), style);

        GUI.Label(new Rect(140, 250, 200, 80), "Score : "+ScoreController.getInstance().GetScore().ToString(), style);
        
    }

}