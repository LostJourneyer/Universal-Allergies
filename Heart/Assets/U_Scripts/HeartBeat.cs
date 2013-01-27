using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour {
	public GameObject[] Planets;
	public Light m_light;
	public float m_lightScale;
	public float m_particalMultiplyer=10;
	public float m_colorMultiplyer;
	public float m_tolerance;
	public TextMesh m_ScoreDisplay;
	public GameObject m_newMass;
	public int m_baseEmissionRate=10;
	public int m_baseMax=1000;
	public int m_absMax=1000000;
	public int m_maxRate=1000;
	public int m_hitsToSpawn=3;
	public static int sm_planetBonus;
	public static int sm_score;
	public MultiplierScript m_multipler;
	public GameObject m_multiplierObj;
	
	private static HeartBeat m_hb;
	private float m_max=0.06877659f;		//max spectrum value
	private AudioSource m_AS;
	private ParticleSystem m_PS;
	private int m_curMax;
	private int m_curRate;
	private int m_curHits=0;
	private Mass m_mass;
	private bool m_up;
	private bool m_down;
	private bool m_caught;
	
	public void Start()
	{	
		m_hb=this;
		m_AS=(AudioSource)gameObject.GetComponent<AudioSource>();
		m_PS=(ParticleSystem)gameObject.GetComponent<ParticleSystem>();
		m_mass=(Mass)gameObject.GetComponent<Mass>();
		m_curMax=m_baseMax;
		m_curRate=m_baseEmissionRate;
		sm_planetBonus=0;
		sm_score=0;
		spawnPlanet();
	}
	public void Update()
	{
        float[] spectrum = m_AS.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);
		float normalizedSpect=Mathf.Min(1f, spectrum[0]/m_max);
		m_light.range=10+(m_lightScale*normalizedSpect);
        m_PS.startSpeed =(normalizedSpect)*m_particalMultiplyer;
		particleSystem.startColor=new Color(Mathf.Max(normalizedSpect,.9f),Mathf.Max(spectrum[1]/m_max,.7f),spectrum[2]/m_max,1f);			
		if(Input.GetKeyDown(KeyCode.Space)){
			m_caught=true;
			if(normalizedSpect>m_tolerance){
				balance();
			}else{
				unbalance(.99f);
			}
		}else if(m_up){
			if(normalizedSpect<m_tolerance){
				if(!m_caught){
					Debug.Log("damage");
					unbalance(.6f);
				}
				m_up=false;
			}
		}else if(normalizedSpect>m_tolerance){
			m_up=true;
			m_caught=false;
		}

		sm_score=sm_score+(int)(Time.deltaTime*100);
		m_ScoreDisplay.text="Score:"+sm_score;
	}
	public void OnCollisionEnter(Collision other){
		HighScores.SetHighScore(sm_score);
		Application.LoadLevel("HighScores");
	}
	
	private void balance(){
		sm_score++;
		m_curHits++;
		if((m_curMax<m_absMax)||(m_curRate<m_maxRate)){
			m_curMax=m_curMax*10;
			m_curRate=m_curRate*10;
		}
		if(m_curHits>m_hitsToSpawn){
			spawnPlanet();
			GravityManager.PowerPlanets();
		}
	}
	public static void spawnPlanet(){
		m_hb.m_curHits=0;
		sm_score=sm_score+10*sm_planetBonus;
		sm_planetBonus=Mathf.Min(sm_planetBonus+1, 10);
		if(sm_planetBonus>1){
			m_hb.m_multiplierObj.SetActive(true);
			m_hb.m_multipler.Show(sm_planetBonus-2);
		}
		float r=Random.Range(5, 10);
		Vector3 newLoc=new Vector3(r,0f,0f);
		GameObject newMass=(GameObject)Instantiate(m_hb.Planets[Random.Range(0,5)],newLoc,Quaternion.identity);
		Mass m=newMass.GetComponent<Mass>();
		m.rigidbody.mass=r;
		m_hb.m_curMax=m_hb.m_baseMax;
		m_hb.m_curRate=m_hb.m_baseEmissionRate;
		m.rigidbody.velocity=Mathf.Sqrt(m_hb.m_mass.rigidbody.mass/(r*r*r*r))*new Vector3(0f,-1f,0f);
	}
	private void unbalance(float damage){
		Debug.Log("fall");
		Rigidbody r=GravityManager.GetRandomPlanet().rigidbody;
		r.velocity=r.velocity*damage;
	}
}