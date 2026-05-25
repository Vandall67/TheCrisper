using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidade = 4f;
    [SerializeField] private float forcaEmpurrao = 8f;

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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Empurravel"))
        {
            Rigidbody rbObjeto = collision.rigidbody;

            if (rbObjeto != null && !rbObjeto.isKinematic)
            {
                Vector3 direcaoEmpurrao = new Vector3(movimento.x, 0f, movimento.z);

                if (direcaoEmpurrao.magnitude > 0.1f)
                {
                    rbObjeto.AddForce(direcaoEmpurrao.normalized * forcaEmpurrao, ForceMode.Force);
                }
            }
        }
    }
}