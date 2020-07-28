using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GateSwitch : MonoBehaviour
{
    public bool isAimingGate;

    private Gate gate;
    private Camera playerCam;
    private TextMeshProUGUI textMesh;
    private bool inRange;

    private void Start()
    {
        playerCam = GetComponentInChildren<Camera>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            if (hit.collider.gameObject.tag == "Gate")
            {
                isAimingGate = true;
            } else
            {
                isAimingGate = false;
            }
        }

        // If aming at an gate
        if (isAimingGate && (hit.collider != null))
        {
            gate = hit.collider.gameObject.GetComponentInChildren<Gate>();
            // If within maxDistance
            if (hit.distance <= gate.maxDistance)
            {
                if (gate.gateDisabled == false)
                {
                    textMesh.text = "press \"e\" to open door";

                } else
                {
                    textMesh.text = "press \"e\" to close door";

                }

                if (Input.GetKeyDown("e"))
                {
                    gate.gateDisabled = !gate.gateDisabled;

                }

            } else
            {
                textMesh.text = null;

            }
        } 
        // Not aiming at gate
        else
        {
            textMesh.text = null;
        }
    }
}
