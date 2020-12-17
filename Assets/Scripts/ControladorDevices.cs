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


    // Start is called before the first frame update
    void Start()
    {
            //Busquem si hi han els objectes amb un nom concret en l'escena
            //Si estan diem que no desapareguin al canviar d'escena per poder tenir els controladors
            //Fem que començin a buscar 
            if (GameObject.Find("CadenceDisplay"))
            {
                cadenceDisplayObject = GameObject.Find("CadenceDisplay");
                cadenceDisplay = (CadenceDisplay)cadenceDisplayObject.GetComponent(typeof(CadenceDisplay));
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
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
