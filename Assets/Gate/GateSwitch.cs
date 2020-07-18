using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{
    private Gate gate;
    private Camera playerCam;

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, 100f))
        {
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            
        }
    }
}
