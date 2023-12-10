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

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        finishScript = FindObjectOfType<Finish>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        groundedPlayer = controller.isGrounded;
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
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    bool IsGrounded()
    {
        return controller.isGrounded;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Wykryto dotkniecie przeszkody (skrypt gracza)");
        }
        else if (collision.CompareTag("Finish"))
        {
            int placed = portalGunScript.GetPlacements();
            int passed = portalAScript.GetPasses() + portalBScript.GetPasses();
            finishScript.ShowUI(placed, passed);
        }
    }
}
