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


    // Start is called before the first frame update
    void Start()
    {
        workouts = PaginaPrincipal.user.workouts;

        float myHeight = 0;
        int listSize = workouts.Count ;
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
            g.transform.GetChild(5).GetComponent<Text>().text = listSize.ToString() + " blocs";
            

            //g.GetComponent<Button>().AddEventListener(i, ItemClickedSelectRoute);

        }

       /*if (afegit)
        {
            afegitText.text = "S'ha afegit la ruta a la llista";
        }*/

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
