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

    public Dropdown dropdownCadence;
    public Text TextBoxCadence;
    private int sizeCadence;
    private GameObject cadenceDisplayObject;
    private CadenceDisplay cadenceDisplay;

    public Dropdown dropdownTrainer;
    public Text TextBoxTrainer;
    private int sizetrainerList;
    private GameObject trainerDisplayObject;
    private FitnessEquipmentDisplay trainerDisplay;

    public Dropdown dropdownPower;
    public Text TextBoxPower;
    private int sizePowerList;
    private GameObject powerDisplayObject;
    private PowerMeterDisplay powerDisplay;

    // Start is called before the first frame update
    void Start()
    {

        TextBoxHeartSensor.text = "";
        TextBoxCadence.text = "";
        TextBoxSpeedCadence.text = "";
        TextBoxTrainer.text = "";
        TextBoxPower.text = "";

        //Agafem el sensor de HR
        if (GameObject.Find("HeartRateDisplay"))
        {
            heartSensorDisplayObject = GameObject.Find("HeartRateDisplay");
            heartRateDisplay = (HeartRateDisplay)heartSensorDisplayObject.GetComponent(typeof(HeartRateDisplay));
        }

        if (GameObject.Find("SpeedCadenceDisplay"))
        {
            speedCadenceDisplayObject = GameObject.Find("SpeedCadenceDisplay");
            speedCadenceDisplay = (SpeedCadenceDisplay)speedCadenceDisplayObject.GetComponent(typeof(SpeedCadenceDisplay));
        }

        if (GameObject.Find("CadenceDisplay"))
        {
            cadenceDisplayObject = GameObject.Find("CadenceDisplay");
            cadenceDisplay = (CadenceDisplay)cadenceDisplayObject.GetComponent(typeof(CadenceDisplay));
        }

        if (GameObject.Find("FitnessEquipmentDisplay"))
        {
            trainerDisplayObject = GameObject.Find("FitnessEquipmentDisplay");
            trainerDisplay = (FitnessEquipmentDisplay)trainerDisplayObject.GetComponent(typeof(FitnessEquipmentDisplay));
        }

        if (GameObject.Find("PowerMeterDisplay"))
        {
            powerDisplayObject = GameObject.Find("PowerMeterDisplay");
            powerDisplay = (PowerMeterDisplay)powerDisplayObject.GetComponent(typeof(PowerMeterDisplay));
        }

        sizeHeartSensor = 0;
        sizeSpeedCadence = 0;
        sizeCadence = 0;
        sizetrainerList = 0;
       
        dropdownHeartSensor.options.Clear();
        dropdownHeartSensor.onValueChanged.AddListener(delegate { HeartDisplaySelected(dropdownHeartSensor); });
        //dropdownHeartSensor.options.Add(new Dropdown.OptionData() { text = "Hola" });

        dropdownSpeedCadence.options.Clear();
        dropdownSpeedCadence.onValueChanged.AddListener(delegate { SpeedCadenceSelected(dropdownSpeedCadence); });

        dropdownCadence.options.Clear();
        dropdownCadence.onValueChanged.AddListener(delegate { CadenceSelected(dropdownCadence); });

        dropdownTrainer.options.Clear();
        dropdownTrainer.onValueChanged.AddListener(delegate { TrainerSelected(dropdownTrainer); });

        dropdownPower.options.Clear();
        dropdownPower.onValueChanged.AddListener(delegate { PowerSelected(dropdownPower); });

        updateListHr();
        updateListSpeedCadenceDisplay();
        updateListCadence();
        updateListTrainer();
        updateListPower();
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

    void CadenceSelected(Dropdown dropdown)
    {

        int index = dropdown.value;
        TextBoxCadence.text = dropdown.options[index].text;
        Debug.Log("Selected Cadence: " + index);

        if (cadenceDisplayObject != null)
        {
            cadenceDisplay.ConnectToDevice(CadenceDisplay.scanResult[index]);
        }
        else
        {
            Debug.LogError("ERROR: No s'ha trobat Cadence Objecte Prefab (SelectDevice) ");
        }
    }

    void TrainerSelected(Dropdown dropdown)
    {

        int index = dropdown.value;
        TextBoxTrainer.text = dropdown.options[index].text;
        Debug.Log("Selected Trainer: " + index);

        if (trainerDisplayObject != null)
        {
            trainerDisplay.ConnectToDevice(FitnessEquipmentDisplay.scanResult[index]);
        }
        else
        {
            Debug.LogError("ERROR: No s'ha trobat FitnesEquipment Objecte Prefab (SelectDevice) ");
        }
    }

    void PowerSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        TextBoxPower.text = dropdown.options[index].text;
        Debug.Log("Selected Power Device: " + index);

        if (powerDisplayObject != null)
        {
            powerDisplay.ConnectToDevice(PowerMeterDisplay.scanResult[index]);
        }
        else
        {
            Debug.LogError("ERROR: No s'ha trobat PowerDevice Objecte Prefab (SelectDevice) ");
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
            if (sizeSpeedCadence != SpeedCadenceDisplay.scanResult.Count )
            {
                updateListSpeedCadenceDisplay();
            }
        }

        if (CadenceDisplay.scanResult != null)
        {
            if (sizeCadence != CadenceDisplay.scanResult.Count)
            {
                updateListCadence();
            }
        }

        if (FitnessEquipmentDisplay.scanResult != null)
        {
            if (sizetrainerList != FitnessEquipmentDisplay.scanResult.Count)
            {
                updateListTrainer();
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

        if (HeartRateDisplay.scanResult.Count == 1 && heartRateDisplay.connected == false)
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

        if (SpeedCadenceDisplay.scanResult.Count == 1 && speedCadenceDisplay.connected == false && cadenceDisplay.connected == false)
        {
            dropdownSpeedCadence.value = 1;
            SpeedCadenceSelected(dropdownSpeedCadence);
        }
    }

    public void updateListCadence()
    {
        sizeCadence = 0;
        if (CadenceDisplay.scanResult != null)
        {
            foreach (var cadence in CadenceDisplay.scanResult)
            {
                dropdownSpeedCadence.options.Add(new Dropdown.OptionData() { text = cadence.ToString() });
                sizeCadence++;
            }
        }

        if (CadenceDisplay.scanResult.Count == 1 && speedCadenceDisplay.connected == false && cadenceDisplay.connected == false)
        {
            dropdownCadence.value = 1;
            CadenceSelected(dropdownCadence);
        }
    }

    public void updateListTrainer()
    {
        sizetrainerList = 0;
        if (FitnessEquipmentDisplay.scanResult != null)
        {
            foreach (var trainer in FitnessEquipmentDisplay.scanResult)
            {
                dropdownTrainer.options.Add(new Dropdown.OptionData() { text = trainer.ToString() });
                sizetrainerList++;
            }
        }

        if (FitnessEquipmentDisplay.scanResult.Count == 1 && trainerDisplay.connected == false)
        {
            dropdownTrainer.value = 1;
            CadenceSelected(dropdownTrainer);
        }
    }

    private void updateListPower()
    {
        sizePowerList = 0;
        if (PowerMeterDisplay.scanResult != null)
        {
            foreach (var powerDevice in PowerMeterDisplay.scanResult)
            {
                dropdownPower.options.Add(new Dropdown.OptionData() { text = powerDevice.ToString() });
                sizePowerList++;
            }
        }

        if (PowerMeterDisplay.scanResult.Count == 1 && powerDisplay.connected == false)
        {
            dropdownPower.value = 1;
            PowerSelected(dropdownTrainer);
        }
    }
    public void changeTest()
    {
        SceneManager.LoadScene(5);
    }

    public void GoToMainPage()
    {
        SceneManager.LoadScene(2);
    }
}
