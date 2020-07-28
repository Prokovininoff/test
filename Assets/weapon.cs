using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Camera playerCam;
    public Transform muzzle;
    public Transform playerBody;
    public GameObject hitImpact;

    public float range;
    public float fireRate;
    public float hitDamage;
    public float impactForce;

    private playerView playerView;
    private float nextTimeFire;

    private void Awake()
    {
        playerView = GetComponentInParent<playerView>();
        playerBody = playerView.playerBody;
        nextTimeFire = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeFire)
        {
            nextTimeFire = Time.time + 1f/fireRate;
            Fire();
        }
    }

    void Recoil()
    {
        playerView.xRotation -= Mathf.Lerp(0f, Random.Range(10f, 20f), 0.1f);
        playerBody.Rotate(Vector3.up * Mathf.Lerp(0f, Random.Range(-0.7f, 0.5f), 0.6f));
    }

    void Fire()
    {
        Recoil();

        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, range))
        {

            //Debug.Log(hit.collider.gameObject.name);
            GameObject impactEffectGo = Instantiate(hitImpact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactEffectGo, 2f);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            if (hit.collider.gameObject.GetComponent<hitTarget>() != null)
            {
                
                hitTarget hitTarget = hit.collider.gameObject.GetComponent<hitTarget>();
                hitTarget.health -= hitDamage;
            }
        }
    }
}
