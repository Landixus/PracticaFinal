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
    public Text afegitText;
    public GameObject contentPanel;

    private List<Session> sessions;

    [SerializeField] Text routeName;
    [SerializeField] Text workoutName;
    [SerializeField] Text duracioSession;
    [SerializeField] Text dataSession;

    [SerializeField] Text listHelper;

    [SerializeField] Text FCMax;
    [SerializeField] Text FCAvg;

    [SerializeField] Text WMax;
    [SerializeField] Text WAvg;

    [SerializeField] Text RpmMax;
    [SerializeField] Text RpmAvg;

    // Start is called before the first frame update
    void Start()
    {
        sessions = PaginaPrincipal.user.historial;


        float myHeight = 0;
        int listSize = sessions.Count;

        if (listSize == 0)
        {
            listHelper.text = "No hi han seguit cap Ruta";
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
            g.transform.GetChild(3).GetComponent<Text>().text = FromSecondsToMinutesString((int)session.tempsTotal);

            if (session.workout != null)
            {
                g.transform.GetChild(5).GetComponent<Text>().text = "Sí";
            }
            else {
                g.transform.GetChild(5).GetComponent<Text>().text = "No";
            }
            


            g.GetComponent<Button>().AddEventListener(i, RouteClicked);

        }
        buttonTemplate.transform.parent = null;
        Destroy(buttonTemplate);
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
        GameObject buttonTemplate;
        Button button;

        buttonTemplate = transform.GetChild(i).gameObject;
        button = buttonTemplate.GetComponent<Button>();
        button.Select();

        Session session = sessions[i];

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

        if (session.fcMax != -1)
        {
            FCMax.text = "--";
            FCAvg.text = "--";
        }
        else {
            FCMax.text = session.fcMax.ToString("0.0");
            FCAvg.text = session.fcAvg.ToString("0.0");
        }

        if (session.powerMax != -1)
        {
            WMax.text = "--";
            WAvg.text = "--";
        }
        else
        {
            WMax.text = session.powerMax.ToString("0.0");
            WAvg.text = session.powerAvg.ToString("0.0");
        }

        if (session.rpmMax != -1)
        {
            RpmMax.text = "--";
            RpmAvg.text = "--";
        }
        else
        {
            RpmMax.text = session.rpmMax.ToString("0.0");
            RpmAvg.text = session.rpmAvg.ToString("0.0");
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
}
