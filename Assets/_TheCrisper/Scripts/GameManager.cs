using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int fragmentosRecolhidos = 0;
    public int totalFragmentos = 3;

    public void RecolherFragmento()
    {
        fragmentosRecolhidos++;

        Debug.Log("Fragmentos recolhidos: " + fragmentosRecolhidos + "/" + totalFragmentos);

        if (fragmentosRecolhidos >= totalFragmentos)
        {
            Debug.Log("Mapa completo!");
        }
    }
}