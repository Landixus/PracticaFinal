using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectedRoute : MonoBehaviour
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
        //Agafar Nom
        string newName = fileNameInput.text;
        string validName;
        //Comprovar si el nou nom es el mateix que l'antic ja que aixi no hem de comprovar res 
        if (oldName == newName)
        {
            validName = newName;
        }
        else
        {
            // Comprovar si el nou nom es valid
            validName = UseRegex(newName);
            fileNameInput.text = validName;
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


            SceneManager.LoadScene(6);
        }
        catch (Exception)
        {
            errorNameText.text = "Ja existeix un fitxer amb aquest nom";
            //throw;
        }
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
}
