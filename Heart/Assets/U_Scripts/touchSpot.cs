using UnityEngine;
using System.Collections;

public class touchSpot : MonoBehaviour {
    public float m_life = 6;
    public ParticleSystem m_particles;
    public float m_score{
        set{
            m_particles.startColor = new Color(1f, value, value);
        }
    
    }

    public void Update(){
        m_life = m_life - Time.deltaTime;
        if (m_life < 0)
            Destroy(gameObject);
    }
}
