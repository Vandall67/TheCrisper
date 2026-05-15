using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidade = 4f;

    private Rigidbody rb;
    private Vector3 movimento;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movimento = new Vector3(horizontal, 0f, vertical).normalized;
    }

    private void FixedUpdate()
    {
        Vector3 novaPosicao = rb.position + movimento * velocidade * Time.fixedDeltaTime;
        rb.MovePosition(novaPosicao);
    }
}