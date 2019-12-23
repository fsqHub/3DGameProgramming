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
    public int index;
    public Color[] colors;

    void initColors()
    {
        colors = new Color[6];
        colors[0] = Color.blue;
        colors[1] = Color.red;
        colors[2] = Color.yellow;
       // colors[3] = Color.purple;
        colors[3] = Color.green;
        colors[4] = Color.white;
        colors[5] = Color.black;
    }

    void Start()
    {
        vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }
        initColors();
        index = 0;
    }


    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        index = (index ++) % 6;
        button.GetComponent<MeshRenderer>().material.color = Color.red;
        cube.GetComponent<MeshRenderer>().material.color = colors[index];

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        button.GetComponent<MeshRenderer>().material.color = Color.white;
    }

}


