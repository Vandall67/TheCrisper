using UnityEngine;

public class FragmentoMapa : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.RecolherFragmento();
            gameObject.SetActive(false);
        }
    }
}