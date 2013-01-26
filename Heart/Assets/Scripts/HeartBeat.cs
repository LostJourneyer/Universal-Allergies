using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour {
	public float m_currentAudioOut;
	public float m_particalMultiplyer=10;
	
	private AudioSource m_AS;
	private ParticleSystem m_PS;
	
	public void Start()
	{	
		m_AS=(AudioSource)gameObject.GetComponent<AudioSource>();
		m_PS=(ParticleSystem)gameObject.GetComponent<ParticleSystem>();
	}
	public void Update()
	{
        float[] spectrum = audio.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);
        m_currentAudioOut =spectrum[0]*m_particalMultiplyer;
		m_PS.startSpeed =m_currentAudioOut;
/*		m_currentAudioOut=spectrum[i];
        while (i < 1023) {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.yellow);
            i++;
        }
*/	}
}
