using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedRoute : MonoBehaviour
{

    static public Ruta ruta;

    private GameObject graph_windowObj;
    private Window_Graph graph_window;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Window_Graph"))
        {
            graph_windowObj = GameObject.Find("Window_Graph");
            graph_window = (Window_Graph)graph_windowObj.GetComponent(typeof(Window_Graph));

            //No s'ha de passar la pendent sino la altura
            graph_window.ShowGraph(ruta.trackPoints, ruta.pendentPunts);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
