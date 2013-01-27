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
			else
			{
				agent.destination = (transform.position + transform.forward)*10;
				agent.destination += Vector3.Normalize(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)))*30;
			}
		}
	}
}
