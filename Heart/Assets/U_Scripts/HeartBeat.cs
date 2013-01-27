using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour {
	public float m_particalMultiplyer=10;
	public float m_colorMultiplyer;
	public float m_tolerance;
	public TextMesh m_ScoreDisplay;
	public GameObject m_newMass;
	public int m_baseEmissionRate=10;
	public int m_baseMax=1000;
	public int m_absMax=1000000;
	public int m_maxRate=1000;
	
	
	private float m_max=0.06877659f;		//max spectrum value
	private AudioSource m_AS;
	private ParticleSystem m_PS;
	private int m_score=0;
	private int m_curMax;
	private int m_curRate;
	private Mass m_mass;
	
	public void Start()
	{	
		m_AS=(AudioSource)gameObject.GetComponent<AudioSource>();
		m_PS=(ParticleSystem)gameObject.GetComponent<ParticleSystem>();
		m_mass=(Mass)gameObject.GetComponent<Mass>();
		m_curMax=m_baseMax;
		m_curRate=m_baseEmissionRate;
	}
	public void Update()
	{
        float[] spectrum = m_AS.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);
		float normalizedSpect=Mathf.Min(1f, spectrum[0]/m_max);
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
		float r=Random.Range(5, 10);
		if((m_curMax<m_absMax)||(m_curRate<m_maxRate)){
			m_curMax=m_curMax*10;
			m_curRate=m_curRate*10;
		}else{
			Vector3 newLoc=new Vector3(r,0f,0f);
			GameObject newMass=(GameObject)Instantiate(m_newMass,newLoc,Quaternion.identity);
			Mass m=newMass.GetComponent<Mass>();
			m.rigidbody.mass=r;
			m_curMax=m_baseMax;
			m_curRate=m_baseEmissionRate;
			m.rigidbody.velocity=Mathf.Sqrt(m_mass.rigidbody.mass/(r*r*r*r))*new Vector3(0f,-1f,0f);
//			Debug.Log(m.m_vel);
		}
	}
	private void unbalance(){
		Debug.Log("fall");
		Rigidbody r=GravityManager.GetRandomMass().rigidbody;
		r.velocity=r.velocity*.9f;
	}
	public void OnCollisionEnter(Collision other){
		HighScores.SetHighScore(m_score);
		Application.LoadLevel("HighScores");
	}
}
