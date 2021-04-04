using ANT_Managed_Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PaginaPrincipal : MonoBehaviour
{

    public static User user;
    public static bool haveUSB;

    [SerializeField] Text userNameTxt;

    // Start is called before the first frame update
    void Start()
    {
        LoadANTPrefabs();

        DontDestroyOnLoad(GameObject.Find("RoutesManager"));

        userNameTxt.text = "Benvingut: " + user.getMail();
    }

    private static void LoadANTPrefabs()
    {
        //Carguem la scena on hi han els prefabs de manera aditiva
        try
        {
            SceneManager.LoadScene(3, LoadSceneMode.Additive);
            haveUSB = true;
            //UsbImageManager.haveUsb = true;
        }
        catch (ANT_Exception exc)
        {
            //UsbImageManager.haveUsb = false;
            Debug.LogWarning(exc.StackTrace);
        }
        catch (System.Exception e)
        {
            UsbImageManager.haveUsb = false;
            Debug.LogError(e.StackTrace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!haveUSB)
        {
            LoadANTPrefabs();
        }
    }

    public void goToPairDevices()
    {
        SceneManager.LoadScene(sceneName: "PairDevices");
    }

    public void goToGPXUploader()
    {
        SceneManager.LoadScene(sceneName: "UploadGPX");
    }

    public void goToSelectRoute()
    {
        SceneManager.LoadScene(sceneName: "SelectRoute");
    }

    public void goToCreateWorkout()
    {
        SceneManager.LoadScene(sceneName: "CreateWorkout");
    }
}