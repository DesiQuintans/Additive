using UnityEngine;
using System.Collections;

public class TitleButtons : MonoBehaviour {
	
	public AudioClip selected;
	public GUISkin customSkin;
	
	void OnGUI () {
		
		GUI.skin = customSkin;
		
		GUILayout.BeginArea (new Rect (Screen.width*0.10f, Screen.height*0.5f, Screen.width*0.80f, Screen.height*0.4f));
		
			GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("1"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("1");
				}
				if (GUILayout.Button ("2"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("2");
				}
				if (GUILayout.Button ("3"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("3");
				}
				if (GUILayout.Button ("4"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("4");
				}
				if (GUILayout.Button ("5"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("5");
				}
				if (GUILayout.Button ("6"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("6");
				}
				if (GUILayout.Button ("7"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("7");
				}
				if (GUILayout.Button ("8"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("8");
				}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
				if (GUILayout.Button ("9"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("9");
				}
				if (GUILayout.Button ("10"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("10");
				}
				if (GUILayout.Button ("11"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("11");
				}
				if (GUILayout.Button ("12"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("12");
				}
				if (GUILayout.Button ("13"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("13");
				}
				if (GUILayout.Button ("14"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("14");
				}
				if (GUILayout.Button ("15"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("15");
				}
				if (GUILayout.Button ("16"))
				{
					 audio.PlayOneShot (selected);
					 Application.LoadLevel ("16");
				}
			GUILayout.EndHorizontal ();
			
			GUILayout.FlexibleSpace ();
			
			if (GUILayout.Button ("About")) {
				audio.PlayOneShot (selected);
				DontDestroyOnLoad (GameObject.Find ("Music"));
				Application.LoadLevel ("About");
			}
			
//			if (GUILayout.Button ("Quit")) {
//				audio.PlayOneShot (selected);
//				Application.Quit();
//			}
			
		GUILayout.EndArea ();
		
	}
	
}
