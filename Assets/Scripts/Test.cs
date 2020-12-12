using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    public Text uiText;
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = "";
        if (GameObject.Find("HeartRateDisplay"))
        {
            uiText.text += "Heart rate sensor - is connected ? " + GameObject.Find("HeartRateDisplay").GetComponent<HeartRateDisplay>().connected + "\n";
            uiText.text += "HR = " + GameObject.Find("HeartRateDisplay").GetComponent<HeartRateDisplay>().heartRate + "\n";
            uiText.text += "----------------------------------------------\n";
        }
    }
}
