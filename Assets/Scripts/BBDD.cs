
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.Networking;
using System.Collections;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BBDD: MonoBehaviour
{
    //La connexio pot ser statica ja que sempre sera la mateixa per totes les connexions
    public string conn;
    int error;

    private void Start()
    {
        conn = "URI=file:" + UnityEngine.Application.dataPath + "/baseDadesSQLitle.db"; //Path to database.
    }
  
    public void insertUser(String mail, String password, int height, int weight) {
        StartCoroutine(insertUserCorroutine(mail, password, height, weight));
    }

    IEnumerator insertUserCorroutine(String mail, String password, int height, int weight) {
        WWWForm form = new WWWForm();
        form.AddField("email", mail);
        form.AddField("password", password);
        form.AddField("height", height);
        form.AddField("weight", weight);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/PracticaFinal/insertUser.php", form);

        yield return www.SendWebRequest();

        if (www.downloadHandler.text == "0")
        {
            Debug.Log("Correct insert");
        } else if (www.downloadHandler.text == "notUnique") {
            Debug.Log("Ja existeix el correu a la BBDD");
            GameObject errorText = GameObject.Find("Output");
            errorText.GetComponent<Text>().text = "Can't create User";

            GameObject emailText = GameObject.Find("EmailErrorText");
            emailText.GetComponent<Text>().text = "Ja existeix un usuari amb aquest correu";

        }
        else if (www.downloadHandler.text == "1")
        {
            Debug.Log("Incorrect insert");
        }
        else
        {
            Debug.Log("Other error ERROR #" + www.downloadHandler.text);
        }
    }

    public int editUser(int id, string password, int height, int weight)
    {
        StartCoroutine(editUserCorrutine(id, password, height, weight));

        return error;
    }

    IEnumerator editUserCorrutine(int id, string password, int height, int weight)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("password", password);
        form.AddField("height", height);
        form.AddField("weight", weight);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/PracticaFinal/updateUser.php", form);

        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        if (www.downloadHandler.text == "0")
        {
            Debug.Log("Correct update");
            error = 0; //ID return 1 == s'ha trobat una coincidencia
        }
        else if (www.downloadHandler.text == "1")
        {
            Debug.Log("Incorrect update");
            error = 1;
        }
        else
        {
            Debug.Log("Other error ERROR #" + www.downloadHandler.text);
            error = 2;
        }
    }

    public void selectUser(String mail, Text errorText)
    {

        StartCoroutine(selectUserCorroutine(mail, errorText));

    }

    IEnumerator selectUserCorroutine(String mail, Text errorText)
    {
        //Variables per crear l'usuari
        int id;
        string email;
        string password;
        int height;
        int weight;
        int maxFC;
        int maxW;

        WWWForm form = new WWWForm();
        form.AddField("email", mail);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/PracticaFinal/selectUser.php", form);

        yield return www.SendWebRequest();

        if (www.downloadHandler.text != "1" || www.downloadHandler.text != "1")
        {
            Debug.Log("User Exists");
            error = 1; //ID return 1 == s'ha trobat una coincidencia

            try
            {
                string[] webResult = www.downloadHandler.text.Split('#');


                Debug.Log(webResult);
                id = int.Parse(webResult[0]);
                email = webResult[1];
                password = webResult[2];
                height = int.Parse(webResult[3]);
                weight = int.Parse(webResult[4]);
                maxFC = int.Parse(webResult[5]);
                maxW = int.Parse(webResult[6]);

                //Debug.Log("id= " + id + "  email=" + email + "  password=" + password + "  height=" + height + "  weight=" + weight + "  maxFC=" + maxFC + "  maxW=" + maxW);

                PaginaPrincipal.user = new User(id, email, password, height, weight, maxFC, maxW);

                SceneManager.LoadScene(sceneName: "MainPage");
            }
            catch (Exception)
            {
                errorText.text = "Hi ha hagut un problema a l'hora d'iniciar sessió, torni a intentar";
                //throw;
            }

           
        }
        else if (www.downloadHandler.text == "1")
        {
            Debug.Log("User no exists");
            error = 0;
        }
        else
        {
            Debug.Log("User creation failed ERROR #" + www.downloadHandler.text);
            error = 2;
        }
    }

    public void CallRegister(string email, string password) 
    {
        error = -1;
        GameObject passwordErrorDisplay = GameObject.Find("Output");
        Text errorText = passwordErrorDisplay.GetComponent<Text>();
        errorText.text = "";
        StartCoroutine(LogInUser(email, password, errorText));
        Debug.Log("Hola1" + error);
    }

    IEnumerator LogInUser(string email, string password, Text errorText) {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/PracticaFinal/checkCorrectUser.php", form);
        
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        if (www.downloadHandler.text == "0")
        {
            Debug.Log("User Exists");
            error = 1; //ID return 1 == s'ha trobat una coincidencia
            
            selectUser(email, errorText);

        } else if (www.downloadHandler.text == "1") {
            Debug.Log("User no exists");
            error = 0;
            errorText.text = "Correu o contrasenya no vàlidas.";

        }
        else {
            Debug.Log("User creation failed ERROR #" + www.downloadHandler.text);
            error = 2;
            errorText.text = "Correu o contrasenya no vàlidas.";
        }
    } 
}
