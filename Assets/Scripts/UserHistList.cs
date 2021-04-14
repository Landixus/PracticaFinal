using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserHistList : MonoBehaviour
{
    static public bool afegit;

    public GameObject contentPanel;

    private List<Session> sessions;

    [SerializeField] Text routeName;
    [SerializeField] Text workoutName;
    [SerializeField] Text duracioSession;
    [SerializeField] Text dataSession;

    [SerializeField] Text listHelper;

    [SerializeField] Text SpeedMax;
    [SerializeField] Text SpeedAvg;

    [SerializeField] Text FCMax;
    [SerializeField] Text FCAvg;

    [SerializeField] Text WMax;
    [SerializeField] Text WAvg;

    [SerializeField] Text RpmMax;
    [SerializeField] Text RpmAvg;

    [SerializeField] Button FCButton;
    [SerializeField] Button WButton;
    [SerializeField] Button RPMButton;
    [SerializeField] Button SpeedButton;

    //[SerializeField] RectTransform graphContainer;

    private int numPoints;
    private int totalPoints;
    private RectTransform graphContainer;

    private Session sessionSelected;
    [SerializeField] Text durText;
    [SerializeField] GameObject graphPanel;

    [SerializeField] Text maxText;

    // Start is called before the first frame update
    void Start()
    {
        sessions = PaginaPrincipal.user.historial;


        float myHeight = 0;
        int listSize = sessions.Count;

        if (listSize == 0)
        {
            listHelper.text = "No s'ha guardat cap Entrenament";
        }
        else
        {
            listHelper.text = "";
        }

        if (listSize < 4)
        {
            myHeight = 430f;
        }
        else
        {
            myHeight = listSize * transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.y + 10 * listSize;
        }
        contentPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, myHeight);
        GameObject buttonTemplate = transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < listSize; i++)
        {

            Session session = sessions[i];
            g = Instantiate(buttonTemplate, transform);
            g.transform.GetChild(1).GetComponent<Text>().text = session.ruta.name;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");
            //dateToDisplay.ToString(formatSpecifier, culture));
            //https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=net-5.0

            g.transform.GetChild(3).GetComponent<Text>().text = session.data.ToString("G", culture);

            g.transform.GetChild(5).GetComponent<Text>().text = FromSecondsToMinutesString((int)session.tempsTotal);

            if (session.workout != null)
            {
                g.transform.GetChild(7).GetComponent<Text>().text = "Sí";
            }
            else {
                g.transform.GetChild(7).GetComponent<Text>().text = "No";
            }
            


            g.GetComponent<Button>().AddEventListener(i, RouteClicked);

        }
        buttonTemplate.transform.parent = null;
        Destroy(buttonTemplate);

        FCButton.gameObject.SetActive(false);
        WButton.gameObject.SetActive(false);
        RPMButton.gameObject.SetActive(false);
        SpeedButton.gameObject.SetActive(false);


        graphContainer = graphPanel.GetComponent<RectTransform>();
        maxText.text = "";
    }

    private string FromSecondsToMinutesString(int totalSeconds)
    {

        if (totalSeconds == 0)
        {
            return "00:00";
        }
        float total = (float)totalSeconds / 60;

        int minutes = (int)total;

        Debug.Log(total);

        var seconds = (total - Math.Truncate(total)) * 30 / 0.5;

        seconds = (float)Math.Round(seconds, 3);

        Debug.Log(seconds);

        string secondsString;
        if (seconds < 10)
        {
            secondsString = "0" + seconds.ToString();
        }
        else
        {
            secondsString = seconds.ToString();
        }

        return minutes.ToString() + ":" + secondsString;
    }


    private void RouteClicked(int i)
    {
        FCButton.gameObject.SetActive(false);
        WButton.gameObject.SetActive(false);
        RPMButton.gameObject.SetActive(false);
        SpeedButton.gameObject.SetActive(false);

       

        GameObject buttonTemplate;
        Button button;

        buttonTemplate = transform.GetChild(i).gameObject;
        button = buttonTemplate.GetComponent<Button>();
        button.Select();

        Session session = sessions[i];

        sessionSelected = session;

        if (session.workout != null)
        {
            workoutName.text = session.workout.name;
        } else {
            workoutName.text = "No s'ha seguit cap workout";
        }

        duracioSession.text = FromSecondsToMinutesString((int)session.tempsTotal);

        CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");
        //dateToDisplay.ToString(formatSpecifier, culture));
        //https://docs.microsoft.com/en-us/dotnet/api/system.datetime.tostring?view=net-5.0

        dataSession.text = session.data.ToString("G", culture);

        if (session.speedMax == -1)
        {
            SpeedMax.text = "--";
            SpeedAvg.text = "--";
        }
        else {
            SpeedMax.text = session.speedMax.ToString("0.00");
            SpeedAvg.text = session.speedAvg.ToString("0.00");
            SpeedButton.gameObject.SetActive(true);
        }

        if (session.fcMax == -1)
        {
            FCMax.text = "--";
            FCAvg.text = "--";
        }
        else {
            FCMax.text = session.fcMax.ToString("0.0");
            FCAvg.text = session.fcAvg.ToString("0.0");
            FCButton.gameObject.SetActive(true);
        }

        if (session.powerMax == -1)
        {
            WMax.text = "--";
            WAvg.text = "--";
        }
        else
        {
            WMax.text = session.powerMax.ToString("0.0");
            WAvg.text = session.powerAvg.ToString("0.0");
            WButton.gameObject.SetActive(true);
        }

        if (session.rpmMax == -1)
        {
            RpmMax.text = "--";
            RpmAvg.text = "--";
        }
        else
        {
            RpmMax.text = session.rpmMax.ToString("0.0");
            RpmAvg.text = session.rpmAvg.ToString("0.0");
            RPMButton.gameObject.SetActive(true);
        }

        Debug.Log("item " + i + " clicked");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoBack()
    {
        SceneManager.LoadScene(8);
    }

    public void GoToMainPage()
    {
        SceneManager.LoadScene(2);
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


    public void ShowGraph(List<int> valueList, int option)
    {
        List<int> simpleList = SimplifyList(valueList, 200);

        //Debug.Log(simpleList.Count);

        durText.text = FromSecondsToMinutesString((int)sessionSelected.tempsTotal);

        float graphHight = graphContainer.sizeDelta.y;
        //float xSize = 1f;

        float maxNum = 0;
        float minNum = 0;

        Color color;

        //El + 100 es per tenir un marge per adalt
        float yMaximum;

        switch (option)
        {
            case 1:
                maxNum = sessionSelected.fcMax;
                yMaximum = maxNum + 100;
                maxText.text = maxNum.ToString();
                color = Color.red;
                break;
            case 2:
                maxNum = sessionSelected.powerMax;
                yMaximum = maxNum + 100;
                maxText.text = maxNum.ToString();
                color = Color.yellow;
                break;
            case 3:
                maxNum = sessionSelected.rpmMax;
                maxText.text = maxNum.ToString();
                yMaximum = maxNum + 100;
                color = Color.green;
                break;
            default:
                color = Color.red;
                yMaximum = 100;
                break;
        }

       
        

        //maxEleText.text = Math.Round(maxEle, 0).ToString() + "m";

        float minCorrection = 0;

        if (minNum > 400)
        {
            //Posem -100 per que no estigui enganxat a baix de tot;
            minCorrection = - 100;
            //minEleText.text = Math.Round(minEle, 0).ToString() + "m";
        }
        else
        {
            //minEleText.text = "0m";
        }

        float xSize = CalcXDiff(simpleList);

        GameObject lastCircleGameObject = null;

        for (int i = 0; i < simpleList.Count; i++)
        {
            float xPosition = xSize + i * xSize;
            float yPosition = (simpleList[i] - minCorrection) / yMaximum * graphHight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null)
            {
                Debug.Log("simpleList Count:" + simpleList.Count);

                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, color);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Color color)
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

        gameObject.GetComponent<Image>().color = color;
        rectTransform.localEulerAngles = new Vector3(0, 0, angle);

    }

    //Funció que serveix per simplificar la llista de punts d'altura per que el gràfic càpiga en el contenidor
    private List<int> SimplifyList(List<int> list, int expectedPoints)
    {
        float graphWidth = graphContainer.sizeDelta.x;

        Debug.Log("Width:" + graphWidth);
        Debug.Log("Num total points:" + list.Count);

        //int numPoints = (int)Math.Ceiling(list.Count / (graphWidth - 20));
        numPoints = list.Count / expectedPoints;
        Debug.Log("Num points:" + numPoints);

        //Si hi han menys punts que l'amplada del contenidor del gràfic retornem la llista sense modificar-la
        if (numPoints == 0)
        {
            totalPoints = list.Count;
            return list;
        }

        List<int> simpleList = new List<int>();

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

    private float CalcXDiff(List<int> simpleList)
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

    public void showSpeedGraph()
    {
        List<int> intSpeedList = sessionSelected.fcList.ConvertAll(x => (int)x);
        ShowGraph(intSpeedList, 0);
        graphPanel.SetActive(true);
        Debug.Log("Apreto ShowSpeeGraph");
    }

    public void showFCGraph()
    {
        List<int> intFCList = sessionSelected.fcList.ConvertAll(x => (int)x);
        ShowGraph(intFCList,1);
        graphPanel.SetActive(true);
        Debug.Log("Apreto showFCGraph");
    }

    public void showPowerGraph()
    {
        ShowGraph(sessionSelected.powerList, 2);
        graphPanel.SetActive(true);
        Debug.Log("Apreto showPowerGraph");
    }

    public void showRPMGraph()
    { 
        ShowGraph(sessionSelected.rpmList, 3);
        graphPanel.SetActive(true);
        Debug.Log("Apreto showRPMGraph");
    }

    public void closeGraphPanel()
    {
        //Netejar gràfic
        graphPanel.SetActive(false);
    }
}
