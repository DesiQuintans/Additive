using UnityEngine;
using System.Collections;

public class InLevelGUI : MonoBehaviour {
	
	public string levelNumber = "";
	public string levelDescription;
	public string levelDescription2;
	public GUISkin customSkin;
	
	void OnGUI () {
		
		GUI.skin = customSkin;
		
//		string levelString = levelNumber + ".\n" + levelDescription;
		string levelString =  levelDescription + "\n" + levelDescription2;
		
		GUILayout.BeginArea (new Rect (0, (Screen.height/7)*6, Screen.width, Screen.height/10));
				GUILayout.BeginHorizontal ();
					if (levelNumber != "")
					{
						GUI.Box (new Rect (Screen.width*0.05f, 0, Screen.width*0.9f, Screen.height/8), levelString);
					}
				GUILayout.EndHorizontal ();
		GUILayout.EndArea ();
		
	}
	
}
