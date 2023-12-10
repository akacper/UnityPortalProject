using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGun : MonoBehaviour
{
    public int portalShots = 0;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject A;
    [SerializeField] private GameObject B;
    [SerializeField] private string hitTagName = "PortalWall";

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot(A);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            Shoot(B);
        }
    }

    public void Shoot(GameObject portal)
    {
        RaycastHit rayHit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out rayHit))
        {
            string hitTag = rayHit.transform.gameObject.tag;
            if (hitTag != hitTagName)
            {
                return;
            }
            Vector3 hitPos = rayHit.point;
            GameObject hit = rayHit.transform.gameObject;
            portal.transform.position = hitPos;
            portal.transform.rotation = hit.transform.rotation;
            portalShots++;
        } 
    }

    public int GetPlacements()
    {
        return portalShots;
    }
}
