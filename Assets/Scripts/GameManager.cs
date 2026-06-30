using UnityEngine;
using UnityEngine.SceneManagement;


using System.Collections;


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

    [Header("Nivel Completo")]
    [SerializeField] private int nivelcompleto = 0;


    [Header("Optional For Level 2")]
    [SerializeField] private bool showmapcutscene = false;
    [SerializeField] private GameObject cutscenescreanhud;

    [SerializeField] private MyVideoPlayerInGame myVideoPlayerInGame;

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

            GameGlobal.updatelevelcompleted(nivelcompleto);
            int x = GameGlobal.getlevelcompleted();
            Debug.Log("NIvel " + x.ToString() + "Completo");

            if (loadSceneWhenMapComplete && !string.IsNullOrEmpty(nomeCenaSeguinte))
            {
                SceneManager.LoadScene(nomeCenaSeguinte);
            }

            if (showmapcutscene)
            {
                cutscenescreanhud.SetActive(true);
                Time.timeScale = 0f;
                myVideoPlayerInGame.StartCut();
                StartCoroutine(waitforcutscene());
            }
        }


    }



    private IEnumerator waitforcutscene()
    {
        yield return new WaitForSecondsRealtime(8f);
        cutscenescreanhud.SetActive(false); //Cutscene Duration
        Time.timeScale = 1f;
    }

    private void AtualizarHUDFragmentos()
    {
        if (hudController != null)
        {
            hudController.UpdateFragments(fragmentosRecolhidos);
        }
    }
}