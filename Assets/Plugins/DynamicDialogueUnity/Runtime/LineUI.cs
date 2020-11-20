using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DynamicDialogue.Unity
{
	/// <summary>
	/// Basic implementation for <see cref="LineUIBehaviour"/>.
	/// </summary>
	public class LineUI : LineUIBehaviour
	{
		public TextMeshProUGUI textField;
		public float textDelay = 3f;

		public UnityEvent onLineStart;
		public UnityEvent onLineEnd;

		protected Coroutine lineCoroutine;

		public override void RunLine(string line)
		{
			if (lineCoroutine != null)
			{
				StopCoroutine(lineCoroutine);
			}

			lineCoroutine = StartCoroutine(ERunLine(line));
		}

		protected virtual IEnumerator ERunLine(string line)
		{
			onLineStart?.Invoke();

			textField.text = line;

			yield return new WaitForSeconds(textDelay);

			onLineEnd?.Invoke();
			lineCoroutine = null;
		}
	}
}
