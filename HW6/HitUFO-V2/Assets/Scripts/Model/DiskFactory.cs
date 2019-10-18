using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;
// 工厂模式
public class DiskFactory : MonoBehaviour, IActionManager
{
    private List<Disk> toDelete = new List<Disk>();
    private List<Disk> toUse = new List<Disk>();
    public Color[] colors = {Color.white,Color.yellow,Color.red,Color.blue,Color.green,Color.black};//可选颜色

   
    public GameObject GetDisk(int round,ActionMode mode){//根据回合数对飞碟设置属性并返回
        GameObject newDisk = null;
        if (toUse.Count > 0){
            newDisk = toUse[0].gameObject;
            toUse.Remove(toUse[0]);
        }else{
            newDisk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"), Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<Disk>();
        }
        //commonProperities();
        if (mode == ActionMode.PHYSICS){
            newDisk.AddComponent<Rigidbody>();
            newDisk.GetComponent<Rigidbody>().AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
        }

          // 飞碟的速度为round*7
        newDisk.GetComponent<Disk>().speed = 7.0f * round;
        // 飞碟随round越来越小
        newDisk.GetComponent<Disk>().size = (1 - round*0.1f);
        // 飞碟颜色随机
        int color = UnityEngine.Random.Range(0, 6);//共有六种颜色
        newDisk.GetComponent<Disk>().color = colors[color];
       
        // 飞碟的发射方向
        float RanX = UnityEngine.Random.Range(-1, 3) < 1 ? -1 : 1;//-1，0则为负方向，1，2则为正方向
        newDisk.GetComponent<Disk>().direction = new Vector3(-RanX, UnityEngine.Random.Range(-2f, 2f), 0);
        // 飞碟的初始位置
        newDisk.GetComponent<Disk>().position = new Vector3(RanX*13, UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-1f, 1f));

        toDelete.Add(newDisk.GetComponent<Disk>());
        newDisk.SetActive(false);
        newDisk.name = newDisk.GetInstanceID().ToString();
        return newDisk;
    }

    public void FreeDisk(GameObject disk){
        Disk cycledDisk = null;
        foreach (Disk toCycle in toDelete){
            if (disk.GetInstanceID() == toCycle.gameObject.GetInstanceID()){
                cycledDisk = toCycle;
            }
        }
        if (cycledDisk != null){
            cycledDisk.gameObject.SetActive(false);
            toUse.Add(cycledDisk);
            toDelete.Remove(cycledDisk);
        }
    }

}