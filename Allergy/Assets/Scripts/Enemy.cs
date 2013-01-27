using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public AllergyType EnemyType;
	public NavMeshAgent agent;
	public Vector3[] waypoints;
	int current=0;
	void Start()
	{
		agent.destination = waypoints[0];
		animation.clip.wrapMode = WrapMode.Loop;
		animation.Play();
	}
	void Update()
	{
		if(Vector3.Distance(transform.position, waypoints[current]) < 10)
		{
			current++;
			if(current >= waypoints.Length)
				current = 0;
			agent.destination = waypoints[current];
		}
	}
}
