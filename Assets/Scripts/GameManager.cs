using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public int fragmentosRecolhidos = 0;
    public int totalFragmentos = 4;
    [SerializeField] private float timeRemaining = 60f;
    [SerializeField] private float maxtimeRemaining = 60f;
    private bool timerRunning = true;



    [SerializeField] private string nomeCenaSeguinte;

    private String gameoverscene = "gameover"; //game over scene this can be serialized or public

    [SerializeField] private HUDController hudController;


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

    void Update()
    {
        if (timerRunning && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;
            OnTimerEnd();
        }


        //regra de tres simples???



        //hudController.UpdateTemperature(timeRemaining);
        hudController.UpdateTemperature(100 * timeRemaining / maxtimeRemaining);
    }




    void OnTimerEnd()
    {
        SceneManager.LoadScene(gameoverscene);
    }
        public void PauseTimer() => timerRunning = false;
    public void ResumeTimer() => timerRunning = true;
    public void ResetTimer() { timeRemaining = 60f; timerRunning = true; }
}