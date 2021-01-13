using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateWorkoutList : MonoBehaviour
{
    static public bool afegit;
    public GameObject contentPanel;

    public GameObject buttPrefab;

    // Start is called before the first frame update
    public void EnsenyarLListaBlocs(List<Bloc> blocs)
    {
        float myWidth;
        //int listSize = RoutesManager.rutas.Count;
        int listSize = blocs.Count;
        if (listSize <= 10)
        {
            myWidth = 1365;
            Debug.Log(myWidth);
        }
        else
        {
            myWidth = listSize * buttPrefab.GetComponent<RectTransform>().sizeDelta.x + 10 * listSize;
        }
        contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, myWidth);
        contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 108);
        //GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < listSize; i++)
        {
            //Ruta ruta = RoutesManager.rutas[i];
            g = Instantiate(buttPrefab, transform);
            /*g.transform.GetChild(1).GetComponent<Text>().text = ruta.name;
            g.transform.GetChild(3).GetComponent<Text>().text = ruta.totalDistance.ToString() + " km";
            g.transform.GetChild(5).GetComponent<Text>().text = ruta.positiveElevation.ToString() + "m";
            g.transform.GetChild(7).GetComponent<Text>().text = ruta.negativeElevation.ToString() + "m";*/

            //g.GetComponent<Button>().AddEventListener(i, ItemClickedSelectRoute);

        }

        //Destroy(buttonTemplate);
    }

    public void DestroyList()
    {
        for (int i = 1; i < contentPanel.transform.childCount; i++)
        {
            GameObject.Destroy(contentPanel.transform.GetChild(i).gameObject);
        }
    }
}
