using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicDialogue;
using DynamicDialogue.Core;
using DynamicDialogue.Compiler;

namespace DynamicDialogue.Unity
{
	/// <summary>
	/// This manager acts as the interface between Unity Engine
	/// and the DynamicDialogue.
	/// </summary>
	public class DialogueManager : MonoBehaviour
	{
		#region Fields

		/// <summary>
		/// Collection of Bark Files to compile and include on Start
		/// </summary>
		public TextAsset[] barkFiles;

		/// <summary>
		/// Reference to the UI to display lines
		/// </summary>
		public LineUI lineUI;

		/// <summary>
		/// The storages that shall be put into queries.
		/// </summary>
		public VariableStorageBehaviour[] linkedStorages;

		protected Dialogue dialogue;

		#endregion

		#region Properties

		public Dialogue Dialogue => dialogue ?? (dialogue = CreateDialogue());

		#endregion

		#region Private Methods

		protected Dialogue CreateDialogue()
		{
			return new Dialogue()
			{
				LogDebugMessage = Debug.Log,
				LogErrorMessage = Debug.LogError,
				TextResponseHandler = lineUI.RunLine,
				TriggerResponseHandler = trigger => { Debug.Log($"TRIGGER: Concept {trigger.ConceptName} to {trigger.To}"); },
				StorageChangeHandler = change => { Debug.Log("CHANGE: " + change.ToString()); }
			};
		}

		#endregion

		#region Public Methods

		public void Add(TextAsset barkFile)
		{
			Dialogue.LoadProgram(barkFile.name);
		}

		public void Remove(TextAsset barkFile)
		{
			Dialogue.UnloadPack(barkFile.name);
		}

		public void Clear()
		{
			Dialogue.UnloadAll();
		}

		public void RunQuery()
		{
			Dialogue.Query(linkedStorages);
		}

		public void RunQuery(IVariableStorage query, bool includeLinkedStorages = true)
		{
			IVariableStorage[] completeQuery = new IVariableStorage[(includeLinkedStorages) ? linkedStorages.Length + 1 : 1];
			if (includeLinkedStorages)
				for (int i = 0; i < linkedStorages.Length; ++i)
					completeQuery[i] = linkedStorages[i];

			completeQuery[completeQuery.Length] = query;
			RunQuery(completeQuery);
		}

		public void RunQuery(IVariableStorage[] query)
		{
			Dialogue.Query(query);
		}

		#endregion
	}
}
