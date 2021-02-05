
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    

    public void goToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }

    public void goToFollowRoute()
    {
        SceneManager.LoadScene(sceneBuildIndex: 9);
    }

    public void goToSelectRoute()
    {
        SceneManager.LoadScene(sceneBuildIndex: 8);
    }

    public void goToSelectWorkout()
    {
        SceneManager.LoadScene(sceneBuildIndex: 11);
    }

    public void goToFollowROuteWithoutWorkout()
    {
        FollowRoute.workout = null;
        SceneManager.LoadScene(sceneBuildIndex: 9);
    }
}
