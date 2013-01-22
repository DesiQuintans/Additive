using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlBlock : MonoBehaviour {
	
	public string nextLevel = "Title"; 
	
	public AudioClip deselectSound; 
	public AudioClip selectSound; 
	public AudioClip invalid;
	public AudioClip replacementSound;
	public AudioClip mergingSound;
	public AudioClip mergingSound2;
	public AudioClip deletionSound;
	public AudioClip winSound;
	
	public GameObject emitter;
	
	GameObject collider1= null;
	int collider1Xpos;
	int collider1Zpos;
	
	GameObject collider2 = null;
	int collider2Xpos;
	int collider2Zpos;
	
	private Color black = ShapeColor.black;
	private Color red = ShapeColor.red;
	private Color green = ShapeColor.green;
	private Color blue = ShapeColor.blue;
	private Color yellow = ShapeColor.yellow;
	private Color purple = ShapeColor.purple;
	private Color teal = ShapeColor.teal;
	private Color error = ShapeColor.error;
	
	private List<Color> RGBList = ShapeColor.RGBList;
	private List<Color> PYTList = ShapeColor.PYTList;
	
	private bool waitBeforeDeletion = false;
	private GameObject colliderToDelete = null;
	private float waitTime = 0;
	
	private int shapesSpawned = 0;
	bool gameWon = false;
	float winSongLength = 4;
	
	void Start ()
	{
		shapesSpawned = GameObject.FindGameObjectsWithTag("Shape").Length;
	} 
	
	void Update ()
	{
		if (waitBeforeDeletion == true)
		{
			waitTime += Time.deltaTime;
			if (waitTime > 0.125f)
			{
				Destroy (colliderToDelete);
				waitTime = 0;
				waitBeforeDeletion = false;
			}
		}
	
		if (shapesSpawned == 0)
		{
			if (gameWon == false)
			{
				audio.PlayOneShot (winSound);
				gameWon = true;
			}
			
			winSongLength -= Time.deltaTime;
			
			if (winSongLength < 0)
			{
				Application.LoadLevel (nextLevel);
			}
		}
	
		if (Input.GetMouseButtonDown (0) == true)
		{
			RaycastHit colliderHit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out colliderHit, 100) == true)
			{
				if (collider1 == null && collider2 == null)
				{
					// Nothing is selected. Any tile is a valid target.
					
					audio.PlayOneShot (selectSound);
					collider1 = colliderHit.collider.gameObject;
					collider1Xpos = Mathf.RoundToInt(colliderHit.transform.position.x);
					collider1Zpos = Mathf.RoundToInt(colliderHit.transform.position.z);
					
					collider1.GetComponent<ShapeScript>().toggleSelected(true);
				}
				else if (collider1 == null && collider2 != null)
				{
					// Collider 1 has been deselected.
					
					collider2.GetComponent<ShapeScript>().toggleSelected(false);
					collider2 = null;
					collider2Xpos = 0;
					collider2Zpos = 0;
				}
				else
				{
					// The first tile has been selected. The other must be within 1 tile range, and not be the same tile.
					GameObject tempCollider2 = colliderHit.collider.gameObject;
					int tempCollider2Xpos = Mathf.RoundToInt(colliderHit.transform.position.x);
					int tempCollider2Zpos = Mathf.RoundToInt(colliderHit.transform.position.z);
					
					if (tempCollider2.GetInstanceID() == collider1.GetInstanceID())
					{
						// Same tile selected, so deselect it.
						audio.PlayOneShot (deselectSound, 0.5f);
						collider1.GetComponent<ShapeScript>().toggleSelected(false);
						collider1 = null;
						collider1Xpos = 0;
						collider1Zpos = 0;
						
						if (collider2 != null)
						{
							// Collider 1 has been deselected.
							
							collider2.GetComponent<ShapeScript>().toggleSelected(false);
							collider2 = null;
							collider2Xpos = 0;
							collider2Zpos = 0;
						}
						return;
					}
					
					// A tile is NSEW of the current position if only one of its coords have changed (not both).
					if ((tempCollider2Xpos != collider1Xpos) && (tempCollider2Zpos != collider1Zpos))
					{
						audio.PlayOneShot (invalid);
						return;
					}
					
					if 	(	((tempCollider2Xpos == (collider1Xpos + 1)) || (tempCollider2Xpos == (collider1Xpos - 1)))
								||
							((tempCollider2Zpos == (collider1Zpos + 1)) || (tempCollider2Zpos == (collider1Zpos - 1)))	)
					{
						audio.PlayOneShot (selectSound, 0.5f);
						collider2 = tempCollider2;
						collider2.GetComponent<ShapeScript>().toggleSelected(true);
						collider2Xpos = tempCollider2Xpos;
						collider2Zpos = tempCollider2Zpos;
					}
					else
					{
						audio.PlayOneShot (invalid);
						return;
					}
					
					audio.PlayOneShot (selectSound, 0.5f);
					collider2 = tempCollider2;
					collider2.GetComponent<ShapeScript>().toggleSelected(true);
					collider2Xpos = tempCollider2Xpos;
					collider2Zpos = tempCollider2Zpos;
				}
				
				if ((collider1Xpos != 0 | collider1Zpos != 0) && (collider2Xpos != 0 | collider2Zpos != 0))
				{
					Color newColor = mixColors (collider1, collider2);
					
					if (newColor == error)
					{
						// A PYT tile is trying to mix with something that isn't a black tile, which is invalid.
						collider2.GetComponent<ShapeScript>().toggleSelected(false);
						collider2 = null;
						collider2Xpos = 0;
						collider2Zpos = 0;
						audio.PlayOneShot (invalid);
						return;
					}
					
					Instantiate (emitter, new Vector3 (collider2Xpos, 0, collider2Zpos), Quaternion.identity);
					
					collider1.GetComponent<ShapeScript>().moveTile (true, collider2Zpos, collider2Xpos);
					collider1.GetComponent<ShapeScript>().setColor (newColor);
					
					Destroy (collider2);
					shapesSpawned--;
					collider2 = null;
					collider2Xpos = 0;
					collider2Zpos = 0;
					
					// This is not a switch because the Color datatype doesn't seem to be supported as Switch argument. 
					if (newColor == Color.black)
					{
						audio.PlayOneShot (deletionSound);
						colliderToDelete = collider1;
						waitBeforeDeletion = true;
						shapesSpawned--;
					}
					else if (newColor == black)
						audio.PlayOneShot (replacementSound);
					else if (newColor == red || newColor == green || newColor == blue)
						audio.PlayOneShot (mergingSound);
					else if (newColor == purple || newColor == yellow || newColor == teal)
						audio.PlayOneShot (mergingSound2);
					
					collider1.GetComponent<ShapeScript>().toggleSelected(false);
					collider1 = null;
					collider1Xpos = 0;
					collider1Zpos = 0;
				}
			}
			else
			{
				// If the background is clicked, clear all selected tiles.
				if (collider1 != null)
				{
					collider1.GetComponent<ShapeScript>().toggleSelected(false);
					collider1 = null;
					collider1Xpos = 0;
					collider1Zpos = 0;
					audio.PlayOneShot (deselectSound, 0.5f);
				}

				if (collider2 != null)
				{
					collider2.GetComponent<ShapeScript>().toggleSelected(false);
					collider2 = null;
					collider2Xpos = 0;
					collider2Zpos = 0;
					audio.PlayOneShot (deselectSound, 0.5f);
				}
			}
		}
	}
	
	Color mixColors (GameObject collider, GameObject collider2)
	{
		// Black tiles always swallow up the other.
		// RGB tiles blend to make a secondary color.
		
		Color collider1Color = collider1.GetComponent<ShapeScript>().myColor;
		Color collider2Color = collider2.GetComponent<ShapeScript>().myColor;
		
		// Handles black deletions
		if (collider1Color == black && collider2Color == black)
			return Color.black;
		
		// Handles black replacements and same-color pairings.
		if (collider1Color == black || collider2Color == black)
			return black;
			
		if (collider1Color == collider2Color)
			return collider1Color;
		
		// I later decided that PYT and RGB should not be allowed to mix.
		if (RGBList.Contains (collider1Color) && PYTList.Contains (collider2Color))
			return error;
		
		if (RGBList.Contains (collider2Color) && PYTList.Contains (collider1Color))
			return error;
		
		// RGB/RGB pairings.
		if ((collider1Color == red || collider2Color == red) && (collider1Color == green || collider2Color == green))
			return yellow;
		
		if ((collider1Color == red || collider2Color == red) && (collider1Color == blue || collider2Color == blue))
			return purple;
		
		if ((collider1Color == green || collider2Color == green) && (collider1Color == blue || collider2Color == blue))
			return teal;
		
		// Like-with-unlike PYT/PYT pairings are invalid.
		if ((PYTList.Contains (collider1Color) && PYTList.Contains (collider2Color)) && (collider1Color != collider2Color))
		{
			return error;
		}
		
		// Default, if it's gotten this far. It's a black tile so that the puzzle is always solvable.
		return black;
	}
}
