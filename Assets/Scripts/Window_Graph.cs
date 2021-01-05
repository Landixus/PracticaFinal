using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour
{

    private RectTransform graphContainer;
    [SerializeField] private Sprite circleSprite;

    [SerializeField] private Text maxEleText;
    [SerializeField] private Text minEleText;
    [SerializeField] private Text distanceText;

    private int numPoints;
    private int totalPoints;

    private void Awake()
    {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        numPoints = 0;

        //double[] valueList = new double[] { 5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        //ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) 
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        //gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.GetComponent<Image>().sprite = null;
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        return gameObject;
    }

    //Funció modificada de https://www.youtube.com/watch?v=CmU5-v-v1Qo
    public void ShowGraph(List<TrackPoint> valueList, float[] slopes, float distance)
    {
        List<TrackPoint> simpleList = SimplifyList(valueList);
        List<float> simpleSlopes = SimplifySlope(slopes);

        //Debug.Log(simpleList.Count);

        distanceText.text = distance + " Km";

        float graphHight = graphContainer.sizeDelta.y;
        //float xSize = 1f;

        float maxEle = FindMaxElevation(simpleList);
        float minEle = FindMinElevation(simpleList);
        //El + 100 es per tenir un marge per adalt
        float yMaximum = maxEle + 100;

        maxEleText.text = Math.Round(maxEle,0).ToString() + "m";
        
        float minCorrection = 0;

        if (minEle > 400)
        {
            //Posem -30 per que no estigui enganxat a baix de tot;
            minCorrection = minEle - 100;
            minEleText.text = Math.Round(minEle, 0).ToString() + "m";
        }
        else {
            minEleText.text ="0m";
        }

        float xSize = CalcXDiff(simpleList);

        GameObject lastCircleGameObject = null;

        //Valor differencia per si tenim més punts en una llista que en una altre;
        //Pot passar ja que al fer el módul i tenir llistes originals de diferents mides
        //podem acabar amb subllistes de diferents mides
        int diff = simpleList.Count - simpleSlopes.Count;
        if (diff < 0)
        {
            diff = 0;
        }
        Debug.Log("Diff:" + diff);
        for (int i = 0; i < simpleList.Count - diff; i++)
        {
            float xPosition = xSize+ i * xSize;
            float yPosition = ((simpleList[i].ele-minCorrection) / yMaximum) * graphHight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null) 
            {
                Debug.Log("simpleList Count:" + simpleList.Count);
                Debug.Log("simpleSlopes Count:" + simpleSlopes.Count);
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, simpleSlopes[i]);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, float slope) 
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        //gameObject.GetComponent<Image>().color = new Color(1, 1, 1);

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 2f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);

        gameObject.GetComponent<Image>().color = MapRainbowColor(slope);
        rectTransform.localEulerAngles = new Vector3(0, 0, angle);

    }

    public float FindMaxElevation(List<TrackPoint> list)
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("Empty list");
        }
        float maxEle = int.MinValue;
        foreach (TrackPoint type in list)
        {
            if (type.ele > maxEle)
            {
                maxEle = type.ele;
            }
        }
        return maxEle;
    }

    public float FindMinElevation(List<TrackPoint> list)
    {
        if (list.Count == 0)
        {
            throw new InvalidOperationException("Empty list");
        }
        float minEle = int.MaxValue;
        foreach (TrackPoint type in list)
        {
            if (type.ele < minEle)
            {
                minEle = type.ele;
            }
        }
        return minEle;
    }

    //Funció que serveix per simplificar la llista de punts d'altura per que el gràfic càpiga en el contenidor
    private List<TrackPoint> SimplifyList(List<TrackPoint> list)
    {
        float graphWidth = graphContainer.sizeDelta.x;

        Debug.Log("Width:" + graphWidth);
        Debug.Log("Num total points:" + list.Count);

        //int numPoints = (int)Math.Ceiling(list.Count / (graphWidth - 20));
        numPoints = list.Count / 150;
        Debug.Log("Num points:" + numPoints);

        //Si hi han menys punts que l'amplada del contenidor del gràfic retornem la llista sense modificar-la
        if (numPoints == 0)
        {
            totalPoints = list.Count;
            return list;
        }

        List<TrackPoint> simpleList = new List<TrackPoint>();

        for (int i = 0; i < list.Count; i++)
        {
            if (i % numPoints == 0)
            {
                simpleList.Add(list[i]);
            }
        }

        totalPoints = simpleList.Count;

        return simpleList;
    }

    private List<float> SimplifySlope(float[] slopes)
    {

        List<float> simplifiedSlope = new List<float>();

        if (numPoints == 0)
        {
            return slopes.ToList<float>();
        }

        for (int i = 0; i < slopes.Length; i++)
        {
            if (i % numPoints == 0)
            {
               
                simplifiedSlope.Add(slopes[i]);
            }
        }

        return simplifiedSlope;
    }


    private float CalcXDiff(List<TrackPoint> simpleList)
    {
        float graphWidth = graphContainer.sizeDelta.x;

        if (simpleList.Count < graphWidth - 20)
        {
            return graphWidth / simpleList.Count;
        }
        else 
        {
            return 1f;
        }
    }

    private Color MapRainbowColor(float value)
    {
        //value *= 0.33f; 

        int red_value = 30;
        int blue_value = -30;
        // Convert into a value between 0 and 1023.
        int int_value = (int)(1023 * (value - red_value) /
            (blue_value - red_value));

        //Debug.Log("%:" + value + " valor:" + int_value);

        // Map different color bands.
        if (int_value < 256)
        {
            
            // Black (255,255,255).
            return new Color(0,0, 0);
        }
        else if (int_value < 341)
        {
            // Red (255, 0, 0)
            int_value -= 171;
            return new Color(255, 0, 0);
        }
        else if (int_value < 426)
        {
            // Yellow (255, 255, 0).
            int_value -= 257;
            return new Color(255, 255, 0);
        }
        else if (int_value < 516)
        {
            // green  (0, 255, 0.
            int_value -= 365;
            return new Color(0, 255, 0);
        }
        else
        {
            // Aqua to white. (0, 255, 255) to (0, 0, 255).
            int_value -= 768;
            return new Color(0, 255, 255);
        }
    }
    //Funció per borrar tots els punts creats pel ShowGraph menys el primer element ja que es el background
    public void DestroyGraph()
    {
        for (int i = 1; i < graphContainer.childCount; i++)
        {
            GameObject.Destroy(graphContainer.GetChild(i).gameObject);
        }
    }
}
