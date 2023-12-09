using UnityEngine;

public class LiftObject : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject hand;
    [SerializeField] private string liftTag = "Liftable";
    [SerializeField] private string liftedTag = "Lifted";
    private GameObject liftedObject;
    private Transform previousParent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(player.position, player.forward, out hit))
            {
                if (hit.collider.CompareTag(liftTag))
                {
                    PickUpObject(hit.collider.gameObject);
                }
                else if (liftedObject)
                {
                    DropObject(liftedObject);
                }
            }
        }
    }

    void PickUpObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.GetComponent<Rigidbody>().useGravity = false;
        previousParent = obj.transform.parent;
        obj.transform.SetParent(hand.transform);
        liftedObject = obj;
        obj.tag = liftedTag;
    }

    void DropObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = false;
        obj.GetComponent<Rigidbody>().useGravity = true;
        obj.transform.SetParent(previousParent);
        liftedObject = null;
        obj.tag = liftTag;
    }
}
