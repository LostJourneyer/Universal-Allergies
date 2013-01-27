using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
	public float m_duration;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		m_duration=m_duration-Time.deltaTime;
		if(m_duration<0)
			gameObject.SetActive(false);
	}
//	void OnCollisionEnter(Collision other){
	void OnTriggerEnter(Collider other){
		Debug.Log("Blocked"+other.gameObject.name);
		if(other.gameObject.tag=="Mass"){
			Destroy(other.gameObject);
			gameObject.SetActive(false);
		}
	}
	public void PowerOn(int dur){
		m_duration=(float)dur;
	}
}
