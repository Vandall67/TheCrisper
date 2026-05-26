using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string scenename = "Nivel_01_PrateleiraInferior";


    public void ChangeToScene()
    {
        Debug.Log(scenename); 
        SceneManager.LoadScene(scenename);

    }


}
