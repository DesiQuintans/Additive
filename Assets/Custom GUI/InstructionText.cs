using UnityEngine;
using System.Collections;

public class InstructionText : MonoBehaviour {

	void Start () {
		guiText.text += "Choose a level and clear it of all colored tiles.\n";
		guiText.text += "[R] to restart a level, [Esc] to return here.\n";
		guiText.text += "Some colors will mix into new colors, but the resulting colors won't.\n";
		guiText.text += "Use black tiles to delete any other color.\n";
		guiText.text += "Finish level 16!\n";
	}
}
