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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void logIn()
    {
        mail = mailInput.GetComponent<InputField>().text;
        password = passwordInput.GetComponent<InputField>().text;

        BBDD baseDades = new BBDD();

       int returnId = baseDades.comprovarCredencials(mail, password);

        if (returnId == 1)
        {
            User user = baseDades.selectUser(mail);

            if (user != null)
            {
                passwordErrorDisplay.GetComponent<Text>().text = "Valid ";
                //go to main page
            }
            else
            {
                //ERROR
            }
        }
        else {
            passwordErrorDisplay.GetComponent<Text>().text = "Correu o contrasenya no vàlidas.";        
        }
    }

    public void goToCreateUserScene() 
    {
        SceneManager.LoadScene(sceneName: "CreateUser");
    }
}
