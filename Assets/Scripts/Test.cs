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
        if (GameObject.Find("CadenceDisplay"))
        {
            uiText.text = "Cadence sensor - is connected ? " + GameObject.Find("CadenceDisplay").GetComponent<CadenceDisplay>().connected + "\n";
            uiText.text += "cadence: " + GameObject.Find("CadenceDisplay").GetComponent<CadenceDisplay>().cadence + "\n";
            uiText.text += "----------------------------------------------\n";
        }
        if (GameObject.Find("SpeedCadenceDisplay"))
        {
            uiText.text += "SpeedCadence sensor - is connected ? " + GameObject.Find("SpeedCadenceDisplay").GetComponent<SpeedCadenceDisplay>().connected + "\n";
            uiText.text += "cadence = " + GameObject.Find("SpeedCadenceDisplay").GetComponent<SpeedCadenceDisplay>().cadence + "\n";
            uiText.text += "speed = " + GameObject.Find("SpeedCadenceDisplay").GetComponent<SpeedCadenceDisplay>().speed + "\n";
            uiText.text += "distance = " + GameObject.Find("SpeedCadenceDisplay").GetComponent<SpeedCadenceDisplay>().distance + "\n";
            uiText.text += "----------------------------------------------\n";
        }
        if (GameObject.Find("HeartRateDisplay"))
        {
            uiText.text += "Heart rate sensor - is connected ? " + GameObject.Find("HeartRateDisplay").GetComponent<HeartRateDisplay>().connected + "\n";
            uiText.text += "HR = " + GameObject.Find("HeartRateDisplay").GetComponent<HeartRateDisplay>().heartRate + "\n";
            uiText.text += "----------------------------------------------\n";
        }
        if (GameObject.Find("SpeedDisplay"))
        {
            uiText.text += "Speed sensor - is connected ? " + GameObject.Find("SpeedDisplay").GetComponent<SpeedDisplay>().connected + "\n";
            uiText.text += "speed = " + GameObject.Find("SpeedDisplay").GetComponent<SpeedDisplay>().speed + "\n";
            uiText.text += "distance = " + GameObject.Find("SpeedDisplay").GetComponent<SpeedDisplay>().distance + "\n";
            uiText.text += "----------------------------------------------\n";
        }
        if (GameObject.Find("PowerMeterDisplay"))
        {
            uiText.text += "Power sensor - is connected ? " + GameObject.Find("PowerMeterDisplay").GetComponent<PowerMeterDisplay>().connected + "\n";
            uiText.text += "power = " + GameObject.Find("PowerMeterDisplay").GetComponent<PowerMeterDisplay>().instantaneousPower + "\n";
            uiText.text += "cadence = " + GameObject.Find("PowerMeterDisplay").GetComponent<PowerMeterDisplay>().instantaneousCadence + "\n";

            uiText.text += "----------------------------------------------\n";
        }
        if (GameObject.Find("FitnessEquipmentDisplay"))
        {
            //BikeTrainerButtons.SetActive(true);
            uiText.text += "Fitness E: - is connected ? " + GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>().connected + "\n";
            uiText.text += "power = " + GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>().instantaneousPower + "\n";
            uiText.text += "speed= " + GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>().speed + "\n";
            uiText.text += "elapsedTime= " + GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>().elapsedTime + "\n";
            uiText.text += "heartRate= " + GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>().heartRate + "\n";
            uiText.text += "distanceTraveled= " + GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>().distanceTraveled + "\n";
            uiText.text += "cadence= " + GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>().cadence + "\n";

            uiText.text += "----------------------------------------------\n";
        }
    }
}
