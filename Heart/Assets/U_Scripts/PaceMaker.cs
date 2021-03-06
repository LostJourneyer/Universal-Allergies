using UnityEngine;
using System.Collections;

public class PaceMaker : MonoBehaviour {
	public AudioClip[] m_HeartBeats;
	// Use this for initialization
	void Start () {
		audio.volume=10f;
		audio.clip=m_HeartBeats[0];
		audio.Play();
		audio.ignoreListenerVolume=true;
	}
	
	// Update is called once per frame
	void Update () {
		if(!audio.isPlaying){
			audio.clip=m_HeartBeats[Random.Range(0,6)];
			audio.Play();
		}
	}
}