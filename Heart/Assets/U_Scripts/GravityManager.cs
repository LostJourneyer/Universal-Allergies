using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityManager
{
	public List<Mass> m_masses;
	private static GravityManager m_Gravity;
	public List<Mass> m_planets;
	
	private GravityManager (){
		m_masses = new List<Mass> ();
		m_planets = new List<Mass>();
	}
	
	public static GravityManager GM {
		get {
			if (m_Gravity == null) {
				m_Gravity = new GravityManager ();
			}
			return m_Gravity;
		}
	}

	public static Vector3 ApplyGravity (Mass body){
		Vector3 totalForces = new Vector3 ();
		foreach (Mass m in m_Gravity.m_masses) {
			if (body != m) {
				Vector3 toVec = m.transform.position - body.transform.position;
				totalForces =totalForces+ Mathf.Sqrt(toVec.sqrMagnitude*(body.rigidbody.mass+m.rigidbody.mass))*toVec.normalized;
//				Debug.Log(m.name);
			}
		}
//		Debug.Log(body.name+totalForces);
		return totalForces;
	}

	public static void removeMass (Mass m){
		m_Gravity.m_masses.Remove (m);
		m_Gravity.m_planets.Remove(m);
	}
	public static void Clear (){
		m_Gravity.m_masses = new List<Mass> ();
	}
	public static Mass GetRandomPlanet(){
		int numMasses=m_Gravity.m_planets.Count-1;
		return m_Gravity.m_planets[Random.Range(0, numMasses)];
	}
	public static void PowerPlanets(){
		foreach(Mass m in m_Gravity.m_planets){
			m.rigidbody.velocity=m.rigidbody.velocity*1.1f;
		}
	}
}