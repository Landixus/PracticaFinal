using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserDetail : MonoBehaviour
{
    private string mail;
    private string password;
    private string confirmPassword;
    private int height;
    private int weight;

    public Text mailInput;
    public Text mailErrorDisplay;

    public InputField passwordInput;
    public Text passwordErrorDisplay;

    public InputField confirmPasswordInput;
    public Text confirmPasswordError;

    public InputField alturaInput;
    public Text alturaError;

    public InputField pesInput;
    public Text pesError;

    public Text maxFC;
    public Text maxW;

    void Start() {

        User user = PaginaPrincipal.user;
        mailInput.text = user.mail;
        passwordInput.text = user.password;
        confirmPasswordInput.text = user.password;

        Debug.Log(user.height);
        alturaInput.text = user.height.ToString();
        pesInput.text = user.weight.ToString();

        maxFC.text = "MaxFC: " + user.maxFC.ToString();
        maxW.text = "MaxW: " + user.maxW.ToString();
    }

    public void EditUser()
    {
        mailErrorDisplay.GetComponent<Text>().color = Color.red;
        Match passMatch = regexPass();
        bool passwordEquals = comparePassword();
        bool alturaCorrecte = comprovarAltura();
        bool pesCorrecte = comprovarPes();


        if (passMatch.Success && passwordEquals && alturaCorrecte && pesCorrecte)
        {
            //Guardar usuari a la BBDD i passar-lo a la pàgina principal
            BBDD baseDades = new BBDD();

            //Edit tindra un return per saber el tipus d'error
            //Sobretot per que no es repeteixi el correu

            int insertErrorId = baseDades.editUser(PaginaPrincipal.user.id, password, height, weight);
            //Posible modificacio: Canviar a switch si hi ha més d'un id d'error
            Debug.Log(insertErrorId);

            if (insertErrorId != 0)
            {
                mailErrorDisplay.GetComponent<Text>().text = "No es pot editar l'usuari";
            }
            else {
                mailErrorDisplay.GetComponent<Text>().color = Color.blue;
                mailErrorDisplay.GetComponent<Text>().text = "S'ha editat l'usuari";
            }
             //baseDades.SelectTest();
        }
        else
        {
            mailErrorDisplay.GetComponent<Text>().text = "Can't update User";
        }
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

    private bool comparePassword()
    {

        confirmPassword = confirmPasswordInput.GetComponent<InputField>().text;
        return confirmPassword.Equals(password);
    }

    public void confirmPassError()
    {
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

    public bool comprovarAltura()
    {
        if (Int32.TryParse(alturaInput.GetComponent<InputField>().text, out int j))
        {
            height = j;

            if (height <= 0 || height > 240)
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
}
