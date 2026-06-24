using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform Camera;
    [SerializeField] private float forcaEmpurrao = 8f;
    [SerializeField] private float jumpPower = 0.7f;
    [SerializeField] private float rotationSpeed = 10f;


    [Header("Cold Penalty")]
    [SerializeField] private float minimumMovementMultiplier = 0.5f;
    [SerializeField] private float minimumJumpMultiplier = 0.5f;

    private float movementSpeed = 5f;
    private Vector3 movimento;
    private float xRotation = 0f;
    private float minX = -10f;
    private float maxX = 10f;
    private bool walking = false;

    public Animator playerAnim;
    private Rigidbody playerRb;

    private bool canMove = true;

    private bool canJump = true;

    private PlayerTemperatureController temperatureController;






    [SerializeField] private GameObject Secondbody;
    [SerializeField] private GameObject Mainbody;



    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        temperatureController = GetComponent<PlayerTemperatureController>();
    }

    void Update()
    {
        HandleRotation();
        HandleAnimations();
        HandleCameraInput();
        HandleBlueVision();
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

        if (!canMove) return; // this will stop movement

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

        float baseSpeed = 5f;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            baseSpeed = 10f;
        }

        float movementMultiplier = GetMovementTemperatureMultiplier();

        movementSpeed = baseSpeed * movementMultiplier;

        transform.position += movimento.normalized * movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && canJump)
            StartCoroutine(jump());
    }

    float GetMovementTemperatureMultiplier()
    {
        if (temperatureController == null)
        {
            return 1f;
        }

        float temperaturePercentage = temperatureController.TemperaturePercentage;

        if (temperaturePercentage > 50f)
        {
            return 1f;
        }

        float factor = temperaturePercentage / 50f;

        return Mathf.Lerp(minimumMovementMultiplier, 1f, factor);
    }

    float GetJumpTemperatureMultiplier()
    {
        if (temperatureController == null)
        {
            return 1f;
        }

        float temperaturePercentage = temperatureController.TemperaturePercentage;

        if (temperaturePercentage > 50f)
        {
            return 1f;
        }

        float factor = temperaturePercentage / 50f;

        return Mathf.Lerp(minimumJumpMultiplier, 1f, factor);
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
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {

            //StartCoroutine(stopjumpanimation());
            //playerAnim.ResetTrigger("idle");
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




    ///---------------------------------BLUEVISION
    void HandleBlueVision()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            canMove = false; //stop Player Movement
            playerAnim.SetTrigger("idle"); //set the animation 
            StartCoroutine(waitzerofive());
        }
    }


    //PHASE - 1
    private IEnumerator waitzerofive()
    {
        yield return new WaitForSeconds(0.3f);
        Mainbody.SetActive(!Mainbody.activeSelf);
        Secondbody.SetActive(!Secondbody.activeSelf);
        StartCoroutine(wait());
    }


    //PHASE - 2
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("blue");
        Mainbody.SetActive(!Mainbody.activeSelf);
        Secondbody.SetActive(!Secondbody.activeSelf);
        canMove = true;
    }



    //---------------------------JUMP
    // private IEnumerator jump()
    // {
    //     yield return new WaitForSeconds(0.6f);
    //     playerRb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);

    //     //sfloat jumpMultiplier = GetJumpTemperatureMultiplier();

    //     // playerRb.AddForce(Vector3.up * jumpPower * jumpMultiplier, ForceMode.Impulse);
    // }


    // private IEnumerator stopjumpanimation()
    // {
    //     playerAnim.SetTrigger("idle");


    //     // Wait one frame for animator to process the trigger
    //     yield return null;
    //     yield return new WaitUntil(() =>
    //     playerAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"));

    //     playerAnim.SetTrigger("jump");

    //     // Wait until we're actually IN the jump state
    //     yield return new WaitUntil(() =>
    //         playerAnim.GetCurrentAnimatorStateInfo(0).IsName("jump"));


    //     yield return new WaitForSeconds(0.6f);
    //     playerAnim.speed = 0.2f; // pause animation use value 0 i am going to slow it down
    //     yield return new WaitForSeconds(1.10f);
    //     playerAnim.speed = 1; // playback
    // }


    private IEnumerator jump()
    {
        canJump = false; // block immediately

        // Trigger animation first
        playerAnim.SetTrigger("idle");
        yield return null;
        yield return new WaitUntil(() =>
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("idle"));

        playerAnim.SetTrigger("jump");
        yield return new WaitUntil(() =>
            playerAnim.GetCurrentAnimatorStateInfo(0).IsName("jump"));

        yield return new WaitForSeconds(0.4f);
        playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        //sfloat jumpMultiplier = GetJumpTemperatureMultiplier();

        // playerRb.AddForce(Vector3.up * jumpPower * jumpMultiplier, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        playerAnim.speed = 0.2f;
        yield return new WaitForSeconds(1.10f);
        playerAnim.speed = 1f;

        canJump = true; // re-enable only after fully done
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