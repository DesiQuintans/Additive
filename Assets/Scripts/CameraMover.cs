using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {
	
	private bool moving = false;
	private float moveTime = 0;
	private Vector3 myPosition;
	private Vector3 destination;
	private Vector3 gridCentre;
	
	void Start ()
	{
		myPosition = transform.position;
	}
	
	void Update () {
		if (moving == true)
		{
			moveTime += Time.deltaTime;
			transform.position = Vector3.Lerp(myPosition, destination, moveTime);
			
			transform.LookAt (gridCentre, Vector3.up);
			
			if (transform.position == destination)
			{
				myPosition = transform.position;
				moveTime = 0;
				moving = false;
			}
		}
	}
	
	public void moveCamera (float x, float y, float z)
	{
		destination = new Vector3 (x, y, z);
		moving = true;
	}
	
	public void teleportCamera (float x, float y, float z)
	{
		transform.position = new Vector3 (x, y, z);
		myPosition = new Vector3 (x, y, z);
		gridCentre = new Vector3 (x, y, z);
	}
}
