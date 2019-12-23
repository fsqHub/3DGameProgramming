using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Vuforia;

public class ButtonEvent : MonoBehaviour, IVirtualButtonEventHandler
{
    public VirtualButtonBehaviour[] vbs;
    public GameObject cube;
    public GameObject button;
    public float move;
    public float moveback;
    public Vector3 Xdistance;
    public Vector3 Zdistance;
    //public float speed;

    void Start()
    {
        vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }
        move = 0.0f;
        moveback = 0.0f;
        //speed = 0.02f;
        Xdistance = new Vector3(0.02f,0.0f,0.0f);//X方向移动的距离
        Zdistance = new Vector3(0.0f,0.0f,0.02f);//Y当向上移动的距离
    }



    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        //方块按规律朝4个方向移动
        move = move % 4.0f + 1.0f;
        Debug.Log("move");
        if(move == 1.0f){
        	cube.transform.Translate(move * Xdistance);
        }
        else  if(move == 2.0f){
        	cube.transform.Translate(move * Xdistance * (-1.0f));
        }
        else  if(move == 3.0f){
        	cube.transform.Translate(move * Zdistance);
        }else {
        	cube.transform.Translate(move * Zdistance * (-1.0f));
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        //方块恢复初始位置
        moveback = -1.0f * move; 
        Debug.Log("move back");
        if(move == 1.0f){
        	cube.transform.Translate(moveback * Xdistance);
        }
        else  if(move == 2.0f){
        	cube.transform.Translate(moveback * Xdistance * (-1.0f));
        }
        else  if(move == 3.0f){
        	cube.transform.Translate(moveback * Zdistance);
        }else {
        	cube.transform.Translate(moveback * Zdistance * (-1.0f));
        }
    }

    // void update(){
    	// Debug.Log("move");
    	// if(move == 1.0f){
    	// 	 float Xposition = cube.transform.position.y;
    	// 	if (Xposition <= 0.5f && Xposition >= 0.0f) {
	    //     	cube.transform.position += distance * Time.deltaTime;
	    //     }else {
	    //     	direction = (-1.0f )* direction;
	    //     	cube.transform.position += distance * Time.deltaTime;
	    //     }
    	// }
    //}
    
}


