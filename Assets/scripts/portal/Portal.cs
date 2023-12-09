using UnityEditor.UIElements;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private GameObject connectedPortal;
    private Vector3 connectedPortalPos;
    private Vector3 velocity;
    private CharacterController controller;
    private GameObject liftable;

    // public string wallTag = "PortalWall";

    private void Start()
    {
        connectedPortal = FindPortal();
    }

    private void OnTriggerEnter(Collider collision)
    {
        connectedPortal = FindPortal();
        if (connectedPortal != null)
        {
            connectedPortalPos = connectedPortal.transform.Find("Destination").position;
            controller = collision.GetComponent<CharacterController>();
            if (controller == null)
            {
                if (collision.transform.gameObject.CompareTag("Liftable"))
                {
                    collision.transform.position = connectedPortalPos;
                }
                return;
            }
            // velocity = collision.GetComponent<Rigidbody>().velocity;
            controller.enabled = false;
            controller.transform.position = connectedPortalPos;
            controller.transform.LookAt(connectedPortal.transform.Find("LookAt"));
            controller.enabled = true;
        }
    }

    private GameObject FindPortal()
    {
        GameObject portal;
        if (gameObject.name == "PortalA")
        {
            portal = GameObject.Find("PortalB");
        }
        else
        {
            portal = GameObject.Find("PortalA");
        }
        return portal;
    }
}