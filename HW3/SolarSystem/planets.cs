using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planets : MonoBehaviour
//所有行星与太阳共用一个文件（但太阳不是行星）
{
	public float revolSpeed,rotaSpeed;
	public GameObject center;//旋转中心
	public int offset_x;//法平面的偏移角x
	public int offset_z;//法平面的偏移角z
	public int offset_y;//法平面的偏移角y
	// Start is called before the first frame update
	void Start()
	{
		offset_x = Random.Range(0, 10);
		offset_z = Random.Range(0, 10);
		offset_y = Random.Range(0, 10);
		revolSpeed = Random.Range (10, 90);
		rotaSpeed = Random.Range (10, 20);
		center = GameObject.Find("Sun");
	}

	// Update is called once per frame
	void Update()
	{
		if (this.name == "Sun") {//太阳只有自转
			this.transform.Rotate (new Vector3 (-offset_y, offset_x, 0) * rotaSpeed * Time.deltaTime);
		} else {
			//公转
			//垂直于角速度(x,y,z)的平面（可以）是（z,0,x）
			Vector3 axis = new Vector3 (-offset_z, 0, offset_x);
			this.transform.RotateAround (center.transform.position, axis, revolSpeed * Time.deltaTime);
			//行星自转，轴随意定的
			this.transform.Rotate (Vector3.up  * rotaSpeed * Time.deltaTime);
		}
	}
}