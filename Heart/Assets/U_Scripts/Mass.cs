using UnityEngine;
using System.Collections;

public class Mass : MonoBehaviour {

	private Rigidbody m_body;
	
	// Use this for initialization
	void Start () {
		GravityManager.GM.m_masses.Add(this);
		m_body=(Rigidbody)GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 force=GravityManager.ApplyGravity(this);
		m_body.AddForce(force*Time.fixedDeltaTime);
	}
	public void OnCollisionEnter(Collision other){
		//Debug.LogError("pause");
	}
}
