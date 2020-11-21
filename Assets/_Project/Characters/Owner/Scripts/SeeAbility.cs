using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicDialogue.Core;

[RequireComponent(typeof(QueryManager))]
[RequireComponent(typeof(IVariableStorage))]
public class SeeAbility : MonoBehaviour
{
	private const int MaxHitCount = 10;
	private const string SeenPrefix = "Seen";

	[SerializeField]
	protected float range = 5f;
	[SerializeField]
	protected float angle = 45f;
	[SerializeField]
	protected LayerMask layerMask;
	[SerializeField]
	protected float intervalDuration = 3f;

	private QueryManager queryManager;
	private float lastLookTime = 0f;
	private Collider[] hitCache;
	private MemoryVariableStorage query;
	private IVariableStorage memory;
	private HashSet<GameObject> seenObjects = new HashSet<GameObject>();

	private float HalfAngle => angle / 2f;

	protected virtual void Awake()
	{
		hitCache = new Collider[MaxHitCount];
		queryManager = GetComponent<QueryManager>();
		query = new MemoryVariableStorage();
		query.SetValue("ConceptSee", true);
		memory = GetComponent<IVariableStorage>();
	}

	protected virtual void Update()
	{
		if (Time.time - lastLookTime > intervalDuration)
		{
			lastLookTime = Time.time;

			var hitCount = Physics.OverlapSphereNonAlloc(transform.position, range, hitCache, layerMask);
			for (int i = 0; i < hitCount; ++i)
			{
				GameObject currentObject = hitCache[i].transform.gameObject;

				if (Vector3.Angle(transform.forward, hitCache[i].transform.position - transform.position) <= HalfAngle && // check view cone
					seenObjects.Contains(currentObject) == false) // check if already seen before
				{
					seenObjects.Add(currentObject);
					if (memory.TryGetValue<float>(SeenPrefix + currentObject.name, out var count))
					{
						memory.SetValue(SeenPrefix + currentObject.name, count + 1f);
					}
					else
					{
						memory.SetValue(SeenPrefix + currentObject.name, 1f);
					}
					Debug.Log("QUERY: seen " + currentObject.name);
					queryManager.Query(query);
				}
			}
		}
	}

}
