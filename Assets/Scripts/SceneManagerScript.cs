
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    

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
        SceneManager.LoadScene(sceneBuildIndex: 8);
    }

    public void goToSelectWorkout()
    {
        SceneManager.LoadScene(sceneBuildIndex: 11);

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
            var segons = temps * 60;

            if (segons < workout.tempsTotal)
            {
                panel.SetActive(true);
                //Ensenyar avis
            }
            else {
                SceneManager.LoadScene(sceneBuildIndex: 11);
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
        SceneManager.LoadScene(sceneBuildIndex: 11);
    }

    public void goToFollowRouteWithoutWorkout()
    {
        FollowRoute.workout = null;
        SceneManager.LoadScene(sceneBuildIndex: 9);
    }
}
