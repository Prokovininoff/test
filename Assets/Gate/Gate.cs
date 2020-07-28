using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private MeshCollider gateCollider;
    private MeshRenderer gateRenderer;
    private Animator gateAnimation;
    
    public bool gateDisabled;
    public Transform playerBody;

    public float maxDistance = 5;

    private void Awake()
    {
        gateCollider = gameObject.GetComponentInChildren<MeshCollider>();
        gateRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        gateAnimation = gameObject.GetComponentInChildren<Animator>();
    }

    public void gateDisable ()
    {
        //gateCollider.enabled = false;
        //gateRenderer.enabled = false;
        gateAnimation.SetBool("gateDisabled", true);
    }

    public void gateEnable ()
    {
        //gateCollider.enabled = true;
        //gateRenderer.enabled = true;
        gateAnimation.SetBool("gateDisabled", false);
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
