using UnityEngine;
using System.Collections;

public class HeartBeat : MonoBehaviour {
	public GameObject[] Planets;
	public GameObject m_shieldGO;
	public Shield m_shieldCon;
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
    public GameObject m_touchFeedback;
	public float m_ForceSpawn=5f;           //used to time tutorial
	
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
	}
	public void Update()
	{
        float[] spectrum = m_AS.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);
		float normalizedSpect=Mathf.Min(1f, spectrum[0]/m_max);
		m_light.range=10+(m_lightScale*normalizedSpect);
        m_PS.startSpeed =(normalizedSpect)*m_particalMultiplyer;
		particleSystem.startColor=new Color(Mathf.Max(normalizedSpect,.9f),Mathf.Max(spectrum[1]/m_max,.7f),spectrum[2]/m_max,1f);
		if((m_ForceSpawn<0)&&(GravityManager.GM.m_planets.Count<1)){
			spawnPlanet(new Vector3(0,20,0));
		}else{
			m_ForceSpawn=m_ForceSpawn-Time.deltaTime;
		}
		if((Input.touches.Length>0)||Input.GetMouseButtonDown(0)){
			m_caught=true;
            Ray ray;
            if (Input.touches.Length > 0){
                ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            }else{
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            Vector3 DisplayLoc=ray.GetPoint(10f);
            GameObject tDisplay=(GameObject)Instantiate(m_touchFeedback, DisplayLoc, Quaternion.identity);
            touchSpot ts = (touchSpot)tDisplay.GetComponent<touchSpot>();
            ts.m_score = normalizedSpect;
			
			if(normalizedSpect>m_tolerance){
				balance(ts.transform.position);
			}else{
				unbalance(.9f);
			}
		}else if(m_up){
			if(normalizedSpect<m_tolerance){
				if(!m_caught){
					Debug.Log("damage");
					unbalance(.8f);
				}
				m_up=false;
			}
		}else if(normalizedSpect>m_tolerance){
			m_up=true;
			m_caught=false;
		}
//		sm_score=sm_score+(int)(Time.deltaTime);
		m_ScoreDisplay.text=""+sm_score;
		if(rigidbody.velocity.magnitude!=0){
			rigidbody.velocity=new Vector3();
			transform.position=new Vector3();
		}
	}
	public void OnCollisionEnter(Collision other){
		if(other.gameObject.tag=="Mass"){
			Debug.Log(m_shieldGO.activeSelf);
			HighScores.SetHighScore(sm_score);
			Application.LoadLevel("DisplayScore");
		}
	}
	
	private void balance(Vector3 spawnLoc){
		sm_score++;
		m_curHits++;
		Debug.Log(m_curHits+","+m_hitsToSpawn);
		if((m_curMax<m_absMax)||(m_curRate<m_maxRate)){
			m_curMax=m_curMax*10;
			m_curRate=m_curRate*10;
		}
		if(m_curHits>m_hitsToSpawn){
			spawnPlanet(spawnLoc);
			GravityManager.PowerPlanets();
		}
		if(sm_planetBonus>5)
		{
			m_hb.m_shieldGO.SetActive(true);
			m_hb.m_shieldCon.PowerOn(sm_planetBonus);
		}else if(sm_planetBonus>2){
			GravityManager.antiGravitBurst();
		}
	}
	public static void spawnPlanet(Vector3 loc){
		m_hb.m_curHits=0;
		sm_score=sm_score+10*sm_planetBonus;
		sm_planetBonus=Mathf.Min(sm_planetBonus+1, 10);
		if(sm_planetBonus>1){
			m_hb.m_multiplierObj.SetActive(true);
			m_hb.m_multipler.Show(sm_planetBonus-2);
		}
		float r=Random.Range(5, 10);
		GameObject newMass=(GameObject)Instantiate(m_hb.Planets[Random.Range(0,5)],loc,Quaternion.identity);
		Mass m=newMass.GetComponent<Mass>();
		m.rigidbody.mass=r;
		m_hb.m_curMax=m_hb.m_baseMax;
		m_hb.m_curRate=m_hb.m_baseEmissionRate;
		m.rigidbody.velocity=Mathf.Sqrt(m_hb.m_mass.rigidbody.mass*m.rigidbody.mass*Mathf.PI/(r*r))*(new Vector3(loc.y, 0, loc.x*-1).normalized);
	}
	private void unbalance(float damage){
		if(GravityManager.GM.m_planets.Count>0){
			if(GravityManager.GetRandomPlanet()!=null){
				Rigidbody r=GravityManager.GetRandomPlanet().rigidbody;
				r.velocity=r.velocity*damage;
			}
		}
		sm_planetBonus=1;
	}
}