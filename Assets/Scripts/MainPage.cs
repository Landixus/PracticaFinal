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

    [SerializeField] Image usbManager;

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

            //Mostrar icona USB bo
            if (UsbImageManager.usbImage != null && haveUSB)
            {
                if (haveUSB)
                {
                    UsbImageManager.usbImage.sprite = (Sprite)Resources.Load("Img/usb");
                } else {
                    UsbImageManager.usbImage.sprite = (Sprite)Resources.Load("Img/noUSB");
                }   
            } 
        }
        catch (ANT_Exception exc) {
            haveUSB = false;
            Debug.LogWarning(exc.StackTrace);
            
            //Mostrar icona USB Dolent
        }
        catch (System.Exception e)
        {
            haveUSB = false;
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
