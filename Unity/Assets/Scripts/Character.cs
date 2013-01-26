using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	public int HitPoints;
	public AllergyType[] Abilities;
	public AllergyType[] Allergy;
	public int [] AllergyStrength;
	Dictionary<int, int> Allergies = new Dictionary<int, int>();
	public CharacterController controller;
	public PlayerControl control;
	void Start()
	{
		for(int i=0;i<Abilities.Length;++i)
			Allergies.Add((int)Allergy[i], AllergyStrength[i]);
		controller = (CharacterController)GetComponent<CharacterController>();
	}
	public void Attack()
	{
		Vector3 attack = transform.position + transform.forward;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i=0;i<enemies.Length;++i)
		{
			if(Vector3.Distance(attack, enemies[i].transform.position) < 10)
			{
				Enemy foe = (Enemy)enemies[i].GetComponent("Enemy");
				for(int j=0;j<Abilities.Length;++j)
					if(foe.EnemyType == Abilities[j])
						Destroy(enemies[i]);
			}
		}
	}
    void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Goal")
		{
			control.KillMe(this);
		}
		if(other.tag == "Enemy")
		{
			Enemy Foe = other.gameObject.GetComponent<Enemy>();
			if(Allergies.ContainsKey((int)Foe.EnemyType))
			{
				Vector3 push = transform.position - other.collider.transform.position;
				transform.position += push;
				HitPoints -= Allergies[(int)Foe.EnemyType];
				if(HitPoints <= 0)
				{
					HitPoints = 0;
					Application.LoadLevel("Lose Screen");
				}
			}
		}
	}
}
