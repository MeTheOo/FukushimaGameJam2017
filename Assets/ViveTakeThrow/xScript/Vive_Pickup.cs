using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Vive_Pickup : MonoBehaviour
{
    public SteamVR_TrackedController LeftHand;
    public SteamVR_TrackedController RightHand;
    public enum PickupType
    {
        OnHover,
        OnTrigger,
        OnTriggerContinue
    }
    public PickupType PickupSelection;
    public Vector3 PickupPoint;
    [Header("Pickup Event")]
    public UnityEvent On_Pickup;
    public UnityEvent On_Picking;
    public UnityEvent On_Drop;


    private SteamVR_TrackedController _currenthand;
    private SteamVR_TrackedController _lasthand;
    private SteamVR_TrackedController _targethand;
    private bool is_pickup;
    private bool has_rigidbody;
    private Rigidbody _rigidbody;
    private Transform origin_parent;


    public SteamVR_TrackedController Get_Controller()
    {
        return _currenthand;
    }
    // Use this for initialization
    public virtual void  Start()
    {
        _lasthand = _currenthand = null;
        has_rigidbody = GetComponent<Rigidbody>();
        if (has_rigidbody)
            _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (_currenthand == null)
            return;
        

        if (_lasthand != _currenthand)
        {
            switch (PickupSelection)
            {
                case PickupType.OnHover:
                    PickUp_Start();
                    break;
                case PickupType.OnTrigger:
                    if (_currenthand.triggerPressed)
                        PickUp_Start();
                    break;
                case PickupType.OnTriggerContinue:
                    if (_currenthand.triggerPressed)
                        PickUp_Start();
                    break;
            }
        }
        if (is_pickup)
        {
            switch (PickupSelection)
            {
                case PickupType.OnHover:
                    Pickup_Update();
                    break;
                case PickupType.OnTrigger:
                    Pickup_Update();
                    break;
                case PickupType.OnTriggerContinue:
                    if (_targethand.triggerPressed)
                        Pickup_Update();
                    else
                        PickUp_Exit();
                    break;
            }
        }
    }
    public virtual void PickUp_Start()
    {
        On_Pickup.Invoke();
        is_pickup = true;
        _targethand = _currenthand;
        _lasthand = _currenthand;
        origin_parent = transform.parent;
        if (has_rigidbody)
            _rigidbody.useGravity = false;
    }
    public virtual void Pickup_Update()
    {
        On_Picking.Invoke();
        transform.SetParent(_currenthand.transform);
        transform.localPosition = PickupPoint;
    }
    public virtual void PickUp_Exit()
    {
        if (has_rigidbody)
        {
            _rigidbody.useGravity = true;
            SwipeValue(_rigidbody);
        }
        On_Drop.Invoke();
        is_pickup = false;
        _targethand = null;
        transform.SetParent(origin_parent);
        _lasthand = _currenthand = null;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == LeftHand.gameObject || other.gameObject == RightHand.gameObject)
            _currenthand = other.GetComponent<SteamVR_TrackedController>();
    }
    void OnTriggerStay(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == LeftHand.gameObject || other.gameObject == RightHand.gameObject)
            _currenthand = null;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == LeftHand.gameObject || other.gameObject == RightHand.gameObject)
            _currenthand = other.gameObject.GetComponent<SteamVR_TrackedController>();
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == LeftHand.gameObject || other.gameObject == RightHand.gameObject)
            _currenthand = null;
    }

    public void SwipeValue(Rigidbody target_rigid)
    {
        float f_velocity_angular = 0.1f;
        SteamVR_TrackedObject trackedObj = _targethand.GetComponent<SteamVR_TrackedObject>();
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
}
