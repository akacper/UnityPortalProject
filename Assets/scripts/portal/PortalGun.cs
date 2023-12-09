using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject A;
    [SerializeField] private GameObject B;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootA();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            ShootB();
        }
    }
    public void ShootA()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit))
        {
            string hitTag = rayHit.transform.gameObject.tag;
            if (hitTag != "PortalWall")
            {
                return;
            }
            Vector3 hitPos = rayHit.point;
            A.transform.position = hitPos;
            GameObject hit = rayHit.transform.gameObject;
            A.transform.rotation = hit.transform.rotation;
        }
    }

    public void ShootB()
    {
        RaycastHit rayHit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit))
        {
            Vector3 hitPos = rayHit.point;
            B.transform.position = hitPos;
            GameObject hit = rayHit.transform.gameObject;
            B.transform.rotation = hit.transform.rotation;
        } 
    }
}
