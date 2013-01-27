using UnityEngine;
using System.Collections;

public class Mass : MonoBehaviour {

	private Rigidbody m_body;
	public bool merger=true;
	
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
		if(other.gameObject.tag=="Mass"){
			Mass m=(Mass)other.gameObject.GetComponent<Mass>();
			if(m.merger){
				merger=false;
			}else{
				transform.localScale=transform.localScale*1.2f;
				rigidbody.velocity=rigidbody.velocity+m.rigidbody.velocity;
				rigidbody.mass=rigidbody.mass+m.rigidbody.mass;
				Camera.mainCamera.transform.Translate(0f,0f,-1f);
				Destroy(other.gameObject);
			}
		}
	}
	public void OnDestroy()
	{
		GravityManager.removeMass(this);
	}
}
