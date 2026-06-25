using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutScenes : MonoBehaviour
{

    [SerializeField] private string scenename = "Nivel_01_PrateleiraInferior";

    [SerializeField] private float CutsceneDuration = 5;


    void Start()
    {
        StartCoroutine(ChangeToNextScene());
    }


    private IEnumerator ChangeToNextScene()
    {
        yield return new WaitForSeconds(CutsceneDuration);
        SceneManager.LoadScene(scenename);
    }
}
