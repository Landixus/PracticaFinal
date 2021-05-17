using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmGPX : MonoBehaviour
{
    public InputField fileNameInput;
    public Text errorNameText;
    public Text totalDistanceText;
    public Text positiveElevationText;
    public Text negativeElevationText;
    public InputField descriptionInput;

    static public Ruta ruta;
    static public string originalPath;

    private GameObject graph_windowObj;
    private Window_Graph graph_window;

    private string oldName;


    [SerializeField] GameObject panel;
    [SerializeField] Text textPanel;
    private bool correcte = false;


    /*
     CREATE TABLE route (
	    id_route SERIAL,
	    userID int NOT NULL,
	    name VARCHAR(100) NOT NULL,
	    file XML NOT NULL,
	    PRIMARY KEY(id_route),
	    FOREIGN KEY (userID) REFERENCES public.user(id_user)
    )
     */


    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Window_Graph"))
        {
            graph_windowObj = GameObject.Find("Window_Graph");
            graph_window = (Window_Graph)graph_windowObj.GetComponent(typeof(Window_Graph));

            graph_window.ShowGraph(ruta.trackPoints, ruta.pendentPunts, ruta.totalDistance,150);
        }

        fileNameInput.characterLimit = 100;
        descriptionInput.characterLimit = 2000;
        errorNameText.text = "";

        oldName = GetNameFromPath();
        totalDistanceText.text = ruta.totalDistance.ToString() + " Km";
        positiveElevationText.text = ruta.positiveElevation.ToString() + "m";
        negativeElevationText.text = "-" + ruta.negativeElevation.ToString() + "m";
    }

    private string GetNameFromPath()
    {
        //Separem al ruta sencera
        string[] pathParts = originalPath.Split('\\');
        //Separem el nom de la extensió .gpx
        string[] nameParts = pathParts[pathParts.Length - 1].Split('.');
        //Sabem que el nom és la primera part ja que la segona es la extensio
        string fileName = nameParts[0];

        fileNameInput.text = fileName;
        
        //Debug.Log(fileName);

        return null;
    }

    public void ConfirmRoute()
    {

        //Mirar que el nom no existeix en la llista de rutes


        //Agafar Nom
        string newName = fileNameInput.text;
        string validName;
        //Comprovar si el nou nom es el mateix que l'antic ja que aixi no hem de comprovar res 

        foreach (Ruta ruta in RoutesManager.rutas)
        {
            if (ruta.name.Equals(newName))
            {
                errorNameText.text = "Ja existeix una ruta aquest nom";
                return;
            }
        }

        if (oldName == newName)
        {
            validName = newName;
            ruta.name = validName;
        }
        else
        {
            // Comprovar si el nou nom es valid
            validName = UseRegex(newName);
            fileNameInput.text = validName;
            ruta.name = validName;
        }

        //Creem nova ruta
        string newPath = Path.Combine(Application.dataPath , "GPX", validName + ".gpx");

        Debug.Log(newPath);
        //Copiar fitxer amb nou nom a la carpeta gpx
        try
        {
            File.Copy(originalPath, newPath);

            Debug.Log("Fitxer copiat");

            RoutesManager.rutas.Add(ruta);
            correcte = true;

            GameObject go = GameObject.Find("BBDD_Manager");
            BBDD baseDades = (BBDD)go.GetComponent(typeof(BBDD));

            string description = descriptionInput.text;

            GameObject go2 = GameObject.Find("RoutesManager");
            RoutesManager routeManager = (RoutesManager)go2.GetComponent(typeof(RoutesManager));
            //Processem el fitxer
            routeManager.ProcessFile(validName);

            //insertem a la base de dades el fitxer
            baseDades.InsertRoute(PaginaPrincipal.user.id, validName, originalPath, description);

            //Borrem tots els fitxers del directori GPX
            string path = Path.Combine(Application.dataPath, "GPX");
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

        }
        catch (Exception)
        {
            errorNameText.text = "Ja existeix un fitxer amb aquest nom";
            //throw;
            correcte = false;
            throw;
        }

        StartCoroutine("ActivarPanelCreat");
    }

    public string UseRegex(string strIn)
    {
        // Replace invalid characters with empty strings.
        return Regex.Replace(strIn, @"[^\w-_ñÑ]", "");
    }

    public void Cancel()
    {
        SceneManager.LoadScene(2);
    }

    public void SaveDescription() {
        ruta.description = descriptionInput.text;
        Debug.Log(ruta.description);
    }

    IEnumerator ActivarPanelCreat()
    {
        if (correcte)
        {
            textPanel.text = "EL GPX s'ha guardat correctament";
        }
        else
        {
            textPanel.text = "Error al pujar el GPX \n Ja existeix un fitxer amb aquest nom";
        }
        panel.SetActive(true);

        yield return new WaitForSeconds(1f);

        panel.SetActive(false);


        //reset

        if (correcte)
        {
            SceneManager.LoadScene(sceneBuildIndex: 6);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
