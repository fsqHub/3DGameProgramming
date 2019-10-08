using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

public class ScoreController
{

    public int score = 0;

    private static ScoreController scorecontrol;

    private ScoreController() {
        score = 0;
    }
    public static ScoreController getInstance()
    {
        if (scorecontrol == null)
        {
            scorecontrol = new ScoreController();
        }
        return scorecontrol;
    }

    public void AddScore()
    {
        score++;
    }

    public int GetScore()
    {
        return score;
    }

}