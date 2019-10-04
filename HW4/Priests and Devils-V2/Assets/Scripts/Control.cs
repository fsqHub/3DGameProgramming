using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriestsAndDevils;

/*-----------------------------------Moveable------------------------------------------*/
	public class Moveable: MonoBehaviour {

		readonly float move_speed = 20;

		// change frequently
		int moving_status;	// 0->not moving, 1->moving to middle, 2->moving to dest
		Vector3 dest;
		Vector3 middle;

		void Update() {
			if (moving_status == 1) {   //小船从一端移向另一端
				transform.position = Vector3.MoveTowards (transform.position, middle, move_speed * Time.deltaTime);
				if (transform.position == middle) {
					moving_status = 2;
				}
			} else if (moving_status == 2) {
				transform.position = Vector3.MoveTowards (transform.position, dest, move_speed * Time.deltaTime);
				if (transform.position == dest) {
					moving_status = 0;
				}
			}
		}
		public void setDestination(Vector3 _dest) {    //_dest为小船该去的位置
			dest = _dest;
			middle = _dest;//             
			if (_dest.y == transform.position.y) {	//       说明角色在船上，小船可以移动
				moving_status = 2;
			}
			else if (_dest.y < transform.position.y) {	// character from bank to boat，说明角色在岸上，可以上船
				middle.y = transform.position.y;
			} else {								// character from boat to bank   说明角色在船上，可以上岸
				middle.x = transform.position.x;
			}
			moving_status = 1;
		}

		public void reset() {
			moving_status = 0;
		}
	}


	/*-----------------------------------MyCharacterController------------------------------------------*/
	public class MyCharacterController {
		readonly GameObject character;
		readonly Moveable moveableScript;
		readonly ClickGUI clickGUI;
		readonly int characterType;	// 0->priest, 1->devil

		// change frequently
		bool _isOnBoat;
		bankController bankController;


		public MyCharacterController(string which_character) {

			if (which_character == "priest") {
				character = Object.Instantiate (Resources.Load ("Prefabs/priest", typeof(GameObject)), new Vector3(0,0,0), Quaternion.identity, null) as GameObject;
				characterType = 0;
			} else {
				character = Object.Instantiate (Resources.Load ("Prefabs/devil", typeof(GameObject)),  new Vector3(0,0,0), Quaternion.identity, null) as GameObject;
				characterType = 1;
			}
			moveableScript = character.AddComponent (typeof(Moveable)) as Moveable;

			clickGUI = character.AddComponent (typeof(ClickGUI)) as ClickGUI;
			clickGUI.setController (this);
		}

		public void setName(string name) {
			character.name = name;
		}

		public void setPosition(Vector3 pos) {
			character.transform.position = pos;
		}

		public void moveToPosition(Vector3 destination) {
			moveableScript.setDestination(destination);
		}

		public int getType() {	// 0->priest, 1->devil
			return characterType;
		}

		public string getName() {
			return character.name;
		}

		public void getOnBoat(BoatController boatCtrl) {
			bankController = null;
			character.transform.parent = boatCtrl.getGameobj().transform;
			_isOnBoat = true;
		}

		public void getOnbank(bankController bankCtrl) {
			bankController = bankCtrl;
			character.transform.parent = null;
			_isOnBoat = false;
		}

		public bool isOnBoat() {
			return _isOnBoat;
		}

		public bankController getbankController() {
			return bankController;
		}
		public GameObject getGameobj() {
			return character;
		}

		public void reset() {
			moveableScript.reset ();
			bankController = (Director.getInstance ().currentSceneController as FirstController).frombank;
			getOnbank (bankController);
			setPosition (bankController.getEmptyPosition ());
			bankController.getOnbank (this);
		}
	}

	/*-----------------------------------bankController------------------------------------------*/
	public class bankController {
		readonly GameObject bank;
		readonly Vector3 from_pos = new Vector3(9,1,0);
		readonly Vector3 to_pos = new Vector3(-9,1,0);
		readonly Vector3[] positions;//角色的初始位置
		readonly int to_or_from;	// to->-1, from->1

		// change frequently
		MyCharacterController[] passengerPlaner;

		public bankController(string _to_or_from) {
			positions = new Vector3[] {new Vector3(6.5F,4.0f,0), new Vector3(7.5F,4.0f,0), new Vector3(8.5F,4.0f,0), 
				new Vector3(9.5F,4.0f,0), new Vector3(10.5F,4.0f,0), new Vector3(11.5F,4.0f,0)};

			passengerPlaner = new MyCharacterController[6];

			if (_to_or_from == "from") {
				bank = Object.Instantiate (Resources.Load ("Prefabs/bank", typeof(GameObject)), from_pos, Quaternion.identity, null) as GameObject;
				bank.name = "from";
				to_or_from = 1;
			} else {
				bank = Object.Instantiate (Resources.Load ("Prefabs/bank", typeof(GameObject)), to_pos, Quaternion.identity, null) as GameObject;
				bank.name = "to";
				to_or_from = -1;
			}
		}

		public int getEmptyIndex() {
			for (int i = 0; i < passengerPlaner.Length; i++) {
				if (passengerPlaner [i] == null) {
					return i;
				}
			}
			return -1;
		}

		public Vector3 getEmptyPosition() {
			Vector3 pos = positions [getEmptyIndex ()];
			pos.x *= to_or_from;//从岸一边到达另一边的位置
			return pos;
		}

		public void getOnbank(MyCharacterController characterCtrl) {
			int index = getEmptyIndex ();
			passengerPlaner [index] = characterCtrl;
		}

		public MyCharacterController getOffbank(string passenger_name) {	// 0->priest, 1->devil
			for (int i = 0; i < passengerPlaner.Length; i++) {
				if (passengerPlaner [i] != null && passengerPlaner [i].getName () == passenger_name) {
					MyCharacterController charactorCtrl = passengerPlaner [i];
					passengerPlaner [i] = null;
					return charactorCtrl;
				}
			}
			Debug.Log ("cant find passenger on bank: " + passenger_name);
			return null;
		}

		public int get_to_or_from() {
			return to_or_from;
		}

		public int[] getCharacterNum() {
			int[] count = {0, 0};
			for (int i = 0; i < passengerPlaner.Length; i++) {
				if (passengerPlaner [i] == null)
					continue;
				if (passengerPlaner [i].getType () == 0) {	// 0->priest, 1->devil
					count[0]++;
				} else {
					count[1]++;
				}
			}
			return count;
		}

		public void reset() {
			passengerPlaner = new MyCharacterController[6];
		}
	}

	/*-----------------------------------BoatController------------------------------------------*/
	public class BoatController {
		readonly GameObject boat;
		readonly Moveable moveableScript;
		readonly Vector3 fromPosition = new Vector3 (3.0f, 1.5f, 0.0f);//小船出发地位置
		readonly Vector3 toPosition = new Vector3 (-3.0f, 1.5f, 0.0f);//小船目的地位置
		readonly Vector3[] from_positions;//角色在出发地小船上地位置
		readonly Vector3[] to_positions;//角色在目的地小船上地位置

		// change frequently
		int to_or_from; // to->-1; from->1
		MyCharacterController[] passenger = new MyCharacterController[2];

		public BoatController() {
			to_or_from = 1;

			from_positions = new Vector3[] { new Vector3 (4.5F, 3.0F, 0), new Vector3 (5.5F, 3.0F, 0) };
			to_positions = new Vector3[] { new Vector3 (-5.5F, 3.0F, 0), new Vector3 (-4.5F, 3.0F, 0) };

			boat = Object.Instantiate (Resources.Load ("Prefabs/boat", typeof(GameObject)), fromPosition, Quaternion.identity, null) as GameObject;
			boat.name = "boat";

			moveableScript = boat.AddComponent (typeof(Moveable)) as Moveable;
			boat.AddComponent (typeof(ClickGUI));
		}


		public void Move() {   //小船从一边移向另一边
			if (to_or_from == -1) {
				moveableScript.setDestination(fromPosition);
				to_or_from = 1;
			} else {
				moveableScript.setDestination(toPosition);
				to_or_from = -1;
			}
		}
		public Vector3 getBoatDest(){
			Vector3 pos;
			if (to_or_from == -1) {
				pos = fromPosition;
				//to_or_from = 1;
			} else {
				pos = toPosition;
				//to_or_from = -1;
			}
			return pos;
		}
		public void pos_change(){
			to_or_from = 0 - to_or_from;
		}
		public int getEmptyIndex() {
			for (int i = 0; i < passenger.Length; i++) {
				if (passenger [i] == null) {
					return i;
				}
			}
			return -1;
		}

		public bool isEmpty() {
			for (int i = 0; i < passenger.Length; i++) {
				if (passenger [i] != null) {
					return false;
				}
			}
			return true;
		}

		public Vector3 getEmptyPosition() {
			Vector3 pos;
			int emptyIndex = getEmptyIndex ();
			if (to_or_from == -1) {
				pos = to_positions[emptyIndex];
			} else {
				pos = from_positions[emptyIndex];
			}
			return pos;
		}
		
		public void GetOnBoat(MyCharacterController characterCtrl) {
			int index = getEmptyIndex ();
			passenger [index] = characterCtrl;
		}

		public MyCharacterController GetOffBoat(string passenger_name) {
			for (int i = 0; i < passenger.Length; i++) {
				if (passenger [i] != null && passenger [i].getName () == passenger_name) {
					MyCharacterController charactorCtrl = passenger [i];
					passenger [i] = null;
					return charactorCtrl;
				}
			}
			Debug.Log ("Cant find passenger in boat: " + passenger_name);
			return null;
		}

		public GameObject getGameobj() {
			return boat;
		}

		public int get_to_or_from() { // to->-1; from->1
			return to_or_from;
		}

		public int[] getCharacterNum() {    //返回牧师与恶魔地数量
			int[] count = {0, 0};
			for (int i = 0; i < passenger.Length; i++) {
				if (passenger [i] == null)
					continue;
				if (passenger [i].getType () == 0) {	// 0->priest, 1->devil
					count[0]++;
				} else {
					count[1]++;
				}
			}
			return count;
		}

		public void reset() {
			if (to_or_from == -1) {
				//Move ();      //小船归位
				boat.transform.position = fromPosition;
            	to_or_from = 1;
			}
			moveableScript.reset ();   //move_state = 0,小船不移动
			passenger = new MyCharacterController[2];
		}
	}