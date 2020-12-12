using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectDevice : MonoBehaviour
{

    public Dropdown dropdownHeartSensor;
    public Text TextBoxHeartSensor;
    private int sizeHeartSensor;
    

    // Start is called before the first frame update
    void Start()
    {

        TextBoxHeartSensor.text = "";

        //Carguem la scena on hi han els prefabs de manera aditiva
        SceneManager.LoadScene(3, LoadSceneMode.Additive);

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

       

        //Agafar sensor amb HeartRateDisplay.scanResult[index] i guarde-lo en un script amb una variable static per poder-la instanciar en qualsevol lloc
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
