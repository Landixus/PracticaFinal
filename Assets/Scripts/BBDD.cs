
using UnityEngine;
using System;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.Networking;
using System.Collections;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.ComponentModel;
using System.IO;

public class BBDD: MonoBehaviour
{
    //La connexio pot ser statica ja que sempre sera la mateixa per totes les connexions
    public string conn;
    int error;
    private int filesFinished;
  
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

    public void InsertRoute(int userid, string validName, string originalPath, string description)
    {

        Debug.Log("Try to pass file to server");
        System.Net.WebClient Client = new System.Net.WebClient();

        Client.Headers.Add("Content-Type", "binary/octet-stream");

        byte[] result = Client.UploadFile("http://localhost/PracticaFinal/uploadFile.php", "POST",
                                          originalPath);

        string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);

        Debug.Log("File passed");

        StartCoroutine(insertRouteCorroutine(userid, validName, description));
    }

    IEnumerator insertRouteCorroutine(int userid, string validName, string description) 
    {
        WWWForm form = new WWWForm();
        form.AddField("userID", userid);
        form.AddField("fileName", validName);
        form.AddField("description", description);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/PracticaFinal/insertRoute.php", form);

        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

        if (www.downloadHandler.text == "0")
        {
            Debug.Log("Route inserted");
        }
        else if (www.downloadHandler.text == "1")
        {
            Debug.Log("Route insert failed " + www.downloadHandler.text);
       
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


    public void SelectFilesofUser(int userId)
    {
        RoutesManager.updateList = false;

        StartCoroutine(SelectFilesofUserCorroutine(userId));
        //RoutesManager.force = true;
    }

    IEnumerator SelectFilesofUserCorroutine(int userId)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/PracticaFinal/selectRoutesName.php", form);

        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        if (www.downloadHandler.text != "0")
        {
            Debug.Log("User Exists");
            
            string[] fileNames = www.downloadHandler.text.Split('#');
            Array.Resize(ref fileNames, fileNames.Length - 1);
            RoutesManager.numRoutes = fileNames.Length;
            foreach (String name in fileNames)
            {
                DownloadFileAsync(name);
            }

            RoutesManager.updateList = true;
            RoutesManager.routesBBDD = true;
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    private void DownloadFileAsync(string fileName)
    {
        string url = "http://localhost/PracticaFinal/fileDownloader.php?fileName=" + fileName;

        using (WebClient client = new WebClient()) {
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileAsync(new Uri(url), Path.Combine(Application.dataPath, "GPX", fileName + ".gpx"));
        }
    }

    private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        // In case you don't have a progressBar Log the value instead 
        // Console.WriteLine(e.ProgressPercentage);
        //Debug.Log(e.ProgressPercentage);
    }

    void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
        //MessageBox.Show(print);
        RoutesManager.numRoutesFinished++;
        
    }

    internal void InsertWorkout(Workout workout)
    {
        StartCoroutine(InsertWorkoutCorroutine(workout));
    }

    IEnumerator InsertWorkoutCorroutine(Workout workout) 
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", PaginaPrincipal.user.id);
        form.AddField("tempsTotal", workout.tempsTotal);
        form.AddField("description", workout.description);
        form.AddField("name", workout.name);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/PracticaFinal/insertWorkout.php", form);

        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        if (www.downloadHandler.text != "-1")
        {
            Debug.Log("Workout Inserted");

            foreach (Bloc bloc in workout.blocs)
            {
                StartCoroutine(InsertBlocCorroutine(bloc, www.downloadHandler.text));
            }
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

    IEnumerator InsertBlocCorroutine(Bloc bloc, string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("bloc_id", bloc.numBloc);
        form.AddField("workout_id", id);
        form.AddField("pot", bloc.pot);
        form.AddField("temps", bloc.temps);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/PracticaFinal/insertBloc.php", form);

        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        if (www.downloadHandler.text == "1")
        {
            Debug.Log("Bloc Inserted");
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }


    public void SelectWorkouts(int userId) {

        StartCoroutine(SelectWorkoutsCorroutine(userId));
        PaginaPrincipal.workoutPopulated = true;
    }

    IEnumerator SelectWorkoutsCorroutine(int userId)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", userId);
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/PracticaFinal/selectWorkouts.php", form);

        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);
        if (www.downloadHandler.text != "-1")
        {
            Debug.Log(www.downloadHandler.text);
            string[] workouts = www.downloadHandler.text.Split('@');
            Array.Resize(ref workouts, workouts.Length - 1);

            foreach (var workout in workouts)
            {
                string[] parts = workout.Split('&');
                string workoutInfoString = parts[0];

                string[] workoutInfo = workoutInfoString.Split('#');
                //Array.Resize(ref workoutInfo, workoutInfo.Length - 1);

                Workout workout1 = new Workout(int.Parse(workoutInfo[0]));
                workout1.description = workoutInfo[2];
                workout1.tempsTotal = int.Parse(workoutInfo[1]);
                workout1.name = workoutInfo[3];

                string[] blocs = parts[1].Split('%');
                Array.Resize(ref blocs, blocs.Length - 1);
                foreach (var bloc in blocs)
                {
                    string[] blocInfo = bloc.Split('#');
                    Bloc newBloc = new Bloc(int.Parse(blocInfo[0]));

                    newBloc.pot = int.Parse(blocInfo[2]);
                    newBloc.temps = int.Parse(blocInfo[3]);
                    workout1.AddBloc(newBloc);
                }

                PaginaPrincipal.user.workouts.Add(workout1);
            }
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
