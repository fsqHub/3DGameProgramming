using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

namespace PriestsAndDevils {
	public class Disk : MonoBehaviour{
	    public float size;  //大小
	    public Color color; //颜色
	    public float speed; //速度
	    public Vector3 position;  //初始位置
	    public Vector3 direction;  //运动方向
	}
	
	public class Director : System.Object {
		private static Director _instance;
		public ISceneController currentSceneController { get; set; }
		public static Director getInstance() {
			if (_instance == null) {
				_instance = new Director();
			}
			return _instance;
		}
	}
	public enum ActionMode { PHYSICS, KINEMATIC}

	public interface ISceneController {		//加载场景
		void Init ();
		SceneController  getSceneController();
		ActionMode getMode();
		void setMode(ActionMode mode);

	}
	public interface IActionManager {
		GameObject GetDisk(int round,ActionMode mode);
	}
	
}