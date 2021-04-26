using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    private string mail;
    private string password;

    public GameObject mailInput;
    public GameObject mailErrorDisplay;

    public GameObject passwordInput;
    public GameObject passwordErrorDisplay;

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("BBDD_Manager"));
    }

    public void logIn()
    {
        mail = mailInput.GetComponent<InputField>().text;
        password = passwordInput.GetComponent<InputField>().text;

        //BBDD baseDades = new BBDD();

        GameObject go = GameObject.Find("BBDD_Manager");
        BBDD baseDades = (BBDD)go.GetComponent(typeof(BBDD));

        baseDades.CallRegister(mail, password);

    }

    public void goToCreateUserScene() 
    {
        SceneManager.LoadScene(sceneName: "CreateUser");
    }

    public void goToMainPage()
    {
        SceneManager.LoadScene(sceneName: "MainPage");
    }

    void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            logIn();
        }
    }
}
