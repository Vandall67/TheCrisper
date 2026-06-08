using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform Camera;
    [SerializeField] private float forcaEmpurrao = 8f;
    [SerializeField] private float jumpPower;
    [SerializeField] private float rotationSpeed = 10f; // Slerp-friendly value

    private float movementSpeed = 5f;
    private Vector3 movimento;
    private float xRotation = 0f;
    private float minX = -10f;
    private float maxX = 10f;
    private bool walking = false;

    public Animator playerAnim;
    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleRotation();
        HandleAnimations();
        HandleCameraInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleRotation()
    {
        Vector3 lookDirection = Camera.transform.forward;
        lookDirection.y = 0f;
        lookDirection.Normalize();

        if (lookDirection == Vector3.zero) return;

        float angle = Vector3.Angle(transform.forward, lookDirection);
        bool isFacingCamera = angle < 5f;

        // Rotate toward camera direction only when moving forward
        if (!isFacingCamera && Input.GetKey(KeyCode.W))
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleMovement()
    {
        movimento = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movimento += transform.TransformDirection(Vector3.forward);
        if (Input.GetKey(KeyCode.S))
            movimento += transform.TransformDirection(Vector3.back);
        if (Input.GetKey(KeyCode.D))
            movimento += transform.TransformDirection(Vector3.right);
        if (Input.GetKey(KeyCode.A))
            movimento += transform.TransformDirection(Vector3.left);

        // Sprint
        movementSpeed = (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) ? 10f : 5f;

        transform.position += movimento.normalized * movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
            StartCoroutine(jump());
    }

    void HandleAnimations()
    {
        // Forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnim.SetTrigger("jog");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("jog");
            playerAnim.SetTrigger("idle");
            walking = false;
        }

        // Back
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("jogback");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("jogback");
            playerAnim.SetTrigger("idle");
        }

        // Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerAnim.SetTrigger("jogleft");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.ResetTrigger("jogleft");
            playerAnim.SetTrigger("idle");
        }

        // Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerAnim.SetTrigger("jogright");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.ResetTrigger("jogright");
            playerAnim.SetTrigger("idle");
        }

        // Jump
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnim.SetTrigger("jump");
            playerAnim.ResetTrigger("idle");
        }

        // Sprint animation
        if (walking && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("jog");
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerAnim.SetTrigger("jog");
                playerAnim.ResetTrigger("run");
            }
        }
    }

    void HandleCameraInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * 500f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * 500f * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);
    }

    private IEnumerator jump()
    {
        yield return new WaitForSeconds(0.6f);
        playerRb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Empurravel"))
        {
            Rigidbody rbObjeto = collision.rigidbody;
            if (rbObjeto != null && !rbObjeto.isKinematic)
            {
                Vector3 direcaoEmpurrao = new Vector3(movimento.x, 0f, movimento.z);
                if (direcaoEmpurrao.magnitude > 0.1f)
                    rbObjeto.AddForce(direcaoEmpurrao.normalized * forcaEmpurrao, ForceMode.Force);
            }
        }
    }
}