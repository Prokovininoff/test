using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private MeshCollider gateCollider;
    private MeshRenderer gateRenderer;
    
    public bool gateDisabled;
    public Transform playerBody;

    public float maxDistance = 5;

    public void gateDisable ()
    {
        gateCollider.enabled = false;
        gateRenderer.enabled = false;
    }

    public void gateEnable ()
    {
        gateCollider.enabled = true;
        gateRenderer.enabled = true;
    }

    private void Awake()
    {
        gateCollider = gameObject.GetComponent<MeshCollider>();
        gateRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (gateDisabled == true)
        {
            gateDisable();
        } else
        {
            gateEnable();
        }
    }
}
