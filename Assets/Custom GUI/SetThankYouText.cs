using UnityEngine;
using System.Collections;

public class SetThankYouText : MonoBehaviour {
	
	public AudioClip selected;
	
	void Start () {
		guiText.text = "Thanks for playing!\n";
		guiText.text += "May a thousand kittens mew at your passing!\n\n";
		guiText.text += "Desi Quintans, August 2012.\n";
		guiText.text += "CowfaceGames.com";
	}
	
	public GUISkin customSkin;
	
	void OnGUI () {
		
		GUI.skin = customSkin;
		
		GUILayout.BeginArea (new Rect (Screen.width/2 - Screen.width/8, Screen.height/2, Screen.width/4, Screen.height/3));

			if (GUILayout.Button ("Back to menu")) {
				audio.PlayOneShot (selected);
				Application.LoadLevel ("Title");
			}
			
		GUILayout.EndArea ();
		
	}
}
