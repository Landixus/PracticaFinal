using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RoutesManager : MonoBehaviour
{
    static public List<Ruta> rutas = new List<Ruta>();
    static bool updateList = true;

    GameObject select;
    RouteGenerator routeGenerator;

    public GameObject routePrefab;
    private RectTransform scrollContainer;

    bool populated;


    private void Awake()
    {
        select = GameObject.Find("RouteGenerator");
        routeGenerator = select.GetComponent<RouteGenerator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        populated = false;


        //Llegir carpeta  Path.Combine(Application.dataPath , "GPX")
        //Per cada fitxer GPX cridar SeleccionRuta GetTrack(ruta fixter)
        //Anar omplint la llista de rutes amb la informacio de GetTrack
        if (updateList)
        {
            try
            {
                string[] files = Directory.GetFiles(Path.Combine(Application.dataPath, "GPX"));

                foreach (string fileName in files)
                    ProcessFile(fileName);

                //printList();
                updateList = false;
            }
            catch (System.Exception)
            {
                Debug.LogWarning("Alguna cosa no ha anat bé al intentar llegir el directori (RoutesManager)");
                throw;
            }
        }
    }

    private void Update()
    {
        try
        {
            scrollContainer = GameObject.Find("Scroll").GetComponent<RectTransform>();

            if (scrollContainer != null && !populated)
            {
                Debug.Log("Scroll find");
                //PopulateList();
                populated = true;
            }
        }
        catch (Exception)
        {
            Debug.LogWarning("No s'ha trobat Scroll");
            //throw;
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

    void PopulateList()
    {
        //try
        //{
            foreach (var item in rutas)
            {
                // Reference prefab variable instead of class type
                GameObject gameObject = Instantiate(routePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                gameObject.transform.SetParent(scrollContainer.transform.GetChild(0), false);

                for (int i = 0; i < gameObject.transform.childCount; ++i)
                {
                    Transform currentItem = gameObject.transform.GetChild(i);
                    currentItem.gameObject.SetActive(true);
                }

                gameObject.transform.GetChild(1).GetComponent<Text>().text = item.name;
                gameObject.transform.GetChild(3).GetComponent<Text>().text = item.totalDistance.ToString();
                gameObject.transform.GetChild(5).GetComponent<Text>().text = item.positiveElevation.ToString();
                gameObject.transform.GetChild(7).GetComponent<Text>().text = item.negativeElevation.ToString();

            //RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            
            Debug.Log("Item created");
            }
        //}
        //catch (System.Exception ex)
        //{

        //}
    }
}
