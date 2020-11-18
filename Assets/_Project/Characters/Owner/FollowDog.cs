using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowDog : MonoBehaviour
{

	[SerializeField]
	protected Transform target;
	[SerializeField]
	protected float desiredRotationSpeed = 0.1f;

	protected NavMeshAgent agent;

	protected virtual void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	protected virtual void Update()
	{
		agent.SetDestination(target.position);
	}

	protected virtual void LookAt(Vector3 pos)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(pos), desiredRotationSpeed);
	}
}
