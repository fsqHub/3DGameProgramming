using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

public class ActionManager : MonoBehaviour
{

    public Vector3 direction;
    public float speed;
    public GameObject cam;

    public void Move(Vector3 direction,float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += speed * direction * Time.deltaTime;


        if (Input.GetButtonDown("Fire1"))
        {
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

            foreach (RaycastHit hit in hits)
            {
                print(hit.transform.gameObject.name);
                if (hit.collider.gameObject.tag.Contains("Finish"))
                { //plane tag
                    Debug.Log("hit " + hit.collider.gameObject.name + "!");
                }
                Singleton<DiskFactory>.Instance.FreeDisk(hit.transform.gameObject);
                ScoreController.getInstance().AddScore();
            }
        }


    }
}
