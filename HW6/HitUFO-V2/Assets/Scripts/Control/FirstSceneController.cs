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


public class FirstSceneController : MonoBehaviour, ISceneController{
    public int diskFlyTimes; //已经发射的飞碟个数，每回合10个，最多30个
    public float time;// 时间，用于控制飞碟发射间隔
    public int round;  // 当前回合数
    public ActionMode mode;//模式选择

    // 飞碟队列
    public Queue<GameObject> diskQueue = new Queue<GameObject>();//飞碟队列
    public SceneController  sceneCtrl;
    

    public ActionMode getMode() 
    {
        return mode; 
    }

    public void setMode(ActionMode m)
    {
        //if (m == ActionMode.KINEMATIC) this.gameObject.AddComponent<CCActionManager>();
    //    else this.gameObject.AddComponent<PhysicActionManager>();
        mode = m;
    }


    // Start is called before the first frame update
    void Start(){
        // 当前场景控制器
        Director.getInstance().currentSceneController = this;
        this.gameObject.AddComponent<DiskFactory>();
        this.gameObject.AddComponent<UserGUI>();
        Director.getInstance().currentSceneController.Init();//初始化FirstSceneController相关数据

        mode = ActionMode.PHYSICS;
    }

    // 初始化每个回合的飞碟队列,每个回合的飞碟属性不同
    void initQueue(){
        diskQueue.Clear();//clear first

        for(int i = 0;i < 10;i ++)
            diskQueue.Enqueue(Singleton<DiskFactory>.Instance.GetDisk(round,getMode()));
    }

    // Update is called once per frame
    void Update() {
       // round = sceneCtrl.getRound();
        time += Time.deltaTime;
        // 发射飞碟的间隔回合数成反比
        if(time >= 2.0f-0.3*round){

            if(diskFlyTimes >= 30){//游戏结束
                Reset();
            }else if ((diskFlyTimes % 10) == 0 ){//更新回合（此步骤必须在发射飞碟前面）
                round ++;//在initQueue();前面才行
                sceneCtrl.addRound();//回合数增加
                initQueue();//初始化新的飞盘队列
            }
            if (diskFlyTimes < 30){
                time = 0;
                ThrowDisk();//发射飞盘
                diskFlyTimes ++;//飞盘数增加
                sceneCtrl.addTotal();//综费盘数增加
            }
        }
    }

    public void ThrowDisk()
    {
        if(diskQueue.Count > 0)
        {
            GameObject disk = diskQueue.Dequeue();
            disk.GetComponent<Renderer>().material.color = disk.GetComponent<Disk>().color;
            disk.transform.position = disk.GetComponent<Disk>().position;
            disk.transform.localScale = disk.GetComponent<Disk>().size * disk.transform.localScale;
            disk.SetActive(true);
            disk.AddComponent<ActionManager>();
            disk.GetComponent<ActionManager>().diskFly(disk.GetComponent<Disk>().direction, disk.GetComponent<Disk>().speed);
        }
    }
    /*public int getRound(){
        return sceneCtrl.getRound();
    }
    public int getTotal(){
        return sceneCtrl.getTotal();
    }*/

    public void Init()
    {
        sceneCtrl = new SceneController();//SceneController元素归0
        diskFlyTimes = 0;
        time = 0;
        round = 0;
        diskQueue.Clear();//清空飞盘队列
    }
    public SceneController  getSceneController(){
        return sceneCtrl;
    }
    void Reset()
    {
        this.gameObject.GetComponent<UserGUI>().reset = 1;
    }

}