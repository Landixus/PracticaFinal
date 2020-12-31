using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoutesManager : MonoBehaviour
{
    static public List<Ruta> rutas = new List<Ruta>();

    // Start is called before the first frame update
    void Start()
    {
        //Llegir carpeta  Path.Combine(Application.dataPath , "GPX")
        //Per cada fitxer GPX cridar SeleccionRuta GetTrack(ruta fixter)
        //Anar omplint la llista de rutes amb la informacio de GetTrack
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
