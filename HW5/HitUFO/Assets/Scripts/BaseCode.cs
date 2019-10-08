using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

namespace PriestsAndDevils {
	public class DiskData : MonoBehaviour{
    // 色彩、大小、发射位置、速度、角度
	    public float size;
	    public Color color;
	    public float speed;
	    public Vector3 direction;
	    public Vector3 initPosition;
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

	public interface ISceneController {		//加载场景
		void loadResources ();
	}

	
}