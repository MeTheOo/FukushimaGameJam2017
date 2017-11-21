using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testaudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPlay()
    {
        SoundManager.Instance.PlaySound(SoundManager.SoundType.SE_BALL_SPONE);
    }
}
