using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorkoutListGenerator : MonoBehaviour
{

    static public bool afegit;
    public Text afegitText;
    public GameObject contentPanel;

    private List<Workout> workouts;

    [SerializeField] Text workoutName;
    [SerializeField] Text descripcio;
    [SerializeField] Text temps;
    [SerializeField] Text numBlocs;

    [SerializeField] GameObject blocPanel;

    [SerializeField] Text listHelper;

    // Start is called before the first frame update
    void Start()
    {
        workouts = PaginaPrincipal.user.workouts;


        float myHeight = 0;
        int listSize = workouts.Count ;

        if (listSize == 0)
        {
            listHelper.text = "No hi han Workouts  creats";
        } else
        {
            listHelper.text = "";
        }

        if (listSize <= 4)
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
            Workout workout = workouts[i];
            g = Instantiate(buttonTemplate, transform);
            g.transform.GetChild(1).GetComponent<Text>().text = workout.name;
            g.transform.GetChild(3).GetComponent<Text>().text = FromSecondsToMinutesString(workout.tempsTotal) + " minuts";
            g.transform.GetChild(5).GetComponent<Text>().text = workout.blocs.Count + " blocs";
            

            g.GetComponent<Button>().AddEventListener(i, RouteClicked);

        }

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

        Workout workout = workouts[i];

        workoutName.text = workout.name;
        descripcio.text = workout.description;
        numBlocs.text = workout.blocs.Count.ToString();
        temps.text = FromSecondsToMinutesString(workout.tempsTotal);


        //Crear llista de blocs no interectuable

        createBlocList(workout);

        FollowRoute.workout = workout;

        Debug.Log("item " + i + " clicked");
    }

    private void createBlocList(Workout workout) {

        //Destroy antiga llista
        for (int i = 1; i < blocPanel.transform.childCount; i++)
        {
            GameObject.Destroy(blocPanel.transform.GetChild(i).gameObject);
        }


        var listBlocsSize = workout.blocs.Count;
        float myWidth;

        if (listBlocsSize < 7)
        {
            myWidth = 735;
        }
        else { 
            myWidth = listBlocsSize * blocPanel.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x + 10 * listBlocsSize;
        }

        blocPanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, myWidth);
        GameObject buttonTemplate = blocPanel.transform.GetChild(0).gameObject;
        GameObject g;

        for (int i = 0; i < listBlocsSize; i++)
        {
            Bloc bloc = workout.blocs[i];
            g = Instantiate(buttonTemplate, blocPanel.transform);
            g.transform.GetChild(1).GetComponent<Text>().text = bloc.numBloc.ToString();
            g.transform.GetChild(3).GetComponent<Text>().text = FromSecondsToMinutesString(bloc.temps) + " minuts";
            g.transform.GetChild(5).GetComponent<Text>().text = bloc.pot + " W";
            g.SetActive(true);
        }

        Destroy(buttonTemplate);
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
