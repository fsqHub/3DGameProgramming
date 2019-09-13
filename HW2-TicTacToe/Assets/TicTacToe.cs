using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour {
	private bool player_O = true;//where is the player O to go
	private int [,] board = new int[3, 3];//the chessboard

	void reset() {//reset the chessboard
		player_O = true;
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				board[i, j] = 0;
			}
		}
	}

	// initiation
	void Start() {
		reset();
	}

	int checkWinner() {//check whether  the game is over and if over,who is the winner
		// horizontal check
		for (int i = 0; i < 3; ++i) {
			if (board[i,0]!=0 && board[i, 0]==board[i,1] && board[i,0]==board[i,2]) {
				return board[i,0];
			}
		}
		// vertical check
		for (int i = 0; i < 3; ++i) {
			if (board[0,i]!=0 && board[0,i]==board[1,i] && board[0,i]==board[2,i]) {
				return board[0,i];
			}
		}
		// diagonal check
		if (board[1,1]!=0 && (board[1,1]==board[0,0] && board[1,1]==board[2,2])
			|| (board[1,1]==board[2,0] && board[1,1]==board[0,2])) {
			return  board[1,1];
		}
		//No winner! Then check where the game is over
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				if (board[i,j] == 0) return  0;//the board is not full,so it not over!
			}
		}

		return  4;//it's a dawn!
	}


	void OnGUI() {
		GUI.skin.button.fontSize = 30;
		GUIStyle myStyle1 = new GUIStyle();
		GUIStyle myStyle2 = new GUIStyle();
		GUIStyle myStyle = new GUIStyle();
		myStyle1.normal.textColor = Color.blue;
		myStyle2.normal.textColor = Color.red;
		myStyle.normal.textColor = Color.white;
		myStyle1.fontSize = myStyle2.fontSize = myStyle.fontSize = 30;
		myStyle1.alignment = myStyle2.alignment = myStyle.alignment = TextAnchor.MiddleCenter;

		if (GUI.Button(new Rect(225, 330, 225, 75), "Reset")) {
			reset();
			//GUI.Label(new Rect(220, 50, 275, 150), "Time to go!");
		}
		//whether the game is over
		int result = checkWinner();
		if (result == 4) {
			GUI.Label(new Rect(225, 15, 275, 150), "Oh,it's a draw!   ",myStyle);
		} else if (result == 1) {
			GUI.Label(new Rect(225, 15, 275, 150), "Game Over! O wins!",myStyle1);
		} else if (result == 2) {
			GUI.Label(new Rect(225, 15,275 , 150), "Game Over! X wins!",myStyle2);
		}
		//Not over! Then check the board update the text
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				if (board[i, j] == 1) {
					GUI.Button(new Rect(225+i*75, 100+j*75, 75, 75), "O",myStyle1);
				} else if (board[i, j] == 2) { //player X
					GUI.Button(new Rect(225+i*75, 100+j*75, 75, 75), "X",myStyle2);
				} else if (GUI.Button(new Rect(225+i*75, 100+j*75, 75, 75), "")) {
					if (result == 0) {
						if (player_O) {
							board[i, j] = 1;
						} else {
							board[i, j] = 2;
						}
						player_O = !player_O;
					}
				}
			}
		}
	}
}
