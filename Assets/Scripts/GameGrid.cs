using UnityEngine;
using System.Collections;

public class GameGrid : MonoBehaviour {
	
	public GameObject square;
	public TextAsset levelFile;
	
	private Camera mainCamera;
	
	void Awake ()
	{
		mainCamera = Camera.main;
		rebuildLevel (levelFile);
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.LoadLevel ("Title");
		}
	}
	
	void rebuildLevel (TextAsset levelData)
	{
		string[] jaggedArray = levelData.text.Split ('\n');
		char[,] gameGrid = ArrayHelper2D.convertJaggedTo2D (jaggedArray, ' ');
		
		
		int height = gameGrid.GetUpperBound (0) + 1;
		int width = gameGrid.GetUpperBound (1) + 1;
		
		for (int row = 0; row < height; row++)
		{
			for (int column = 0; column < width; column++)
			{
				if (gameGrid[row, column] == ' ') continue;
				
				spawnNewShape (gameGrid[row, column], row+1, column+1);
			}
		}
		
		float gridHeight = height + 1;
		float gridWidth = width + 1;
		
		float cameraDepth = -gridHeight;
		float cameraTrack = gridWidth / 2;
		float cameraHeight = gridHeight * 0.6f;
		
		mainCamera.GetComponent<CameraMover>().teleportCamera (gridWidth/2, -1, -gridHeight/2);
		mainCamera.GetComponent<CameraMover>().moveCamera (cameraTrack, cameraHeight, cameraDepth);
	}
	
	void spawnNewShape (char shapeID, int row, int column)
	{
		if (shapeID == ' ')
			return;
		
		Color color;
		
		switch (shapeID)
		{
			case 'a':
				color = ShapeColor.black;
				break;
			case '#':
				color = ShapeColor.black;
				break;
			case 'r':
				color = ShapeColor.red;
				break;
			case 'g':
				color = ShapeColor.green;
				break;
			case 'b':
				color = ShapeColor.blue;
				break;
			case 'p':
				color = ShapeColor.purple;
				break;
			case 'y':
				color = ShapeColor.yellow;
				break;
			case 't':
				color = ShapeColor.teal;
				break;
			case 'R':
				color = ShapeColor.red;
				break;
			case 'G':
				color = ShapeColor.green;
				break;
			case 'B':
				color = ShapeColor.blue;
				break;
			case 'P':
				color = ShapeColor.purple;
				break;
			case 'Y':
				color = ShapeColor.yellow;
				break;
			case 'T':
				color = ShapeColor.teal;
				break;
			default:
				color = ShapeColor.black;
				break;
		}
		
		GameObject newShape = (GameObject) Instantiate (square, new Vector3(column, 0, -row), Quaternion.Euler (-90, 0, 0));
		newShape.renderer.material.SetColor ("_Color", color);
	}
}