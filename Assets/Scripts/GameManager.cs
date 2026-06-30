using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Map Fragments")]
    [SerializeField] private int fragmentosRecolhidos = 0;
    [SerializeField] private int totalFragmentos = 4;

    [Header("HUD")]
    [SerializeField] private HUDController hudController;

    [Header("Optional Automatic Scene Transition")]
    [SerializeField] private bool loadSceneWhenMapComplete = false;
    [SerializeField] private string nomeCenaSeguinte;

    [Header("Optional For Level 2")]
    [SerializeField] private bool showmapcutscene = false;
    [SerializeField] private GameObject cutscenescreanhud;

    public int FragmentosRecolhidos => fragmentosRecolhidos;
    public int TotalFragmentos => totalFragmentos;
    public bool MapaCompleto => fragmentosRecolhidos >= totalFragmentos;

    private void Start()
    {
        AtualizarHUDFragmentos();
    }

    public void RecolherFragmento()
    {
        fragmentosRecolhidos = Mathf.Clamp(fragmentosRecolhidos + 1, 0, totalFragmentos);

        Debug.Log("Fragmentos recolhidos: " + fragmentosRecolhidos + "/" + totalFragmentos);

        AtualizarHUDFragmentos();

        if (MapaCompleto)
        {
            Debug.Log("Mapa completo!");

            if (loadSceneWhenMapComplete && !string.IsNullOrEmpty(nomeCenaSeguinte))
            {
                SceneManager.LoadScene(nomeCenaSeguinte);
            }

            if (showmapcutscene)
            {
                cutscenescreanhud.SetActive(true);
            }
        }


    }

    private void AtualizarHUDFragmentos()
    {
        if (hudController != null)
        {
            hudController.UpdateFragments(fragmentosRecolhidos);
        }
    }
}