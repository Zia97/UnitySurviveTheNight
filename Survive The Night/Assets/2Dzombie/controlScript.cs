//Author : THIRTYSIXLAB
using UnityEngine;
using System.Collections;

public class controlScript : MonoBehaviour {
	
	//---------------------------
	void OnGUI() {
		
		 GUIStyle customButton = new GUIStyle("button");
		customButton.fontSize =24;
		
        if (GUI.Button(new Rect(10, 10, 150, 50), "Enter",customButton))PlayAnim("enter");
		
		if (GUI.Button(new Rect(10, 80, 150, 50), "Idle1",customButton))PlayAnim("idle1");
		
		if (GUI.Button(new Rect(10, 150, 150, 50), "Idle2",customButton))PlayAnim("idle2");
		
		if (GUI.Button(new Rect(10, 220, 150, 50), "WalkSide",customButton))PlayAnim("walkSide");
		
		if (GUI.Button(new Rect(10, 290, 150, 50), "WalkUp",customButton))PlayAnim("walkUp");
		
		if (GUI.Button(new Rect(10, 360, 150, 50), "WalkDown",customButton))PlayAnim("walkDown");
		
		if (GUI.Button(new Rect(10, 430, 150, 50), "JumpSide",customButton))PlayAnim("jumpSide");
		
		if (GUI.Button(new Rect(10, 500, 150, 50), "JumpUp",customButton))PlayAnim("jumpUp");
		
		if (GUI.Button(new Rect(10, 570, 150, 50), "JumpDown",customButton))PlayAnim("jumpDown");
		
		
		if (GUI.Button(new Rect(Screen.width-160, 10, 150, 50), "StrikeSide",customButton))PlayAnim("strike");
		
		if (GUI.Button(new Rect(Screen.width-160, 80, 150, 50), "StrikeUp",customButton))PlayAnim("strikeUp");
		
		if (GUI.Button(new Rect(Screen.width-160, 150, 150, 50), "StrikeDown",customButton))PlayAnim("strikeDown");
		
		if (GUI.Button(new Rect(Screen.width-160, 220, 150, 50), "DefendSide",customButton))PlayAnim("defendSide");
		
		if (GUI.Button(new Rect(Screen.width-160, 290, 150, 50), "DefendUp",customButton))PlayAnim("defendUp");
		
		if (GUI.Button(new Rect(Screen.width-160, 360, 150, 50), "DefendDown",customButton))PlayAnim("defendDown");
		
		if (GUI.Button(new Rect(Screen.width-160, 430, 150, 50), "Bite",customButton))PlayAnim("bite");
		
		if (GUI.Button(new Rect(Screen.width-160, 500, 150, 50), "Die",customButton))PlayAnim("die");
		
		if (GUI.Button(new Rect(Screen.width-160, 570, 150, 50), "Headshot",customButton))PlayAnim("headshot");
		
		
    }// end of ongui
	
	void PlayAnim(string nameState){
		
        foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("zombies")) {
            zombie.GetComponent<Animator>().Play(nameState,0,0);
        }
	}
}
