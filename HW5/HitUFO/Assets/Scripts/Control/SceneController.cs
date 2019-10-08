using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

public class SceneController
{
    public int round ;  //回合数
    public int total ;  //总飞碟数
    public int score ;//得到的分数

    private static SceneController sceneCtrl;

    public SceneController() {//用于SceneController归0
        score = 0;
        total = 0;
        score = 0;
    }
    public static SceneController getInstance(){
        if (sceneCtrl == null){
            sceneCtrl = new SceneController();
        }
        return sceneCtrl;
    }

    public void addRound(){
        round++;
    }
    public void addTotal(){
        total++;
    }
    public void addScore(){
        score++;
    }
    
    public int getRound(){
        return round;
    }
    public int getTotal() {
        return total;
    }
    public int getScore() {
        return score;
    }


}