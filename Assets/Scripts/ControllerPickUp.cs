using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerPickUp : MonoBehaviour
{
    public SteamVR_TrackedController Controller;
    public PickedUpItem PickUpItem;
    public Transform origin_parent;
    public GameObject Ball;
    bool inBusy = false;
    bool inpress = false;
    public UnityEvent OnCatch;
    // Use this for initialization
    void Start()
    {
        Controller = GetComponent<SteamVR_TrackedController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.padPressed && Controller.triggerPressed && PickUpItem == null)
        {
            
        }
        if (inBusy && PickUpItem != null)
        {
            if (!Controller.triggerPressed)
            {
                ReleaseItem();
            }
        }
    }
    void ReleaseItem()
    {
        PickUpItem.transform.SetParent(origin_parent);
        PickUpItem.GetComponent<Rigidbody>().useGravity = true;
        PickUpItem.GetComponent<Rigidbody>().isKinematic = false;
        SwipeValue(PickUpItem.GetComponent<Rigidbody>());
        PickUpItem.Picking = false;
        PickUpItem.holding = false;
        PickUpItem = null;
        inBusy = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (Controller.triggerPressed && other.GetComponent<PickedUpItem>() && PickUpItem == null)
        {
            PickedUpItem PickObj = other.GetComponent<PickedUpItem>();
            if (!PickObj.Picking)
            {
                if (PickObj.Waiting)
                {
                    PickObj.Waiting = false;
                    SpawnBall(); 
                }
                PickObj.Picking = true;
                PickObj.OnPick.Invoke();
                origin_parent = PickObj.transform.parent;
                PickObj.transform.SetParent(transform);
                PickObj.transform.localPosition = Vector3.zero;
                PickObj.GetComponent<Rigidbody>().useGravity = false;
                PickObj.GetComponent<Rigidbody>().isKinematic = true;
                PickObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                PickObj.holding = true;
                PickUpItem = PickObj;
                
                inBusy = true;
                OnCatch.Invoke();
            }
        }
    }

    public void SwipeValue(Rigidbody target_rigid)
    {
        float f_velocity_angular = 0.1f;
        SteamVR_TrackedObject trackedObj = GetComponent<SteamVR_TrackedObject>();
        SteamVR_Controller.Device device_left = SteamVR_Controller.Input((int)trackedObj.index);
        var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            target_rigid.velocity = origin.TransformVector(device_left.velocity);
            target_rigid.angularVelocity = origin.TransformVector(device_left.angularVelocity) * f_velocity_angular;
        }
        else
        {
            target_rigid.velocity = device_left.velocity;
            target_rigid.angularVelocity = device_left.angularVelocity * f_velocity_angular;
        }
        target_rigid.maxAngularVelocity = target_rigid.angularVelocity.magnitude * f_velocity_angular;
    }

    public void SpawnBall()
    {
        StartCoroutine(SpawnBallAction());
    }
    IEnumerator SpawnBallAction()
    {
        yield return new WaitForSeconds(1);
        GameObject Obj = Instantiate(Ball, GameObject.Find("BallParent").transform);
        yield break;
    }
}
