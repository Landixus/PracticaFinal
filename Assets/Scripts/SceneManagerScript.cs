
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] GameObject advertPanel;

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


    public void goToFollowRoute()
    {
        SceneManager.LoadScene(sceneName: "FollowRoute");

        /*
            Mirar si duracio ruta és inferior a duració entrenament
            si es aixi mostrar advertencia
         */

        //Resources.FindObjectsOfTypeAll<Panel>();
        //Agafar panell inactiu

        Debug.Log("Estic a goToFollowRoute");

        Ruta ruta = FollowRoute.ruta;
        Workout workout = FollowRoute.workout;

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
            else {
                Debug.Log("Anem a carregar l'escena FollowRoute");
                SceneManager.LoadScene(sceneName: "FollowRoute");
            }
        }
    }

    public void cancelPanel()
    {
        GameObject panel = GameObject.Find("AdvertPanel");
        panel.SetActive(false);
    }

    public void goToFollowRouteAfterWorkout()
    {
        SceneManager.LoadScene(sceneName: "FollowRoute");
    }

    public void goToFollowRouteWithoutWorkout()
    {
        FollowRoute.workout = null;
        SceneManager.LoadScene(sceneName: "FollowRoute");
    }
}
