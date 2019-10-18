using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

public class UserGUI : MonoBehaviour
{
    /*public int round;//直接在此定义使用时旧的数字不会消失
    public int total;
    public int score;*/
    public int reset;
    GUIStyle style;
    GUIStyle buttonStyle;
   // public ISceneController userAction;直接用可以运行，但是会报错
    // Start is called before the first frame update
    ActionMode mode;
    void Start()
    {
        reset = 0;//不能一开始就设定为1，否则需要按两下按钮才会消失
        style = new GUIStyle();
        style.fontSize = 30;
        //style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.green;// 

        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 15;
        buttonStyle.normal.textColor = Color.green;// 
        //userAction = Director.getInstance().currentSceneController;//此处挂载失败

        mode = ActionMode.PHYSICS;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnGUI()
    {
       
        if(reset == 1){
            /*if(GUI.Button(new Rect(380, 250, 100, 80), "Reset",buttonStyle)){
                //userAction.Init();
                Director.getInstance().currentSceneController.Init();
                reset = 0;
            }*/
            
           if(GUI.Button(new Rect(200, 250, 100, 80), "KINEMATIC",buttonStyle)){
                mode = ActionMode.KINEMATIC;
                Director.getInstance().currentSceneController.setMode(mode);
                Director.getInstance().currentSceneController.Init();
                reset = 0;
                return;
            }
            if(GUI.Button(new Rect(400, 250, 100, 80), "PHYSICS",buttonStyle)){
                mode = ActionMode.PHYSICS;
                Director.getInstance().currentSceneController.setMode(mode);
                Director.getInstance().currentSceneController.Init();
                reset = 0;
                return;
            } 
           // else mode = ActionMode.PHYSICS;
            
            
        }

        if (reset == 0){
           
            int round = Director.getInstance().currentSceneController.getSceneController().getRound();
            int total = Director.getInstance().currentSceneController.getSceneController().getTotal();
            int score = Director.getInstance().currentSceneController.getSceneController().getScore();
            int miss = total - score;//未击中的飞碟数
            //string text = "Round: " + userAction.getSceneController().GetRound().ToString() + "\nTotal:  " + total.ToString() + "\nScores:  " + score.ToString();
            string text = "Round: " + round.ToString() + "\nMiss:  " + miss.ToString() + "\nScores:  " + score.ToString();
            GUI.Label(new Rect(10, 10, Screen.width, 50),text,style); 

        }     
    }

}