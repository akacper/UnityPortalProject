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
    private AudioSource audioSource;
    [SerializeField] private AudioClip portalSound1;
    [SerializeField] private AudioClip portalSound2;
    [SerializeField] private AudioClip portalNotPlacedSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            PlaySound(portalSound1);
            Shoot(A);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            PlaySound(portalSound2);
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
                PlaySound(portalNotPlacedSound);
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

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
