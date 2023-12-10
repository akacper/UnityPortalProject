using UnityEngine;
// using UnityEditor.UIElements;

public class Portal : MonoBehaviour
{
    public int portalPasses = 0;
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
        if (connectedPortal == null) {
            return;
        }
        connectedPortalPos = connectedPortal.transform.Find("Destination").position;
        controller = collision.GetComponent<CharacterController>();
        if (collision.transform.gameObject.CompareTag("Liftable"))
        {
            velocity = collision.GetComponent<Rigidbody>().velocity;
            Debug.Log(connectedPortal.transform.forward);
            velocity = new Vector3(velocity.y, 0, 0);
            if (collision.transform.forward == Vector3.zero)
            {

            }
            collision.GetComponent<Rigidbody>().useGravity = false;
            collision.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.transform.position = connectedPortalPos;
            collision.transform.LookAt(connectedPortal.transform.Find("LookAt"));
            // collision.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);

           Vector3 portalForward = connectedPortal.transform.Find("LookAt").forward;
            // Project the velocity onto the portal forward direction
            float velocityProjection = Vector3.Dot(collision.GetComponent<Rigidbody>().velocity, portalForward);
            // Set the velocity based on the direction of the portal
            collision.GetComponent<Rigidbody>().velocity = portalForward * velocityProjection;
            collision.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);

            collision.GetComponent<Rigidbody>().useGravity = true;
        }
        else if (controller)
        {
            // velocity = collision.GetComponent<Rigidbody>().velocity;
            controller.enabled = false;
            controller.transform.position = connectedPortalPos;
            controller.transform.LookAt(connectedPortal.transform.Find("LookAt"));
            controller.enabled = true;
            portalPasses++;
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

    public int GetPasses()
    {
        return portalPasses;
    }
}