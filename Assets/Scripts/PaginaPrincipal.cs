using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaginaPrincipal : MonoBehaviour
{

    public static User user;

    // Start is called before the first frame update
    void Start()
    {
        //Carguem la scena on hi han els prefabs de manera aditiva
        SceneManager.LoadScene(3, LoadSceneMode.Additive);

        DontDestroyOnLoad(GameObject.Find("RouteManager"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToSelectDevices()
    {
        SceneManager.LoadScene(sceneName: "PairDevices");
    }

    public void goToGPXUploader()
    {
        SceneManager.LoadScene(sceneBuildIndex: 6);
    }
}
