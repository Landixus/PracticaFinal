using ANT_Managed_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PaginaPrincipal : MonoBehaviour
{

    public static User user;
    public static bool haveUSB;
    public static bool workoutPopulated;

    [SerializeField] Text userNameTxt;


    public static AntDevice cadenceDevice;
    public static AntDevice speedCadenceDevice;
    public static AntDevice powerDevice;
    public static AntDevice rodilloDevice;
    public static AntDevice heartRateDevice;
    public static AntDevice speedDevice;

    [SerializeField] GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        LoadANTPrefabs();

        DontDestroyOnLoad(GameObject.Find("RoutesManager"));

        userNameTxt.text = "Benvingut: " + user.getMail();

        if (!workoutPopulated)
        {
            GameObject go = GameObject.Find("BBDD_Manager");
            BBDD baseDades = (BBDD)go.GetComponent(typeof(BBDD));

            baseDades.SelectWorkouts(PaginaPrincipal.user.id);
        }

        
    }

    private static void LoadANTPrefabs()
    {
        //Carguem la scena on hi han els prefabs de manera aditiva
        GameObject powerDisplayObject = GameObject.Find("PowerMeterDisplay");
        if (powerDisplayObject == null)
        {
            try
            {
                SceneManager.LoadScene(3, LoadSceneMode.Additive);

                //UsbImageManager.haveUsb = true;
            }
            catch (ANT_Exception exc)
            {
                //UsbImageManager.haveUsb = false;
                Debug.LogWarning(exc.StackTrace);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.StackTrace);
            }
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

    IEnumerator ActivarPanelCreat()
    {

        panel.SetActive(true);

        yield return new WaitForSeconds(1f);

        panel.SetActive(false);

        PaginaPrincipal.user.EnsenyarWorkoutsLog();
        //reset
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goToCreateWorkout()
    {
        SceneManager.LoadScene(sceneName: "CreateWorkout");
    }

    public void goToUserDetail() {
        SceneManager.LoadScene(sceneName: "UserDetail");
    }

    public void goToUserHist()
    {
        SceneManager.LoadScene(sceneName: "UserHistorial");
        //Debug.LogWarning("No implementat (goToUserHist)");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}