// Stwórz nową scenę i zbuduj w niej testowy poziom wykorzystując ProBuilder.
// Stwórz podejścia o różnym kącie nachylenia, schody i ściany. Dodaj dowolny
// model postaci (może to być dość prosta bryła) i wykorzystaj przykładową
// implementację ruchu z wykorzystaniem CharacterController z dokumentacji Unity.
// Przetestuj poziom (aktualnie ustawiając kamerę tak, żeby obejmowała cały poziom) i ewentualnie dostosuj parametry komponentu jeżeli nie można pokonać niektórych przeszkód (wzniesienia, schody).
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveWithCharacterController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float groundCheckDelay = 0.1f;
    private float lastTimeGrounded;
    [SerializeField] private Finish finishScript;
    [SerializeField] private PortalGun portalGunScript;
    [SerializeField] private Portal portalAScript;
    [SerializeField] private Portal portalBScript;
    private AudioSource audioSource;
    [SerializeField] private AudioClip walkSound;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        finishScript = FindObjectOfType<Finish>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = walkSound;
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        groundedPlayer = IsGrounded();
        if (groundedPlayer)
        { 
            // Update last time player was grounded
            lastTimeGrounded = Time.time;
            playerVelocity.y = 0f;
        }
        else if (Time.time - lastTimeGrounded < groundCheckDelay)
        {
            // Consider grounded short time after leaving the ground
            groundedPlayer = true;
        }
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Play walking sound if the character is moving
        if (moveDirection.magnitude > 0.1f && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (moveDirection.magnitude < 0.1f && audioSource.isPlaying)
        {
            // Stop the walking sound if the character is not moving
            audioSource.Stop();
        }
        if (!groundedPlayer)
        {
            audioSource.Stop();
        }
    }
    bool IsGrounded()
    {
        return controller.isGrounded;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Finish"))
        {
            int placed = portalGunScript.GetPlacements();
            int passed = portalAScript.GetPasses() + portalBScript.GetPasses();
            audioSource.Stop();
            finishScript.ShowUI(placed, passed);
        }
    }
}
