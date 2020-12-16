using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PairDevices : MonoBehaviour
{

    public Dropdown dropdownHeartSensor;
    public Text TextBoxHeartSensor;
    private int sizeHeartSensor;
    private GameObject heartSensorDisplayObject;
    private HeartRateDisplay heartRateDisplay;

    public Dropdown dropdownSpeedCadence;
    public Text TextBoxSpeedCadence;
    private int sizeSpeedCadence;
    private GameObject speedCadenceDisplayObject;
    private SpeedCadenceDisplay speedCadenceDisplay;

    // Start is called before the first frame update
    void Start()
    {

        TextBoxHeartSensor.text = "";

        //Agafem el sensor de HR
        if (GameObject.Find("HeartRateDisplay"))
        {
            heartSensorDisplayObject = GameObject.Find("HeartRateDisplay");
            heartRateDisplay = (HeartRateDisplay)heartSensorDisplayObject.GetComponent(typeof(HeartRateDisplay));
        }

        if (GameObject.Find("CadenceDisplay"))
        {
            speedCadenceDisplayObject = GameObject.Find("SpeedCadenceDisplay");
            speedCadenceDisplay = (SpeedCadenceDisplay)speedCadenceDisplayObject.GetComponent(typeof(SpeedCadenceDisplay));
        }



        sizeHeartSensor = 0;
        sizeSpeedCadence = 0;
       
        dropdownHeartSensor.options.Clear();
        dropdownHeartSensor.onValueChanged.AddListener(delegate { HeartDisplaySelected(dropdownHeartSensor); });
        //dropdownHeartSensor.options.Add(new Dropdown.OptionData() { text = "Hola" });


        dropdownSpeedCadence.options.Clear();
        dropdownSpeedCadence.onValueChanged.AddListener(delegate { SpeedCadenceSelected(dropdownSpeedCadence); });

        updateListHr();
        updateListSpeedCadenceDisplay();
    }

    void HeartDisplaySelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        TextBoxHeartSensor.text = dropdown.options[index].text;
        Debug.Log("Selected HeartMonitor" + index);


        //Agafem l'objecte HeartDisplay i utilitzem la funció ConnectToDevice per connectar el sensor de FC seleccionat per l'usuari
        if (heartSensorDisplayObject != null)
        {
            heartRateDisplay.ConnectToDevice(HeartRateDisplay.scanResult[index]);
        } else {
            Debug.LogError("ERROR: No s'ha trobat heartRateDisplay Objecte Prefab (SelectDevice) ");
        }
     }

    void SpeedCadenceSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        TextBoxSpeedCadence.text = dropdown.options[index].text;
        Debug.Log("Selected SpeedCadence: " + index);


        //Agafem l'objecte HeartDisplay i utilitzem la funció ConnectToDevice per connectar el sensor de FC seleccionat per l'usuari
        if (speedCadenceDisplayObject != null)
        {
            speedCadenceDisplay.ConnectToDevice(SpeedCadenceDisplay.scanResult[index]);
        }
        else
        {
            Debug.LogError("ERROR: No s'ha trobat speedCadence Objecte Prefab (SelectDevice) ");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (HeartRateDisplay.scanResult != null)
        {
            if (sizeHeartSensor != HeartRateDisplay.scanResult.Count)
            {
                updateListHr();
            }
        }

        if (SpeedCadenceDisplay.scanResult != null)
        {
            if (sizeHeartSensor != SpeedCadenceDisplay.scanResult.Count)
            {
                updateListSpeedCadenceDisplay();
            }
        }
    }

    void updateListHr() {

        sizeHeartSensor = 0;

        if (HeartRateDisplay.scanResult != null)
        {
            foreach (var heartMonitor in HeartRateDisplay.scanResult)
            {
                dropdownHeartSensor.options.Add(new Dropdown.OptionData() { text = heartMonitor.ToString() });
                sizeHeartSensor++;
            }
        }

        if (HeartRateDisplay.scanResult.Count == 1)
        {
            dropdownHeartSensor.value = 1;
            HeartDisplaySelected(dropdownHeartSensor);
        }
    }

    void updateListSpeedCadenceDisplay()
    {

        sizeSpeedCadence = 0;

        if (SpeedCadenceDisplay.scanResult != null)
        {
            foreach (var speedCadence in SpeedCadenceDisplay.scanResult)
            {
                dropdownSpeedCadence.options.Add(new Dropdown.OptionData() { text = speedCadence.ToString() });
                sizeSpeedCadence++;
            }
        }

        if (SpeedCadenceDisplay.scanResult.Count == 1)
        {
            dropdownSpeedCadence.value = 1;
            SpeedCadenceSelected(dropdownSpeedCadence);
        }
    }


    public void changeTest()
    {
        SceneManager.LoadScene(5);
    }
}
