using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DynamicDialogue.Unity
{
	/// <summary>
	/// Stores variables in the current memory (not persistent)
	/// </summary>
	public class MemoryVariableStorage : VariableStorageBehaviour
	{
		private SortedList<string, object> variables = new SortedList<string, object>();

		public override string this[int index] => variables.Keys[index];

		public override int Count => variables.Count;

		[System.Serializable]
		public class DefaultVariable<T>
		{
			public string name;
			public T value;
		}

		public DefaultVariable<string>[] defaultStringValues;
		public DefaultVariable<float>[] defaultFloatValues;
		public DefaultVariable<bool>[] defaultBoolValues;

		internal void Awake()
		{
			ResetToDefaults();
		}

		internal void ResetToDefaults()
		{
			Clear();
			for (int i = 0; i < defaultStringValues.Length; ++i)
				SetValue(defaultStringValues[i].name, defaultStringValues[i].value);
			for (int i = 0; i < defaultFloatValues.Length; ++i)
				SetValue(defaultFloatValues[i].name, defaultFloatValues[i].value);
			for (int i = 0; i < defaultBoolValues.Length; ++i)
				SetValue(defaultBoolValues[i].name, defaultBoolValues[i].value);
		}

		#region VariableStorageBehaviour

		public override void Clear()
		{
			variables.Clear();
		}

		public override void GetAllValues(out IReadOnlyDictionary<string, object> values)
		{
			values = variables;
		}

		public override void SetValue(string variableName, string stringValue)
		{
			variables[variableName] = stringValue;
		}

		public override void SetValue(string variableName, float floatValue)
		{
			variables[variableName] = floatValue;
		}

		public override void SetValue(string variableName, bool boolValue)
		{
			variables[variableName] = boolValue;
		}

		public override bool TryGetValue<T>(string variableName, out T result)
		{
			if (variables.TryGetValue(variableName, out var foundValue))
			{
				if (foundValue is T t)
				{
					result = t;
					return true;
				}
				else
				{
					throw new System.ArgumentException($"Variable {variableName} is present, but is of type {foundValue.GetType()}, not {typeof(T)}");
				}
			}

			result = default;
			return false;
		}

		#endregion
	}
}
