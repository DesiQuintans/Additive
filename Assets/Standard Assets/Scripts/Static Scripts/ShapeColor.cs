using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ShapeColor
{
	public static Color black = ColorsXKCD.Charcoal;
	public static Color red = ColorsXKCD.BloodRed;
	public static Color green = ColorsXKCD.LimeGreen;
	public static Color blue = ColorsXKCD.Cobalt;
	public static Color yellow = ColorsXKCD.BrightYellow;
	public static Color purple = ColorsXKCD.PinkyPurple;
	public static Color teal = ColorsXKCD.BrightTeal;
	public static Color error = ColorsXKCD.Amethyst;
	
	public static List<Color> RGBList = new List<Color>() {red, green, blue};
	public static List<Color> PYTList = new List<Color>() {purple, yellow, teal};
}