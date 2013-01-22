using UnityEngine;
using System.Collections;

public class ShapeScript : MonoBehaviour {
	
	public Color myColor;
	private bool selected;
	private bool moving;
	private Vector3 myPosition;
	private Vector3 destination;
	private Color flashColor = Color.white;
	private Color highlightColor;
	private float moveTime = 0;
	private GameObject frame = null;
	public GameObject selectionFrame;
    
    void Start ()
    {
    	myPosition = transform.position;
    	myColor = renderer.material.GetColor ("_Color");
		highlightColor = myColor + new Color (0.4f, 0.4f, 0.4f);
		
    }
    
	void OnMouseEnter ()
	{
		renderer.material.SetColor ("_Color", highlightColor);
	}
	
	void OnMouseExit ()
	{
		renderer.material.SetColor ("_Color", myColor);
	}
	
	void Update ()
	{
		if (selected)
		{
			renderer.material.SetColor ("_Color", Color.Lerp(myColor, flashColor, (Mathf.Sin(Time.time*2))));
		}
		if (moving)
		{
			moveTime += Time.deltaTime * 12;
			transform.position = Vector3.Lerp(myPosition, destination, moveTime);
			
			if (transform.position == destination)
			{
				myPosition = transform.position;
				moveTime = 0;
				moving = false;
			}
		}
	}
	
	public void toggleSelected (bool thingIsSelected)
	{
		selected = thingIsSelected;
		if (frame == null)
		{
			frame = (GameObject) Instantiate (selectionFrame, transform.position, Quaternion.Euler(-90, 0, 0));
			frame.transform.parent = transform;
		}
		
		if (thingIsSelected == false)
		{
			renderer.material.SetColor ("_Color", myColor);
			Destroy (frame);
			frame = null;
		}
	}
	
	public void moveTile (bool tileIsMoving, int row, int column)
	{
		moving = tileIsMoving;
		destination = new Vector3 (column, 0, row);
	}
	
	public void setColor (Color newColor)
	{
		renderer.material.SetColor ("_Color", newColor);
		myColor = newColor;
		highlightColor = myColor + new Color (0.4f, 0.4f, 0.4f);
	}
}