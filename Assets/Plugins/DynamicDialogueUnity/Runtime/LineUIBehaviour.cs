using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicDialogue.Unity
{
	/// <summary>
	/// Abstract Monobehaviour to show the Lines to display.
	/// Works in conjunction with the 
	/// TODO finish summary
	/// </summary>
	public abstract class LineUIBehaviour : MonoBehaviour
	{
		public abstract void RunLine(string line);
	}
}