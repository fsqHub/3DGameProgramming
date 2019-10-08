using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;
// 工厂模式
public class DiskFactory : MonoBehaviour
{
    public GameObject diskPrefab;
    private List<DiskData> used = new List<DiskData>();
    private List<DiskData> free = new List<DiskData>();

    public GameObject GetDisk(int round)
    {
        GameObject newDisk = null;
        if (free.Count > 0)
        {
            newDisk = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
        {
            newDisk = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"), Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<DiskData>();
        }

        // 飞碟的速度跟round成正比
        newDisk.GetComponent<DiskData>().speed = 2.0f * round;
        // 飞碟的大小跟round成反比（缩放倍数）
        newDisk.GetComponent<DiskData>().size = 1 - 0.1f * (round-1);
        // 飞碟的颜色是随机生成的
        float random = UnityEngine.Random.Range(0f, 3f);
        if (random < 1)
        {
            newDisk.GetComponent<DiskData>().color = Color.yellow;
        }
        else if(random < 2)
        {
            newDisk.GetComponent<DiskData>().color = Color.red;
        }
        else
        {
            newDisk.GetComponent<DiskData>().color = Color.blue;
        }
        // 飞碟的发射方向
        float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
        newDisk.GetComponent<DiskData>().direction = new Vector3(-RanX, UnityEngine.Random.Range(-1f, 1f), 0);
        // 飞碟的初始位置
        newDisk.GetComponent<DiskData>().initPosition = new Vector3(RanX*9, UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

        used.Add(newDisk.GetComponent<DiskData>());



        newDisk.SetActive(false);
        newDisk.name = newDisk.GetInstanceID().ToString();
        return newDisk;
    }

    public void FreeDisk(GameObject disk)
    {
        DiskData tmp = null;
        foreach (DiskData dd in used)
        {
            if (disk.GetInstanceID() == dd.gameObject.GetInstanceID())
            {
                tmp = dd;
            }
        }
        if (tmp != null)
        {
            tmp.gameObject.SetActive(false);
            free.Add(tmp);
            used.Remove(tmp);
        }
    }

}