using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

public class SSAction : ScriptableObject
{

    public bool enable = true;
    public bool destroy = false;

    public GameObject GameObject { get; set; }
    public Transform Transform { get; set; }
    public ISSActionCallback Callback { get; set; }

    protected SSAction() { }

    // Use this for initialization
    public virtual void Start()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        throw new System.NotImplementedException();
    }
}

public class SSMoveToAction : SSAction{

    public Vector3 target; // 移动目标
    public float speed; // 移动速度

    // 创建并返回动作的实例
    public static SSMoveToAction GetSSMoveToAction(Vector3 target, float speed){
        SSMoveToAction action = ScriptableObject.CreateInstance<SSMoveToAction>();
        action.target = target;
        action.speed = speed;
        return action;
    }

    public override void Start(){

    }

    // 在 Update 函数中用 Vector3.MoveTowards 实现直线运动
    public override void Update(){
        this.Transform.position = Vector3.MoveTowards(this.Transform.position, target, speed * Time.deltaTime);
        if (this.Transform.position == target)
        {
            this.destroy = true;
            // 完成动作后进行动作回掉
            this.Callback.SSActionEvent(this);
        }
    }
}

public class SSSequenceAction : SSAction, ISSActionCallback{

    public List<SSAction> sequence; 
    public int repeat = -1;
    public int start = 0; 

    // 创建并返回动作序列的实例
    public static SSSequenceAction GetSSSequenceAction(int repeat, int start, List<SSAction> sequence){
        SSSequenceAction action = ScriptableObject.CreateInstance<SSSequenceAction>();
        action.repeat = repeat;
        action.sequence = sequence;
        action.start = start;
        return action;
    }
	
	// 在 Update 中执行当前动作
    public override void Update(){
        if (sequence.Count == 0) return;
        if (start < sequence.Count)
        {
            sequence[start].Update();
        }
    }

    // 更新当前执行的动作
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Complete, 
    							int intParam = 0, string strParam = null, Object objectParam = null) {
        source.destroy = false;
        this.start++;
        if (this.start >= sequence.Count)
        {
            this.start = 0;
            if (repeat > 0) repeat--;
            if (repeat == 0)
            {
                this.destroy = true;
                this.Callback.SSActionEvent(this);
            }
        }
    }

    // Use this for initialization
    public override void Start(){
        foreach (SSAction action in sequence)
        {
            action.GameObject = this.GameObject;
            action.Transform = this.Transform;
            action.Callback = this;
            action.Start();
        }
    }

  

    // 执行完毕后销毁动作
    void OnDestroy()
    {
        foreach (SSAction action in sequence)
        {
            DestroyObject(action);
        }
    }

    
}

public class SSActionManager : MonoBehaviour{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction>();
    private List<int> waitingDelete = new List<int>();

    protected void Update(){
        foreach (SSAction action in waitingAdd){
            actions[action.GetInstanceID()] = action;
        }
        waitingAdd.Clear();

        foreach (KeyValuePair<int, SSAction> KeyValue in actions){
            SSAction action = KeyValue.Value;
            if (action.destroy){
                waitingDelete.Add(action.GetInstanceID()); // release action
            }
            else if (action.enable){
                action.Update(); // update action
            }
        }

        foreach (int key in waitingDelete){
            SSAction action = actions[key];
            actions.Remove(key);
            DestroyObject(action);
        }
        waitingDelete.Clear();
    }
    protected void Start(){ 

    }
    // 执行动作
    public void RunAction(GameObject gameObject, SSAction action, ISSActionCallback callback){
        action.GameObject = gameObject;
        action.Transform = gameObject.transform;
        action.Callback = callback;
        waitingAdd.Add(action);
        action.Start();
    }
    public void SSActionEvent(SSAction source, SSActionEventType events = SSActionEventType.Complete,
        int intParam = 0, string strParam = null, Object objectParam = null){
        
    }
}
public class ActionManager : SSActionManager, ISSActionCallback  
{

    private SSMoveToAction boat_move;     
    private SSSequenceAction char_move;     

    public FirstController sceneController;

    protected new void Start()
    {
        sceneController = (FirstController)Director.getInstance().currentSceneController;
        sceneController.actionManager = this;
    }
    public void moveBoat(BoatController boatCtrl, Vector3 dest, float speed)
    {
        boat_move = SSMoveToAction.GetSSMoveToAction(dest, speed);
        this.RunAction(boatCtrl.getGameobj(), boat_move, this);
    }

    public void moveChar(MyCharacterController charCtrl,Vector3 dest, float speed)
    {
        //moveable.setDestion()的功能
        Vector3 middle = dest;
        Vector3 chara = charCtrl.getGameobj().transform.position;
        if (dest.y < chara.y) {	// character from bank to boat，说明角色在岸上，可以上船
			middle.y = chara.y;
		}else {								// character from boat to bank   说明角色在船上，可以上岸
			middle.x = chara.x;
		}

        SSAction action1 = SSMoveToAction.GetSSMoveToAction(middle, speed);
        SSAction action2 = SSMoveToAction.GetSSMoveToAction(dest, speed);
        char_move = SSSequenceAction.GetSSSequenceAction (1, 0, new List<SSAction> { action1, action2 });
        this.RunAction(charCtrl.getGameobj(), char_move, this);
    }
}

