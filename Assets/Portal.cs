using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Camera playerCam;
    public Portal linkedPortal;
    public MeshRenderer screen;
    public Camera portalCam;
    private RenderTexture viewTexture;
    public List<portalTraveller> trackedTravellers;

    private void Awake()
    {
        portalCam.enabled = false;
        trackedTravellers = new List<portalTraveller>();
    }

    private void LateUpdate()
    {
        for (int i = 0; i < trackedTravellers.Count; i++)
        {
            portalTraveller traveller = trackedTravellers[i];
            Transform travellerT = traveller.transform;

            Vector3 offsetFromPortal = travellerT.position - transform.position;
            int portalSide = System.Math.Sign(Vector3.Dot(offsetFromPortal, transform.forward));
            int portalSideOld = System.Math.Sign(Vector3.Dot(traveller.previousOffsetFromPortal, transform.forward));
                if (portalSide != portalSideOld)
                {
                    var m = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * travellerT.localToWorldMatrix;
                    traveller.Teleport(transform, linkedPortal.transform, m.GetColumn(3), m.rotation);
                }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(trackedTravellers);
        }
    }


    void createViewTexture()
    {
        if (viewTexture == null || viewTexture.height != Screen.height || viewTexture.width != Screen.width)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }

            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            portalCam.targetTexture = viewTexture;
            linkedPortal.screen.material.SetTexture("_MainTex", viewTexture);
        }
    }

    static bool VisibleFromCam(Renderer renderer, Camera camera)
    {
        Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(frustumPlanes, renderer.bounds);
    }

    public void Render()
    {
        if (!VisibleFromCam(linkedPortal.screen, playerCam))
        {
            var testTexture = new Texture2D(1, 1);
            testTexture.SetPixel(0, 0, Color.red);
            testTexture.Apply();
            linkedPortal.screen.material.SetTexture("_MainTex", testTexture);
            return;
        }

        linkedPortal.screen.material.SetTexture("_MainTex", viewTexture);
        screen.enabled = false;
        createViewTexture();

        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);

        portalCam.Render();
        screen.enabled = true;
    }

    void OnTravellerEnterPortal (portalTraveller traveller)
    {
        if (!trackedTravellers.Contains (traveller))
        {
            traveller.EnterPortalThreshold();
            traveller.previousOffsetFromPortal = traveller.transform.position - transform.position;
            trackedTravellers.Add(traveller);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var traveller = other.GetComponent<portalTraveller>();
        if (traveller)
        {
            OnTravellerEnterPortal(traveller);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var traveller = other.GetComponent<portalTraveller>();
        if (traveller && trackedTravellers.Contains(traveller))
        {
            traveller.ExitPortalThreshold();
            trackedTravellers.Remove(traveller);
        }
    }

    void ProtectScreenFromClipping()
    {
        float halfHeight = playerCam.nearClipPlane * Mathf.Tan(playerCam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float halfWidth = halfHeight * playerCam.aspect;
        float dstToNearClipPlaneCorner = new Vector3(halfWidth, halfHeight, playerCam.nearClipPlane).magnitude;

        Transform screenT = screen.transform;
        bool canFacingSameDirAsPortal = Vector3.Dot(transform.forward, transform.position - playerCam.transform.position) > 0;
        screenT.localScale = new Vector3(screenT.localScale.x, screenT.localScale.y, dstToNearClipPlaneCorner);
        screenT.localPosition = Vector3.forward * dstToNearClipPlaneCorner * ((canFacingSameDirAsPortal) ? 0.5f : -0.5f);
    }
}