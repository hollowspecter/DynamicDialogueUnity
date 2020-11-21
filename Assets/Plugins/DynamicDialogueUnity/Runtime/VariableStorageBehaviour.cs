using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicDialogue.Core;

namespace DynamicDialogue.Unity
{
	/// <summary>
	/// A storage to save and retrieve data for queries.
	/// </summary>
	public abstract class VariableStorageBehaviour : MonoBehaviour, IVariableStorage
	{
		public abstract int Count
		{
			get;
		}

		public abstract string this[int index] { get; }

		public abstract void SetValue(string variableName, string stringValue);

		public abstract void SetValue(string variableName, float floatValue);

		public abstract void SetValue(string variableName, bool boolValue);

		public abstract bool TryGetValue<T>(string variableName, out T result);

		public abstract void GetAllValues(out IReadOnlyDictionary<string, object> values);

		public abstract void Clear();
	}
}
