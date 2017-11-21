using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vive_Sticker : Sticker {

    public override void Update()
    {
        if (TargetObj != null && TargetObj.GetComponent<Vive_Pickup>() != null && TargetObj.GetComponent<Vive_Pickup>().Get_Controller() != null)
            condition = TargetObj.GetComponent<Vive_Pickup>().Get_Controller().triggerPressed;
        base.Update();
    }
    public override void Stick()
    {
        TargetObj.GetComponent<Vive_Pickup>().enabled = false;
        TargetObj.GetComponent<Rigidbody>().detectCollisions = false;
        base.Stick();
    }
    public override void StickExit()
    {
        TargetObj.GetComponent<Vive_Pickup>().enabled = true;
        TargetObj.GetComponent<Rigidbody>().detectCollisions = true;
        base.StickExit();
    }
}
