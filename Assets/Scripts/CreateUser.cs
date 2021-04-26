using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateUser : MonoBehaviour
{
    private string mail;
    private string password;
    private string confirmPassword;
    private int height;
    private int weight;

    public GameObject textDisplay;

    public GameObject mailInput;
    public GameObject mailErrorDisplay;

    public GameObject passwordInput;
    public GameObject passwordErrorDisplay;

    public GameObject confirmPasswordInput;
    public GameObject confirmPasswordError;

    public GameObject alturaInput;
    public GameObject alturaError;

    public GameObject pesInput;
    public GameObject pesError;


    public void StoreName()
    {

        Match emailMatch = regexMail();
        Match passMatch = regexPass();
        bool passwordEquals = comparePassword();
        bool alturaCorrecte = comprovarAltura();
        bool pesCorrecte = comprovarPes();

        if (mailInput.GetComponent<InputField>().text == "")
        {
            mailErrorDisplay.GetComponent<Text>().text = "El correu no pot estar buit";
            return;
        }

        if (emailMatch.Success && passMatch.Success && passwordEquals && alturaCorrecte && pesCorrecte)
        {
            textDisplay.GetComponent<Text>().text = "Welcome " + mail + " to the Game";
            //Guardar usuari a la BBDD i passar-lo a la pàgina principal

            GameObject go = GameObject.Find("BBDD_Manager");
            BBDD baseDades = (BBDD)go.GetComponent(typeof(BBDD));
            //Insert tindra un return per saber el tipus d'error
            //Sobretot per que no es repeteixi el correu

            baseDades.insertUser(mail, password, height, weight);
            
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "Can't create User";
        }
    }

    public void comprovarMail()
    {
        if (mailInput.GetComponent<InputField>().text == "")
        {
            mailErrorDisplay.GetComponent<Text>().text = "El correu no pot estar buit";
        }
        else {
            Match match = regexMail();
            if (match.Success)
            {
                mailErrorDisplay.GetComponent<Text>().text = "";

            }
            else
            {
                mailErrorDisplay.GetComponent<Text>().text = "Correu incorrecte";

            }
        }
    }
    private Match regexMail()
    {
        Regex regexMail = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z");
        mail = mailInput.GetComponent<InputField>().text;

        Match match = regexMail.Match(mail);

        return match;
    }

    public void comprovarPassword()
    {
        Match match = regexPass();
        if (match.Success)
        {
            passwordErrorDisplay.GetComponent<Text>().text = "";

        }
        else
        {
            passwordErrorDisplay.GetComponent<Text>().text = "Password no compleix amb les regles";

        }
    }

    private Match regexPass()
    {

        //Expressió regular per mirar que una contrasenya contingui com a mínim un a minúscula, majúscula i símbol 
        //8 o més caràcters 
        Regex regexPass = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        password = passwordInput.GetComponent<InputField>().text;

        Match match = regexPass.Match(password);
        return match;
    }

    private bool comparePassword() {

        confirmPassword = confirmPasswordInput.GetComponent<InputField>().text;
        return confirmPassword.Equals(password);
    }

    public void confirmPassError() {
        bool equals = comparePassword();
        if (equals)
        {
            confirmPasswordError.GetComponent<Text>().text = "";

        }
        else
        {
            confirmPasswordError.GetComponent<Text>().text = "Les contrasenyes no son iguals";

        }
    }

    public bool comprovarAltura() {
        if (Int32.TryParse(alturaInput.GetComponent<InputField>().text, out int j))
        {
            height = j;

            if (height <= 0 || height > 240)
            {
                return false;
            }
            else {
                return true;
            }  
        }
        else
        {
            return false;
        }
    }

    public void mostrarAlturaError() 
    {
        bool correcte = comprovarAltura();
        if (correcte)
        {
            alturaError.GetComponent<Text>().text = "";
        }
        else
        {
            alturaError.GetComponent<Text>().text = "Error en l'altura\n(altura entre 0 i 240cm)";
        }
    }

    private bool comprovarPes()
    {

        if (Int32.TryParse(pesInput.GetComponent<InputField>().text, out int j))
        {
            weight = j;

            if (weight < 30 || weight > 150)
            {
                return false;
            }
            else
            {
               return true;
            }
        }
        else
        {
            return false; ;
        }
    }

    public void mostrarPesError() 
    {
        bool correcte = comprovarPes();
        if (correcte)
        {
            pesError.GetComponent<Text>().text = "";   
        }
        else
        {
            pesError.GetComponent<Text>().text = "Error en el pes\n(pes entre 30 i 150kg)";
        }
    }

    public void goToLogIn()
    {
        SceneManager.LoadScene(sceneName: "LogIn");
    }

    public void goToMainPage()
    {
        SceneManager.LoadScene(sceneName: "MainPage");
    }

    void Start()
    {
    }
}
