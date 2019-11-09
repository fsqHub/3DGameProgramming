using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    
	public GameObject[] newHalos;  //另外的两个光环
	//public PHalo[2] newHaloScript;
	public int index;  // 1 for small halo,2 for large halo

    // Start is called before the first frame update
    void Start()
    {
        //newHalos = new GameObject[2];
        index = 0;
        Reset();//重置加载两个光环

    }

    // Update is called once per frame
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 80, 80, 60), "Reset"))
        {
           Reset();//重置
        }
        if (GUI.Button(new Rect(0, 160, 80, 60), "Split"))  //光环分离
        {
            Split();//光环分离或重合
        }
    }

    void Reset(){  //重置加载两个光环（即与初始光环重合）
    	//newHalos[0].Clear(newHalos,0,1);

    	newHalos = new GameObject[2];
    	//加载光环预置
    	newHalos[0] = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Halo"), Vector3.zero, Quaternion.identity);
        newHalos[1] = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Halo"), Vector3.zero, Quaternion.identity);
    }
    void Split(){//与初始光环分离或重合
    	index = (index + 1) % 3;//取值0，1，2
    	if (index == 0){//如果index为0，则光环全部回到初始位置
    		newHalos[0].GetComponent<PHalo>().SetFlag(index);
    		newHalos[1].GetComponent<PHalo>().SetFlag(index);
    	}else{//否则两个光环之中的一个分离
    		newHalos[index - 1].GetComponent<PHalo>().SetFlag(index);
    	} 
    	Debug.Log("index:"+index);
    }
}
