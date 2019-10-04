using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

public class Judger : MonoBehaviour{
	

    void Start(){
        
    }

    public void judge(UserGUI userGUI,GUIStyle style,GUIStyle buttonStyle){
        if (userGUI.status == 1) {
            GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "You Lose!", style);
            if (GUI.Button(new Rect(Screen.width/2-70, Screen.height/2, 140, 70), "Restart", buttonStyle)) {
                userGUI.status = 0;
                userGUI.action.restart ();
            }
        } else if(userGUI.status == 2) {
            GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "You win!", style);
            if (GUI.Button(new Rect(Screen.width/2-70, Screen.height/2, 140, 70), "Restart", buttonStyle)) {
                userGUI.status = 0;
                userGUI.action.restart ();
            }
        }

    }
}

