
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

        /*
            Mirar si duracio ruta és inferior a duració entrenament
            si es aixi mostrar advertencia
         */

        //Resources.FindObjectsOfTypeAll<Panel>();
        //Agafar panell inactiu

        Ruta ruta = FollowRoute.ruta;
        Workout workout = FollowRoute.workout;

        if (workout != null)
        {
            var temps = ruta.totalDistance / 20;
            var segons = temps * 60 * 60;

            if (segons < workout.tempsTotal)
            {
                advertPanel.SetActive(true);
                //Ensenyar avis
            }
            else {
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
