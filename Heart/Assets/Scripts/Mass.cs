using UnityEngine;
using System.Collections;

public class Mass : MonoBehaviour {

	public float m_size;
	public Vector3 m_vel;
	private Rigidbody m_body;
	
	// Use this for initialization
	void Start () {
		GravityManager.GM.m_masses.Add(this);
		m_body=(Rigidbody)GetComponent<Rigidbody>();
		m_body.velocity=m_vel;
		m_body.mass=m_size;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 force=GravityManager.ApplyGravity(this);
		m_body.AddForce(force*Time.fixedDeltaTime);
	}
}
