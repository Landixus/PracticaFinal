using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public static class ButtonExtension {
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick) {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}
public class ListGenerator : MonoBehaviour
{
    public int selectedIndex;
    public GameObject contentPanel;
    // Start is called before the first frame update
    void Start()
    {
        float myHeight = 0;
        int listSize = RoutesManager.rutas.Count;
        if (listSize <= 4)
        {
            myHeight = 400f;
        } else
        {
            myHeight = listSize * transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y + 10 * listSize;
        }
        contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, myHeight);
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < listSize; i++)
        {
            Ruta ruta = RoutesManager.rutas[i];
            g = Instantiate(buttonTemplate, transform);
            g.transform.GetChild(1).GetComponent<Text>().text = ruta.name + " km";
            g.transform.GetChild(3).GetComponent<Text>().text = ruta.totalDistance.ToString();
            g.transform.GetChild(5).GetComponent<Text>().text = ruta.positiveElevation.ToString() + "m";
            g.transform.GetChild(7).GetComponent<Text>().text = ruta.negativeElevation.ToString() + "m";

            g.GetComponent<Button>().AddEventListener(i, ItemClicked);

            if (i == selectedIndex)
            {

            }
        }

        Destroy(buttonTemplate);
    }

    private void ItemClicked(int i)
    {
        Debug.Log("item " +i+ " clicked");
        selectedIndex = i;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
