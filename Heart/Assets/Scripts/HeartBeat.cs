using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour {
	public float m_particalMultiplyer=10;
	public float m_colorMultiplyer;
	public float m_tolerance;
	public TextMesh m_ScoreDisplay;
	
	private float m_max=0.06877659f;		//max spectrum value
	private AudioSource m_AS;
	private ParticleSystem m_PS;
	private int m_score=0;
	
	public void Start()
	{	
		m_AS=(AudioSource)gameObject.GetComponent<AudioSource>();
		m_PS=(ParticleSystem)gameObject.GetComponent<ParticleSystem>();
	}
	public void Update()
	{
        float[] spectrum = m_AS.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);
		float normalizedSpect=Mathf.Min(1f, spectrum[0]/m_max);
		Debug.Log("ns "+normalizedSpect+ ", Spect "+spectrum[0]+", m_max "+m_max);
        m_PS.startSpeed =(normalizedSpect)*m_particalMultiplyer;
		particleSystem.startColor=new Color(Mathf.Max(normalizedSpect,.9f),Mathf.Max(spectrum[1]/m_max,.7f),spectrum[2]/m_max,1f);
		if(Input.GetKeyDown(KeyCode.Space)){
			if(normalizedSpect>m_tolerance){
				balance();
			}else{
				unbalance();
			}
		}
		m_ScoreDisplay.text="Score:"+m_score;
	}
	private void balance()
	{
		m_score++;
	}
	private void unbalance()
	{
	}
}
