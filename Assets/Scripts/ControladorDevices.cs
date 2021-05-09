using ANT_Managed_Library;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDevices : MonoBehaviour
{

    private GameObject heartSensorDisplayObject;
    private HeartRateDisplay heartRateDisplay;

    private GameObject speedCadenceDisplayObject;
    private SpeedCadenceDisplay speedCadenceDisplay;

    private GameObject cadenceDisplayObject;
    private CadenceDisplay cadenceDisplay;

    private GameObject trainerDisplayObject;
    private FitnessEquipmentDisplay trainerDisplay;

    private GameObject powerDisplayObject;
    private PowerMeterDisplay powerDisplay;

    private bool connectedUSB;

    int frames = 0;

    // Start is called before the first frame update
    void Start()
    {
        ConnectDevices();

    }

    private void ConnectDevices()
    {
        try
        {
            //Busquem si hi han els objectes amb un nom concret en l'escena
            //Si estan diem que no desapareguin al canviar d'escena per poder tenir els controladors
            //Fem que començin a buscar 
            if (GameObject.Find("CadenceDisplay"))
            {
                cadenceDisplayObject = GameObject.Find("CadenceDisplay");
                cadenceDisplay = (CadenceDisplay)cadenceDisplayObject.GetComponent(typeof(CadenceDisplay));

                //Posem autoCOnnectToFirstSensor a false ja que no volem que es connecti al primer aparell que trobi
                //En principi ja està a fals en l'script pero ens assagurem que sigui aixi
                cadenceDisplay.autoConnectToFirstSensorFound = false;
                cadenceDisplay.StartScan();
                DontDestroyOnLoad(GameObject.Find("CadenceDisplay"));
            }
            if (GameObject.Find("SpeedCadenceDisplay"))
            {
                speedCadenceDisplayObject = GameObject.Find("SpeedCadenceDisplay");
                speedCadenceDisplay = (SpeedCadenceDisplay)speedCadenceDisplayObject.GetComponent(typeof(SpeedCadenceDisplay));
                speedCadenceDisplay.autoConnectToFirstSensorFound = false;
                speedCadenceDisplay.StartScan();
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
                powerDisplayObject = GameObject.Find("PowerMeterDisplay");
                powerDisplay = (PowerMeterDisplay)powerDisplayObject.GetComponent(typeof(PowerMeterDisplay));
                powerDisplay.autoConnectToFirstSensorFound = false;
                powerDisplay.StartScan();
                DontDestroyOnLoad(GameObject.Find("PowerMeterDisplay"));
            }
            if (GameObject.Find("FitnessEquipmentDisplay"))
            {
                trainerDisplayObject = GameObject.Find("FitnessEquipmentDisplay");
                trainerDisplay = (FitnessEquipmentDisplay)trainerDisplayObject.GetComponent(typeof(FitnessEquipmentDisplay));
                trainerDisplay.autoConnectToFirstSensorFound = false;
                trainerDisplay.StartScan();
                DontDestroyOnLoad(GameObject.Find("FitnessEquipmentDisplay"));
            }
            PaginaPrincipal.haveUSB = true;
            connectedUSB = true;
        }
        catch (ANT_Exception e)
        {
            PaginaPrincipal.haveUSB = false;
            connectedUSB = false;
            //Debug.Log(e.StackTrace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!connectedUSB)
        {
            if (frames == 10)
            {
                frames = 0;
                // call method 
                ConnectDevices();
            }
            frames++;
        }
    }
}
