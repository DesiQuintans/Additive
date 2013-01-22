using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrayHelper2D {

	public static bool isIndexValid (char[,] thisArray, int row, int column)
	{
		// Must check the number of rows at this point, or else an OutOfRange exception gets thrown when checking number of columns.
		if (row < thisArray.GetLowerBound (0) || row > (thisArray.GetUpperBound (0))) return false;
		
		if (column < thisArray.GetLowerBound (1) || column > (thisArray.GetUpperBound (1))) return false;
		else return true;
	}
			
	public static bool areSurroundingsValid (char[,] thisArray, int row, int column)
	{
			if (isIndexValid (thisArray, (row-1), column) == false) return false;
			else if (isIndexValid (thisArray, (row+1), column) == false) return false;
			else if (isIndexValid (thisArray, row, (column-1)) == false) return false;
			else if (isIndexValid (thisArray, row, (column+1)) == false) return false;
			else return true;
	}
	
	public static void floodFill2D (char[,] thisArray, char filler)
	{
		for (int row = 0; row < (thisArray.GetUpperBound (0) + 1); row++)
		{
			for (int column = 0; column < (thisArray.GetUpperBound (1) + 1); column++)
			{
				thisArray[row, column] = filler;
			}
		}
	}
	
	public static void print2D (char[,] thisArray)
	{
		// 2D array lengths increased by 1 to ensure that the whole array is iterated through.
		// For a 2D with 15 rows, for example, GetUpperBound(0) will return 15, and a for(row < GetUpperBound(0)) will stop at row = 14.
		int maxRows = thisArray.GetUpperBound (0) + 1;
		int maxColumns = thisArray.GetUpperBound (1) + 1;
		string printout = "Contents of 2D Array (" + maxRows + "x" + maxColumns + "):\n";
		
		for (int row = 0; row < maxRows; row++)
		{
			for (int column = 0; column < maxColumns; column++)
			{
				if (isIndexValid (thisArray, row, column) == true)
				{
					printout += thisArray[row, column];
				}
				else
				{
					printout += '_';
				}
			}
			printout += "\t\t\t\t\t\t\t>>>" + row + "\n";
		}
		
		Debug.Log (printout);
	}
	
	public static char[,] convertJaggedTo2D (string[] jaggedArray, char filler)
	{
		int maxWidth = 0;
		foreach (string line in jaggedArray)
		{
			if (line.Length > maxWidth) maxWidth = line.Length;
		}
		
		
		char[,] array2D = new char[jaggedArray.Length, maxWidth];
		
		floodFill2D (array2D, filler);
		
		for (int row = 0; row < (jaggedArray.Length); row++)
		{
			for (int column = 0; column < (jaggedArray[row].Length); column++)
			{
				array2D[row, column] = jaggedArray[row][column];
			}
		}

		return array2D;
	}
	
	public static char[,] getNeighbouringIndices (char[,] thisArray, int row, int column)
	{
		char[,] arr = new char[3,3];
		
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				int tempRow = row + i;
				int tempColumn = column + j;
				
				if (isIndexValid (thisArray, tempRow, tempColumn) == true)
				{
					arr[(i + 1), (j + 1)] = thisArray[tempRow, tempColumn];
				}
				else
				{
					arr[(i + 1), (j + 1)] = '_';
				}
			}
		}
		
		return arr;
	}
	
	public static char[] listNeighbouringIndices (char[,] thisArray, int row, int column)
	{
		char[] arr = new char[9];
		int arrCounter = 0;
		
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				int tempRow = row + i;
				int tempColumn = column + j;
				
				if (isIndexValid (thisArray, tempRow, tempColumn) == true)
				{
					arr[arrCounter] = thisArray[tempRow, tempColumn];
				}
				else
				{
					arr[arrCounter]  = '_';
				}
				
				arrCounter++;
			}
		}
		
		return arr;
	}
}
