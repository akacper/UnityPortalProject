using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject exit;
    private AudioSource audioSource;
    [SerializeField] private Material activeMat;
    [SerializeField] private Material inactiveMat;
    private Renderer render;

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        render = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Liftable"))
        {
            PlaySound(openSound);
            render.material = activeMat;
            collision.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0,1,2,1));
            exit.transform.Find("Door_Left").gameObject.SetActive(false);
            exit.transform.Find("Door_Right").gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Liftable") || collision.CompareTag("Lifted"))
        {
            PlaySound(closeSound);
            render.material = inactiveMat;
            collision.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            exit.transform.Find("Door_Left").gameObject.SetActive(true);
            exit.transform.Find("Door_Right").gameObject.SetActive(true);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
