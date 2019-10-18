using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

public class ActionManager : MonoBehaviour
{

    public Vector3 direction;//运动方向
    public float speed;//初速度
    public GameObject cam;

    public void diskFly(Vector3 direction,float speed){//赋予飞碟初速度和方向
        this.direction = direction;
        this.speed = speed;
    }

    // Start is called before the first frame update
    void Start(){
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update(){
        
        this.gameObject.transform.position += speed * direction * Time.deltaTime;

        if (Input.GetButtonDown("Fire1")){   //光标拾取物体的结果，即鼠标集中飞碟的结果
           
            Debug.Log("Fired Pressed");
            Debug.Log(Input.mousePosition);

            Vector3 mp = Input.mousePosition; //get Screen Position

            //create ray, origin is camera, and direction to mousepoint
            Camera ca;
            if (cam != null) ca = cam.GetComponent<Camera>();
            else ca = Camera.main;

            Ray ray = ca.ScreenPointToRay(Input.mousePosition);

            //Return the ray's hits
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits){
                print(hit.transform.gameObject.name);
                if (hit.collider.gameObject.tag.Contains("Finish")){ //plane tag
                    Debug.Log("hit " + hit.collider.gameObject.name + "!");
                }
                Singleton<DiskFactory>.Instance.FreeDisk(hit.transform.gameObject);//飞碟回收
                //this.gameObject.GetComponent<UserGUI>().score ++;
                //SceneController.getInstance().addScore();
                Director.getInstance ().currentSceneController.getSceneController().addScore();//分数加一
            }
        }

    }
}
