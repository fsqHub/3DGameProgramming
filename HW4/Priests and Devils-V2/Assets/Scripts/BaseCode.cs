using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

namespace PriestsAndDevils {

	public class Director : System.Object {
		private static Director _instance;
		public SceneController currentSceneController { get; set; }
		public static Director getInstance() {
			if (_instance == null) {
				_instance = new Director ();
			}
			return _instance;
		}
	}

	public interface SceneController {		//加载场景
		void loadResources ();
	}

	public interface UserAction {     //动作管理
		void moveBoat();
		void characterIsClicked(MyCharacterController characterCtrl);
		void restart();
	}
	//新增如下语句
	public enum SSActionEventType : int { Started, Complete }
	
	public interface ISSActionCallback	{   
		void SSActionEvent(SSAction source,
		SSActionEventType events = SSActionEventType.Complete,
        int intParam = 0, 
        string strParam = null, 
        Object objectParam = null);
	}
}