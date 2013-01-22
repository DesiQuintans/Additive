using UnityEngine;

public class ColorLerp : MonoBehaviour {
	
	private Color myColor = Color.grey;
	private Color flashColor = Color.white;
	
	void Update () {
		renderer.material.SetColor ("_Color", Color.Lerp(myColor, flashColor, (Mathf.Sin(Time.time*4))));
	}
}
