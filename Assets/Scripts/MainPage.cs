using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaginaPrincipal : MonoBehaviour
{

    public static User user;
    public static bool haveUSB;

    // Start is called before the first frame update
    void Start()
    {
        
        LoadANTPrefabs();

        DontDestroyOnLoad(GameObject.Find("RoutesManager"));
    }

    private static void LoadANTPrefabs()
    {
        //Carguem la scena on hi han els prefabs de manera aditiva
        try
        {
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            haveUSB = true;
        }
        catch (System.Exception e)
        {
            haveUSB = false;
            Debug.LogWarning(e.StackTrace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!haveUSB)
        {

        }
    }

    public void goToSelectDevices()
    {
        SceneManager.LoadScene(sceneName: "PairDevices");
    }

    public void goToGPXUploader()
    {
        SceneManager.LoadScene(sceneBuildIndex: 6);
    }

    public void goToFollowRoute()
    {
        SceneManager.LoadScene(sceneBuildIndex: 8);
    }

    public void goToCreateWorkout()
    {
        SceneManager.LoadScene(sceneBuildIndex: 10);
    }
}
