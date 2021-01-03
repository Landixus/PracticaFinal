using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RoutesManager : MonoBehaviour
{
    static public List<Ruta> rutas = new List<Ruta>();
    GameObject select;
    RouteGenerator routeGenerator;

    // Start is called before the first frame update
    void Start()
    {

        select = GameObject.Find("RouteGenerator");
        routeGenerator = select.GetComponent<RouteGenerator>();
        //Llegir carpeta  Path.Combine(Application.dataPath , "GPX")
        //Per cada fitxer GPX cridar SeleccionRuta GetTrack(ruta fixter)
        //Anar omplint la llista de rutes amb la informacio de GetTrack

        try
        {
            string[] files = Directory.GetFiles(Path.Combine(Application.dataPath, "GPX"));

            foreach (string fileName in files)
                ProcessFile(fileName);

            printList();
        }
        catch (System.Exception)
        {
            Debug.LogWarning("Alguna cosa no ha anat bé al intentar llegir el directori (RoutesManager)");
            throw;
        }
        
    }

    private void printList()
    {
        foreach (var route in rutas)
        {
            Debug.Log(route.toString());
        }
    }

    private void ProcessFile(string fileName)
    {
        if (IsGPX(fileName))
        {
            string path = Path.Combine(Application.dataPath, "GPX", fileName);

            //Debug.Log(path);
            //creem l'objecte ruta utilitzant la ruta del gpx
            Ruta ruta = routeGenerator.GetTrack(path);
            //afegim a llista
            rutas.Add(ruta);
        }
    }

    private bool IsGPX(string fileName)
    {
        //Separem el nom de la extensió .gpx
        string[] nameParts = fileName.Split('.');

        //Sabem que l'extensió és la l'ultima part ja que la primera es el nom
        string extension = nameParts[nameParts.Length - 1];

        //Com Unity crea fitxers .meta per defecte hem de mirar si l'extensio es gpx o no, ja que els .meta no els volem agafar
        return extension == "gpx";
    }
}
