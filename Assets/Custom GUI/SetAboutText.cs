using UnityEngine;
using System.Collections;

public class SetAboutText : MonoBehaviour {
	
	public AudioClip selected;
	
	void Start () {
		guiText.text = "'Additive'\n";
		guiText.text += "(Post-Competition Version)\n";
		guiText.text += "A puzzle game made in 48 hours for Ludum Dare 24.\n\n";
		guiText.text += "Uses 'Metrophobic' font by Vernon Adams, under Open Font License 1.1.\n\n";
		guiText.text += "Desi Quintans (CowfaceGames.com), August 2012.";
	}
	
	public GUISkin customSkin;
	
	void OnGUI () {
		
		GUI.skin = customSkin;
		
		GUILayout.BeginArea (new Rect (Screen.width/2 - Screen.width/8, Screen.height/1.25f, Screen.width/4, Screen.height/3));

			if (GUILayout.Button ("Back to menu")) {
				audio.PlayOneShot (selected);
				Destroy (GameObject.Find ("Music"));
				Application.LoadLevel ("Title");
			}
			
		GUILayout.EndArea ();
		
	}
}
