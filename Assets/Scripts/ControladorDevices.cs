using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDevices : MonoBehaviour
{

    public GameObject heartSensorDisplayObject;
    private HeartRateDisplay heartRateDisplay;


    // Start is called before the first frame update
    void Start()
    {
            //Busquem si hi han els objectes amb un nom concret en l'escena
            //Si estan diem que no desapareguin al canviar d'escena per poder tenir els controladors
            //Fem que començin a buscar 
            if (GameObject.Find("CadenceDisplay"))
            {
                DontDestroyOnLoad(GameObject.Find("CadenceDisplay"));
            }
            if (GameObject.Find("SpeedCadenceDisplay"))
            {
                DontDestroyOnLoad(GameObject.Find("SpeedCadenceDisplay"));
            }
            if (GameObject.Find("HeartRateDisplay"))
            {
                heartSensorDisplayObject = GameObject.Find("HeartRateDisplay");
                heartRateDisplay = (HeartRateDisplay)heartSensorDisplayObject.GetComponent(typeof(HeartRateDisplay));
                heartRateDisplay.autoConnectToFirstSensorFound = false;
                heartRateDisplay.StartScan();
                DontDestroyOnLoad(GameObject.Find("HeartRateDisplay"));
            }
            if (GameObject.Find("SpeedDisplay"))
            {
                DontDestroyOnLoad(GameObject.Find("SpeedDisplay"));
            }
            if (GameObject.Find("PowerMeterDisplay"))
            {
                DontDestroyOnLoad(GameObject.Find("PowerMeterDisplay"));
            }
            if (GameObject.Find("FitnessEquipmentDisplay"))
            {
                DontDestroyOnLoad(GameObject.Find("FitnessEquipmentDisplay"));
            }
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
