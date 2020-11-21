using DynamicDialogue.Unity;
using DynamicDialogue.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IVariableStorage))]
public class QueryManager : MonoBehaviour
{
	[SerializeField]
	protected DialogueManager dialogueManager;
	[SerializeField]
	protected float minQueryInterval = 5f; // don't talk faster than every x seconds

	private IVariableStorage[] queryCache;
	private float lastQueryTime;

	protected virtual void Awake()
	{
		queryCache = new IVariableStorage[2];
		queryCache[0] = GetComponent<IVariableStorage>();
	}

	public void Query(IVariableStorage query)
	{
		if (Time.time - lastQueryTime < minQueryInterval)
			return;

		lastQueryTime = Time.time;
		queryCache[1] = query;
		dialogueManager.RunQuery(queryCache);
	}
}
