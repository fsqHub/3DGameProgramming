using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;


public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null) {  
                    Debug.LogError ("An instance of " + typeof(T) +
                    " is needed in the scene, but there is none.");  
                }  
            }
            return instance;
        }
    }
}

// 第一个场景控制器
public class FirstSceneController : MonoBehaviour, ISceneController
{
    // 已经发射的飞碟数量，第一关10个飞碟，第二关20个飞碟，第三关30个飞碟
    public int DiskNum;
    // 当前时间
    public float time;
    // 当前回合数
    public int round;
    // 飞碟队列
    public Queue<GameObject> diskQueue = new Queue<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        DiskNum = 0;
        // 设置第一个场景控制器为当前场景控制器
        Director.getInstance().currentSceneController = this;
        this.gameObject.AddComponent<DiskFactory>();
        this.gameObject.AddComponent<UserGUI>();
        Director.getInstance().currentSceneController.loadResources();

    }

    // 初始化每个回合的飞碟队列
    void InitDiskQueue(int round)
    {
        for(int i = 0;i < round*10;i ++)
        {
            diskQueue.Enqueue(Singleton<DiskFactory>.Instance.GetDisk(round));
        }
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        // 发射飞碟的间隔回合数成反比
        if(time >= 2.0f-0.3*round)
        {
            if (DiskNum == 0)
            {
                round = 1;
                this.gameObject.GetComponent<UserGUI>().round = 1;
                InitDiskQueue(round);
            }
            else if (DiskNum == 10)
            {
                round = 2;
                this.gameObject.GetComponent<UserGUI>().round = 2;
                InitDiskQueue(round);
            
            }else if (DiskNum == 20)
            {
                round = 3;
                this.gameObject.GetComponent<UserGUI>().round = 3;
                InitDiskQueue(round);
            }
            else if(DiskNum >= 30)
            {
                Reset();
            }
            if(DiskNum < 30)
            {
                time = 0;
                ThrowDisk();
                DiskNum++;
                this.gameObject.GetComponent<UserGUI>().total++;
                Debug.Log(DiskNum);
            }


        }
    }

    public void ThrowDisk()
    {
        if(diskQueue.Count > 0)
        {
            GameObject disk = diskQueue.Dequeue();
            disk.GetComponent<Renderer>().material.color = disk.GetComponent<DiskData>().color;
            disk.transform.position = disk.GetComponent<DiskData>().initPosition;
            disk.transform.localScale = disk.GetComponent<DiskData>().size * disk.transform.localScale;
            disk.SetActive(true);
            disk.AddComponent<ActionManager>();
            disk.GetComponent<ActionManager>().Move(disk.GetComponent<DiskData>().direction, disk.GetComponent<DiskData>().speed);
        }
    }

    public void loadResources()
    {
        this.gameObject.GetComponent<UserGUI>().round = 0;
        this.gameObject.GetComponent<UserGUI>().total = 0;
        ScoreController.getInstance().score = 0;
        DiskNum = 0;
        time = 0;
        round = 0;
        diskQueue.Clear();
    }

    void Reset()
    {
        this.gameObject.GetComponent<UserGUI>().reset = 1;
    }

}