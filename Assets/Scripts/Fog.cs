using UnityEngine;

public class Fog : MonoBehaviour
{
    [Header("Fragmento do Mapa")]
    [SerializeField] private GameObject fragmentomapa;


    void Update()
    {
        if (!fragmentomapa.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
