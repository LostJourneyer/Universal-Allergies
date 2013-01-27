using UnityEngine;
using System.Collections;

public class MultiplierScript : MonoBehaviour {
	public Texture2D[] m_multipliers;
	public float m_life;
	public static int m_amount;

	// Update is called once per frame
	void Update () {
		if(m_life<0){
			gameObject.SetActive(false);
		}else{
			m_life=m_life-Time.deltaTime;
		}
	}
	public void Show(int multi){
		gameObject.renderer.material.mainTexture=m_multipliers[m_amount];
		m_life=3;
	}
}
