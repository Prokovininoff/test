using System.Collections.Generic;
using UnityEngine;

public class portalTraveller : MonoBehaviour
{
    public Vector3 previousOffsetFromPortal { get; set; }

    public virtual void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }


    // Called once no longer touching portal (excluding when teleporting)
    public virtual void ExitPortalThreshold()
    {
        // Disable slicing
        
    }
    public virtual void EnterPortalThreshold()
    {

    }
}