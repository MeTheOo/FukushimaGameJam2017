using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sticker : MonoBehaviour {
    public GameObject TargetObj;
    public Vector3 PickupPoint;
    public float Range;
    public UnityEvent On_StickStart;
    public UnityEvent On_StickUpdate;
    public UnityEvent On_StickExit;
    
    protected bool condition = true;
    private Transform origin_parent;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public float debug_range;
    private bool is_stuck;
	public virtual void Update () {
        if (!is_stuck)
        {
            debug_range = Vector3.Distance(TargetObj.transform.position, transform.position);
            if (Vector3.Distance(TargetObj.transform.position, transform.position) < Range)
                if (condition)
                    Stick();
        }
        else
            Sticking();

    }
    public virtual void Stick()
    {
        On_StickStart.Invoke();
        is_stuck = true;
        origin_parent = TargetObj.transform.parent;
        TargetObj.transform.SetParent(transform);
    }
    void Sticking()
    {
        TargetObj.transform.localPosition = PickupPoint;
        On_StickUpdate.Invoke();
    }
    public virtual void StickExit()
    {
        is_stuck = false;
        On_StickExit.Invoke();
        TargetObj.transform.parent = origin_parent;
    }
}
