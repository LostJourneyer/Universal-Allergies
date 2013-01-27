using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public AllergyType EnemyType;
	public NavMeshAgent agent;
	public Vector3[] waypoints;
	public bool wander = false;
	int current=0;
	void Start()
	{
		Random.seed = (int)Time.time;
		if(waypoints.Length >0)
			agent.destination = waypoints[0];
		animation.clip.wrapMode = WrapMode.Loop;
		animation.Play();
	}
	void Update()
	{
		if(Vector3.Distance(transform.position, agent.destination) < 10)
		{
			if(!wander)
			{
				current++;
				if(current >= waypoints.Length)
					current = 0;
				agent.destination = waypoints[current];
			}
		}
		if(wander)
			agent.destination += (new Vector3(Random.value - 0.5f, 0, Random.value - 0.5f))*25;
	}
}
