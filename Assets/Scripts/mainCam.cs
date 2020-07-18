using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCam : MonoBehaviour
{
    Portal[] portals;

    void Awake()
    {
        portals = FindObjectsOfType<Portal>();
    }

    private void OnPreCull()
    {
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].Render();
        }
    }
}
