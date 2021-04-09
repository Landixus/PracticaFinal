using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private float x100completed;
    private float graphWidth;
    private float graphHeight;

    private RectTransform graphContainer;

    private int distanceTravel;
    private int currentSectorNum;

    [SerializeField] Text numText;
    [SerializeField] Text slopeText;

    private int distSession;

    private bool simular;
    [SerializeField] Button addVel;
    private float simulatedDist;
    private float simulatedVel;
    private float simulatedTime;
    private float simulatedTotalTime;
    
    private bool workoutSet;
    [SerializeField] Text workoutTxt;
    private List<float> tempsBlocs;

    private bool finished;

    private List<float> fcList;
    private List<int> rpmList;
    private List<int> powerList;
    private List<float> speedList;

    private float time;

    [SerializeField] Text diffText;
    private int instantPower;

    private bool sessioGuardada;

    [SerializeField] GameObject routeFinishedPanel;
    [SerializeField] GameObject confirmexitPanel;
    [SerializeField] GameObject savedPanel;


    // Start is called before the first frame update
    void Start()
    {

        Scene scene = SceneManager.GetActiveScene();
        Debug.Log("Active Scene name is: " + scene.name + "\nActive Scene index: " + scene.buildIndex);

        graphContainer = GameObject.Find("graphContainer").GetComponent<RectTransform>();

        if (workout != null)
        {
            workoutSet = true;
            tempsBlocs = CrearTempsBlocs();
        }
        else {
            workoutSet = false;
        }

        if (GameObject.Find("Window_Graph"))
        {
            graph_windowObj = GameObject.Find("Window_Graph");
            graph_window = (Window_Graph)graph_windowObj.GetComponent(typeof(Window_Graph));
            graphWidth = graph_windowObj.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta.x - 8;
            graphHeight = graph_windowObj.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta.y;
           
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


        workoutTxt.text = "";

        distanceTravel = 0;
        currentSectorNum = 0;
        
        String debug = "";
        foreach (var punt in ruta.distanciaPunts)
        {
            debug += punt.ToString() + " ";
        }
        Debug.Log(debug);

        Debug.Log(ruta.distanciaPunts.Length);

        debug = "";
        foreach (var punt in ruta.pendentPunts)
        {
            debug += punt.ToString() + " ";
        }
        Debug.Log(debug);

        debug = "";
        foreach (var punt in ruta.distAcomuladaSector)
        {
            debug += punt.ToString() + " ";
        }
        Debug.Log(debug);


        distSession = rodillo.distanceTraveled;


        rodillo.RequestTrainerCapabilities();
        rodillo.SetTrainerUserConfiguration(8.5f, 55.5f);
        rodillo.RequestUserConfig();
        rodillo.RequestCommandStatus();
        rodillo.RequestCommandStatus();
        /*Debug.Log("------> simulation" + rodillo.trainerCapabilities.simulationModeSupport);
        Debug.Log("------> targetPower " + rodillo.trainerCapabilities.targetPowerModeSupport);
        Debug.Log("------> masx " + rodillo.trainerCapabilities.maximumResistance.ToString());
        Debug.Log("------> basic" + rodillo.trainerCapabilities.basicResistanceNodeSupport);*/

        //Inicialitzar dades simualdor
        simulatedDist = 0;
        simulatedVel = 0;
        simulatedTime = 0;
        simulatedTotalTime = 0;


        //Inicialitzem variables per crear historial
        fcList = new List<float>();
        rpmList = new List<int>();
        powerList = new List<int>();
        speedList = new List<float>();

        diffText.text = "";

        sessioGuardada = false;
}

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            if (cadence != null)
            {
                if (cadence.connected)
                {
                    cad.text = cadence.cadence.ToString();

                    rpmList.Add(cadence.cadence);
                }
            }

            if (speedCadence != null)
            {
                if (speedCadence.connected)
                {
                    cad.text = speedCadence.cadence.ToString();
                    spd.text = speedCadence.speed.ToString();


                    rpmList.Add(speedCadence.cadence);
                    speedList.Add(speedCadence.speed);
                }
            }

            if (heartRate != null)
            {
                if (heartRate.connected)
                {
                    hr.text = heartRate.heartRate.ToString();

                    fcList.Add(heartRate.heartRate);
                }
            }

            if (speed != null)
            {
                if (speed.connected)
                {
                    spd.text = speed.speed.ToString();
                    //uiText.text += "distance = " + GameObject.Find("SpeedDisplay").GetComponent<SpeedDisplay>().distance + "\n";

                    speedList.Add(speed.speed);
                }
            }

            if (power != null)
            {
                if (power.connected)
                {
                    pow.text = power.instantaneousPower.ToString();
                    //uiText.text += "cadence = " + GameObject.Find("PowerMeterDisplay").GetComponent<PowerMeterDisplay>().instantaneousCadence + "\n"; 
                    instantPower = power.instantaneousPower;
                    powerList.Add(power.instantaneousPower);
                }
            }

            if (rodillo.connected)
            {
                if (!power.connected)
                {
                    pow.text = rodillo.instantaneousPower.ToString();
                    instantPower = rodillo.instantaneousPower;
                }

                if (!speed.connected && !speedCadence.connected)
                {
                    spd.text = rodillo.speed.ToString();
                }

                //Pot ser una opció pero no es vaible ja que la majoría de rodillos crec que no te connexio directa
                //Amb el sensor de frec card i per tant ens ensenya el número 255 (com a mínim en el bkool)
                //(255 númeor màxim que es pot aconseguir amb un byte)
                /*if (!heartRate.connected)
                {
                    hr.text = rodillo.heartRate.ToString();
                }*/

                if (!cadence.connected && !speedCadence.connected)
                {
                    cad.text = rodillo.cadence.ToString();
                }

                //rodillo.elapsedTime en segons
                time = rodillo.elapsedTime;
                elapsedTime.text = rodillo.elapsedTime.ToString();

                // rodillo.distanceTraveled en metres 
                //dist session es la distancia que ha fet en total el rodillo en una mateixa connexio, per tant si l'usuari fa varies rutes sebse desconnectar
                //tindriem un error per tant li hem de restar la distancia inicial que agafem en el start
                distanceTravel = rodillo.distanceTraveled - distSession;



                //Passem de metres a km;
                float kmTraveled = (float)distanceTravel / (float)1000;

                //Mirar de fer truncate;
                dist.text = Math.Round(kmTraveled, 3).ToString();
                dist.text = kmTraveled.ToString() + "km";



                //dist.text = kmTraveled.ToString("0.00");

                if (distanceTravel > ruta.distAcomuladaSector[currentSectorNum])
                {
                    currentSectorNum = FindClosest(distanceTravel);

                    if (currentSectorNum >= ruta.pendentPunts.Length)
                    {
                        finished = true;
                    }
                    else
                    {

                        //No funciona ja que no podem enviar informació al rodillo
                        //canviar pendent segons currentSectNum
                        rodillo.SetTrainerResistance((int)(ruta.pendentPunts[currentSectorNum] * 100));
                        rodillo.RequestTrainerCapabilities();
                    }
                }

                if (!finished)
                {
                    Debug.Log("Current sector num:" + currentSectorNum);
                    numText.text = currentSectorNum.ToString();
                    Debug.Log("Current % of sector" + ruta.pendentPunts[currentSectorNum]);
                    slopeText.text = "% " + ruta.pendentPunts[currentSectorNum].ToString();
                }

                Debug.Log("Faig log abans de x100 completed");
                try
                {
                    //ruta.totalDistance * 1000 ja que totalDistance està en Km
                    //es pot afegir * constant en  rodillo.distanceTraveled per si volem que la distancia passi més ràpid
                    x100completed = distanceTravel / (ruta.totalDistance * 1000);
                    Debug.Log("DistanecTravel:" + rodillo.distanceTraveled + "TotalDist(m):" + ruta.totalDistance * 1000 + " % " + x100completed + "%");
                }
                catch (System.Exception)
                {
                    //
                    Debug.LogWarning("Rodillo s'ha desconnectat");
                    ConnectarSensors();
                }
                //uiText.text += "distanceTraveled= " + GameObject.Find("FitnessEquipmentDisplay").GetComponent<FitnessEquipmentDisplay>().distanceTraveled + "\n";

            }
            else
            {

                //mirem si volem entrar mode simulacio
                if (simular)
                {
                    simulatedDist = (simulatedDist + simulatedVel * (simulatedTime * 0.06f));
                    simulatedTotalTime += simulatedTime * 0.06f;

                    elapsedTime.text = simulatedTotalTime.ToString();
                    spd.text = simulatedVel.ToString();


                    //Passem de metres a km;
                    float kmTraveled = (float)simulatedDist / (float)1000;

                    //Mirar de fer truncate;
                    dist.text = Math.Round(kmTraveled, 3).ToString();
                    dist.text = kmTraveled.ToString() + "km";



                    //dist.text = kmTraveled.ToString("0.00");

                    if (simulatedDist > ruta.distAcomuladaSector[currentSectorNum])
                    {
                        currentSectorNum = FindClosest(simulatedDist);

                        if (currentSectorNum >= ruta.pendentPunts.Length)
                        {
                            finished = true;
                        }
                    }

                    if (!finished)
                    {
                        Debug.Log("Current sector num:" + currentSectorNum);
                        numText.text = currentSectorNum.ToString();
                        Debug.Log("Current % of sector" + ruta.pendentPunts[currentSectorNum]);
                        slopeText.text = "% " + ruta.pendentPunts[currentSectorNum].ToString();
                    }

                    x100completed = simulatedDist / (ruta.totalDistance * 1000);
                }
            }

            Debug.Log("Workout set:" + workoutSet);
            if (workoutSet)
            {
                Debug.Log("Faig log abans de calcular temps");


                float temps;
                if (simular)
                {
                    temps = simulatedTotalTime;
                }
                else
                {
                    temps = rodillo.elapsedTime;
                }

                Debug.Log("Temps rodillo: " + temps);
                Debug.Log("Temps workout: " + workout.tempsTotal);

                if ((float)workout.tempsTotal < temps)
                {
                    workoutTxt.text = "Felicitats!! Has acabat l'entrenament";
                    Debug.Log("S'ha acabat l'entrenament");
                }
                else
                {
                    Debug.Log("No s'ha acabat l'entrenament");
                    var numBloc = 0;
                    Bloc bloc = null;
                    for (int i = 0; i < tempsBlocs.Count; i++)
                    {
                        Debug.Log("Temps bloc " + i + " = " + tempsBlocs[i]);
                        if (temps > tempsBlocs[i])
                        {
                            numBloc = i;
                            if (numBloc != workout.blocs.Count)
                            {
                                bloc = workout.blocs[numBloc];
                            }
                        }
                    }
                    if (bloc != null)
                    {
                        numBloc++;
                        workoutTxt.text = "Num bloc: " + numBloc + " Poténcia objectiu: " + bloc.pot + "W Temps bloc: " + bloc.temps;

                        if (instantPower > bloc.pot * 1.10)
                        {
                            diffText.text = "Massa poténcia. Afluixa";
                        }
                        else if (instantPower < bloc.pot * 0.9)
                        {
                            diffText.text = "Falta poténcia. Apreta";
                        }
                        else
                        {
                            diffText.text = "";
                        }
                    }
                }
            }

            //Anem movent la barra segons l'usuari va avançant en la ruta
            //+3 i +34 per compensar el desviament de la barra en la posició original. 
            //(Crec que es problema de com es dibuixa la barra ja que agafa el punt intermitg entre els dos punts que passem en comptes de
            //dibuixar directament) 
            Vector2 puntA = new Vector2(graphWidth * x100completed + 4, 35);
            Vector2 puntB = new Vector2(graphWidth * x100completed + 4, graphHeight + 35);

            DrawVerticalLine(puntA, puntB);
        } else
        {
            Debug.Log("S'ha acabat la ruta");

            routeFinishedPanel.SetActive(true);  
        }
    }

    public void guardarEntrenament()
    {
        StartCoroutine("ActivarPanelGuardat");
        //Només volem guardar a l'historial les sessions no simulades i que no hem guardat previament
        //Ja que el update segueix guardant la sessio cada cop que es crida si no li diem que ja està guardada
        if (!simular && !sessioGuardada)
        {
            //Creem la sessio acabada i la guardem a la llista de l'usuari
            Session session = new Session(ruta, workout, time, fcList, rpmList, powerList, speedList);
            PaginaPrincipal.user.historial.Add(session);
            sessioGuardada = true;
            Debug.Log("Num sessions: " + PaginaPrincipal.user.historial.Count);
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

            if (rodillo != null)
            {
                Debug.Log("Rodillo connected:" + rodillo.connected);
                if (rodillo.connected == false)
                {
                    simular = true;
                    addVel.enabled = true;
                }
                else {
                    simular = false;
                    addVel.enabled = false;
                }
            }
            else {
                simular = true;
                addVel.enabled = true;
            }
        }
    }


    private void DrawVerticalLine(Vector2 dotPositionA, Vector2 dotPositionB)
    {

        GameObject line = GameObject.Find("verticalLine");
        Destroy(line);

        //La x dels dos punts es la mateixa
        //Debug.Log("x: " + dotPositionA.x);
        GameObject gameObject = new GameObject("verticalLine", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1);

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 1f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .3f;

        rectTransform.localEulerAngles = new Vector3(0, 0, 90);

    }

    //Funció que retorna la posició de l'array amb el nombre igual o més proper al que l' hem passat
    //https://stackoverflow.com/questions/57921661/finding-sorted-array-index-of-closest-int-value
    private static int FindClosest( float value)
    {
        var result = Array.BinarySearch(ruta.distAcomuladaSector, value);
        if (result >= 0)
            return result;
        else return ~result;
    }

    public void addVelocity() {
        simulatedVel += 30;
        simulatedTime = 1;
    }

    private List<float> CrearTempsBlocs()
    {
        List<float> temp = new List<float>();

        temp.Add(0f);

        float tempsTotal;

        for (int i = 0; i < workout.blocs.Count; i++)
        {
            tempsTotal = 0;
            for (int j = 0; j <= i; j++)
            {
                tempsTotal += workout.blocs[j].temps;
            }
            temp.Add(tempsTotal);
        }

        return temp;
    }


    public void goToMainPage()
    {
        SceneManager.LoadScene(sceneName: "MainPage");
    }

    public void showConfirmPanel()
    {
        confirmexitPanel.SetActive(true);
    }

    public void noSortir() 
    {
        confirmexitPanel.SetActive(false);
    }
    IEnumerator ActivarPanelGuardat()
    {

        savedPanel.SetActive(true);

        yield return new WaitForSeconds(1f);

        savedPanel.SetActive(false);

        goToMainPage();
    }
}
