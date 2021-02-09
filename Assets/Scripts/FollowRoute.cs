using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowRoute : MonoBehaviour
{

    private GameObject graph_windowObj;
    private Window_Graph graph_window;

    static public Ruta ruta;
    static public Workout workout;


    private CadenceDisplay cadence;
    private SpeedCadenceDisplay speedCadence;
    private PowerMeterDisplay power;
    private FitnessEquipmentDisplay rodillo;
    private HeartRateDisplay heartRate;
    private SpeedDisplay speed;

    public Text elapsedTime;
    public Text hr;
    public Text spd;
    public Text pow;
    public Text cad;
    public Text dist;


    private bool workoutSet;
    // Start is called before the first frame update
    void Start()
    {
        if (workout != null)
        {
            workoutSet = true;
        }
        else {
            workoutSet = false;
        }

        if (GameObject.Find("Window_Graph"))
        {
            graph_windowObj = GameObject.Find("Window_Graph");
            graph_window = (Window_Graph)graph_windowObj.GetComponent(typeof(Window_Graph));

            if (ruta != null)
            {
                graph_window.ShowGraph(ruta.trackPoints, ruta.pendentPunts, ruta.totalDistance, 160);
            }
            else {
                Debug.LogWarning("ruta == null (FollowRoute)");
            }
        }

        ConnectarSensors();

        cad.text = "--";
        spd.text = "--";
        pow.text = "--";
        elapsedTime.text = "--";
        hr.text = "--";
        dist.text = "--";
    }

    // Update is called once per frame
    void Update()
    {
        

        if (cadence.connected)
        {
            cad.text = cadence.cadence.ToString();
        }
        if (GameObject.Find("SpeedCadenceDisplay"))
        {
            cad.text = speedCadence.cadence.ToString();
            spd.text = speedCadence.speed.ToString();
        }
        if (heartRate.connected)
        {
            hr.text = heartRate.heartRate.ToString();
        }
        if (speed.connected)
        {
            spd.text = speed.speed.ToString();
            //uiText.text += "distance = " + GameObject.Find("SpeedDisplay").GetComponent<SpeedDisplay>().distance + "\n";
            
        }
        if (power.connected)
        {
            pow.text = power.instantaneousPower.ToString();
            //uiText.text += "cadence = " + GameObject.Find("PowerMeterDisplay").GetComponent<PowerMeterDisplay>().instantaneousCadence + "\n";    
        }
        if (rodillo.connected)
        {
            if (!power.connected)
            {
                pow.text = rodillo.instantaneousPower.ToString();
            }

            if (!speed.connected && !speedCadence.connected)
            {
                spd.text = rodillo.speed.ToString();
            }

            if (!heartRate.connected)
            {
                hr.text = rodillo.heartRate.ToString();
            }

            if (!cadence.connected && !speedCadence.connected)
            {
                cad.text = rodillo.cadence.ToString();
            }

            elapsedTime.text = rodillo.elapsedTime.ToString();

            dist.text = rodillo.distanceTraveled.ToString();
            //uiText.text += "distanceTraveled= " + GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>().distanceTraveled + "\n";
      
        }
        else { 
            //Si no està connectat pausem o algo
        }
    }

    //Funció que serveix per buscar els diferents objectes en Unity i guardarlos en una variable
    private void ConnectarSensors()
    {
        if (GameObject.Find("CadenceDisplay"))
        {
            cadence = GameObject.Find("CadenceDisplay").GetComponent<CadenceDisplay>();
        }
        if (GameObject.Find("SpeedCadenceDisplay"))
        {
            speedCadence = GameObject.Find("SpeedCadenceDisplay").GetComponent<SpeedCadenceDisplay>();
        }
        if (GameObject.Find("HeartRateDisplay"))
        {
            heartRate = GameObject.Find("HeartRateDisplay").GetComponent<HeartRateDisplay>();
        }
        if (GameObject.Find("SpeedDisplay"))
        {
            speed = GameObject.Find("SpeedDisplay").GetComponent<SpeedDisplay>();
        }
        if (GameObject.Find("PowerMeterDisplay"))
        {
            power = GameObject.Find("PowerMeterDisplay").GetComponent<PowerMeterDisplay>();

        }
        if (GameObject.Find("FitnessEquipmentDisplay"))
        {
            rodillo = GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>();
        }
    }
}
