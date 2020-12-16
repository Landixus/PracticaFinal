using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectDevices : MonoBehaviour
{

    public Dropdown dropdownHeartSensor;
    public Text TextBoxHeartSensor;
    private int sizeHeartSensor;
    public GameObject heartSensorDisplayObject;
    private HeartRateDisplay heartRateDisplay;

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




        sizeHeartSensor = 0;
       
        dropdownHeartSensor.options.Clear();
        dropdownHeartSensor.onValueChanged.AddListener(delegate { DropdownItemSelected(dropdownHeartSensor); });
        //dropdownHeartSensor.options.Add(new Dropdown.OptionData() { text = "Hola" });
        updateList();

      
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        TextBoxHeartSensor.text = dropdown.options[index].text;
        Debug.Log("Selected" + index);


        //Agafem l'objecte HeartDisplay i utilitzem la funció ConnectToDevice per connectar el sensor de FC seleccionat per l'usuari
        if (heartSensorDisplayObject != null)
        {
            heartRateDisplay.ConnectToDevice(HeartRateDisplay.scanResult[index]);
        } else {
            Debug.LogError("ERROR: No s'ha trobat heartRateDisplay Objecte Prefab (SelectDevice) ");
        }
     }


    // Update is called once per frame
    void Update()
    {
        if (HeartRateDisplay.scanResult != null)
        {
            if (sizeHeartSensor != HeartRateDisplay.scanResult.Count)
            {
                updateList();
            }
        }
        
    }

    void updateList() {

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
            DropdownItemSelected(dropdownHeartSensor);
        }
    }


    public void changeTest()
    {
        SceneManager.LoadScene(5);
    }
}
