using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalCollision : MonoBehaviour
{
    public Transform linkedPortal;
    private List<GameObject> travellers;
    private List<string> travellersName;
    private Vector3 locationOffset;
    private Vector3 offSetFromPortal;

    private void Awake()
    {
        travellers = new List<GameObject>();
        travellersName = new List<string>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!travellers.Contains(other.gameObject) && other.tag == "Player")
        {
            travellers.Add(other.gameObject);
            travellersName.Add(other.name);
        }

    }


    private void displayList ()
    {
        if (travellers != null)
        {
            for (int i = 0; i < travellers.Count; i++)
            {
                Debug.Log(travellers[i]);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            displayList();
        }

        for (int i = 0; i < travellers.Count; i++)
        {
            if (travellers[i].tag == "Player")
            {
                teleport(travellers[i]);
            }

        }
    }

    public void teleport(GameObject traveller)
    {
        Debug.Log("Teleporting " + traveller.name);

        var playerMovement = traveller.GetComponent<playerMove>();
        playerMovement.enabled = false;

        Vector3 offSetFromPortal = traveller.transform.position - transform.position;
        Vector3 locationOffset = linkedPortal.position - transform.position;

        traveller.transform.SetPositionAndRotation(linkedPortal.position, traveller.transform.rotation);

        travellers.Remove(traveller);

        playerMovement.enabled = true;
    }
}
