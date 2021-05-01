using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RoutesManager : MonoBehaviour
{
    static public List<Ruta> rutas = new List<Ruta>();
    public static bool updateList = true;

    GameObject select;
    RouteGenerator routeGenerator;

    public GameObject routePrefab;
    private RectTransform scrollContainer;

    public static int numRoutes;
    public static int numRoutesFinished;
    public static bool force;
    public static bool routesBBDD;

    bool populated;


    private void Awake()
    {
        select = GameObject.Find("RouteGenerator");
        routeGenerator = select.GetComponent<RouteGenerator>();

        //Demanem nom dels fitxers a la BBDD
        if (!routesBBDD)
        {
            GameObject go = GameObject.Find("BBDD_Manager");
            BBDD baseDades = (BBDD)go.GetComponent(typeof(BBDD));

            baseDades.SelectFilesofUser(PaginaPrincipal.user.id);

            force = false;
        }
    }

    // Start is called before the first frame update
    IEnumerator UpdateList()
    {
        populated = false;


        //Llegir carpeta  Path.Combine(Application.dataPath , "GPX")
        //Per cada fitxer GPX cridar SeleccionRuta GetTrack(ruta fixter)
        //Anar omplint la llista de rutes amb la informacio de GetTrack
        if (updateList)
        {
            
            string path = Path.Combine(Application.dataPath, "GPX");
            DirectoryInfo di = new DirectoryInfo(path);

            string[] files = Directory.GetFiles(path);

            foreach (string fileName in files)
            {
                ProcessFile(fileName);
                yield return null;
            }

            //printList();
            updateList = false;
            force = false;

            //Delete files from folder

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            

            Debug.Log("S'ha actualitzat llista");
        }
    }

    private void Update()
    {

        Debug.Log("Num files: " + numRoutes + " Num finished: " + numRoutesFinished + " updateList: " + updateList);
        if ((numRoutes < numRoutesFinished) || force)
        {
            try
            {
                StartCoroutine(UpdateList());
            }
            catch (System.Exception)
            {
                Debug.LogWarning("Alguna cosa no ha anat bé al intentar llegir el directori (RoutesManager)");
                throw;
            }
        }
    }

    private void printList()
    {
        foreach (var route in rutas)
        {
            Debug.Log(route.toString());
        }
    }

    public void ProcessFile(string fileName)
    {
        if (IsGPX(fileName))
        {
            string path = Path.Combine(Application.dataPath, "GPX", fileName);

            Debug.Log(path);
            //creem l'objecte ruta utilitzant la ruta del gpx
            Ruta ruta = routeGenerator.GetTrack(path);
            //afegim a llista
            if (ruta != null)
            {
                if (rutas.Count == 0)
                {
                    rutas.Add(ruta);
                    return;
                }
                foreach (Ruta item in rutas.ToArray())
                {
                    if (ruta.name == item.name)
                    {
                        return;
                    }
                    rutas.Add(ruta);
                }
                
            }
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
