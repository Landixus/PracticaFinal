using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWorkoutList : MonoBehaviour
{
    static public bool afegit;
    public GameObject contentPanel;

    // Start is called before the first frame update
    void Start()
    {
        float myWidth = 0;
        //int listSize = RoutesManager.rutas.Count;
        int listSize = 2;
        if (listSize <= 4)
        {
            myWidth = Screen.width;
        }
        else
        {
            myWidth = listSize * transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y + 10 * listSize;
        }
        contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, myWidth);
        contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 108);
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < listSize; i++)
        {
            //Ruta ruta = RoutesManager.rutas[i];
            g = Instantiate(buttonTemplate, transform);
            /*g.transform.GetChild(1).GetComponent<Text>().text = ruta.name;
            g.transform.GetChild(3).GetComponent<Text>().text = ruta.totalDistance.ToString() + " km";
            g.transform.GetChild(5).GetComponent<Text>().text = ruta.positiveElevation.ToString() + "m";
            g.transform.GetChild(7).GetComponent<Text>().text = ruta.negativeElevation.ToString() + "m";*/

            //g.GetComponent<Button>().AddEventListener(i, ItemClickedSelectRoute);

        }

        Destroy(buttonTemplate);
    }    
}
