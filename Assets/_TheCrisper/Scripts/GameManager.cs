using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int fragmentosRecolhidos = 0;
    public int totalFragmentos = 4;

    [SerializeField] private string nomeCenaSeguinte;

    public void RecolherFragmento()
    {
        fragmentosRecolhidos++;

        Debug.Log("Fragmentos recolhidos: " + fragmentosRecolhidos + "/" + totalFragmentos);

        if (fragmentosRecolhidos >= totalFragmentos)
        {
            Debug.Log("Mapa completo!");

            if (!string.IsNullOrEmpty(nomeCenaSeguinte))
            {
                SceneManager.LoadScene(nomeCenaSeguinte);
            }
        }
    }
}