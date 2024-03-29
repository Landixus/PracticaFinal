﻿
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] GameObject advertPanel;
    [SerializeField] GameObject nullWorkout;
    [SerializeField] GameObject devicePanel;

    public bool simular = false;

    public void goToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }

    /*public void goToFollowRoute()
    {
        SceneManager.LoadScene(sceneBuildIndex: 9);
    }*/

    public void goToSelectRoute()
    {
        SceneManager.LoadScene(sceneName: "SelectRoute");
    }

    public void goToSelectWorkout()
    {
        SceneManager.LoadScene(sceneName: "SelectWorkout");

    }

    public void goToSelectWorkoutCheckingRouteSelected()
    {
        if (FollowRoute.ruta != null)
        {
            SceneManager.LoadScene(sceneName: "SelectWorkout");
        }
        else {
            ListGenerator.errorText.text = "Error: No s'ha escollit ruta";
        }
    }

    private bool checkDevicesConnectes()
    {
        if (simular)
        {
            return true;
        }

        if (PaginaPrincipal.powerDevice == null || PaginaPrincipal.rodilloDevice == null)
        {
            StartCoroutine("ActivarPanelAdvert");
            return false;
        }

        return true;
    }

    IEnumerator ActivarPanelAdvert()
    {

        devicePanel.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        devicePanel.SetActive(false);

        PaginaPrincipal.user.EnsenyarWorkoutsLog();
        //reset
        SceneManager.LoadScene(sceneName: "PairDevices");
    }

    public void goToFollowRoute()
    {
 

        /*
            Mirar si duracio ruta és inferior a duració entrenament
            si es aixi mostrar advertencia
         */

        //Resources.FindObjectsOfTypeAll<Panel>();
        //Agafar panell inactiu

        Debug.Log("Estic a goToFollowRoute");

        Ruta ruta = FollowRoute.ruta;
        Workout workout = FollowRoute.workout;

        Debug.Log(workout);
        if (checkDevicesConnectes())
        {
            if (workout != null)
            {
                Debug.Log("Vaig a calcular temps");
                var temps = ruta.totalDistance / 20;
                var segons = temps * 60 * 60;

                if (segons < workout.tempsTotal)
                {
                    advertPanel.SetActive(true);
                    //Ensenyar avis
                }
                else
                {
                    Debug.Log("Anem a carregar l'escena FollowRoute");
                    SceneManager.LoadScene(sceneName: "FollowRoute");
                }
            }
            else
            {
                Debug.Log("No s'ha escollit cap workout");
                StartCoroutine("ActivarPanelNull");
            }
        }
    }

    public void cancelAdvertPanel()
    {
        GameObject panel = GameObject.Find("AdvertPanel");
        panel.SetActive(false);
    }

    public void cancelNullPanel()
    {
        nullWorkout.SetActive(false);
    }

    IEnumerator ActivarPanelNull()
    {

        nullWorkout.SetActive(true);

        yield return new WaitForSeconds(4f);

        nullWorkout.SetActive(false);
    }

    public void goToFollowRouteAfterWorkout()
    {
        SceneManager.LoadScene(sceneName: "FollowRoute");
    }

    public void goToFollowRouteWithoutWorkout()
    {
        if (checkDevicesConnectes())
        {
            FollowRoute.workout = null;
            SceneManager.LoadScene(sceneName: "FollowRoute");
        }
    }
}
