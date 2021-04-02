using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Session
{
    private Ruta ruta { get; set; }
    private Workout workout { get; set; }
    private float tempsTotal { get; set; }
    private List<float> fcList { get; set; }
    private List<int> rpmList { get; set; }
    private List<int> powerList { get; set; }
    private List<float> speedList { get; set; }

    private float fcMax { get; set; }
    private int rpmMax { get; set; }
    private int powerMax { get; set; }
    private float speedMax { get; set; }

    private float fcAvg { get; set; }
    private double rpmAvg { get; set; }
    private double powerAvg { get; set; }
    private float speedAvg { get; set; }

    public Session(Ruta ruta, Workout workout, float tempsTotal, List<float> fcList, List<int> rpmList, List<int> powerList, List<float> speedList)
    {
        this.ruta = ruta;
        this.workout = workout;
        this.tempsTotal = tempsTotal;
        
        this.fcList = fcList;
        this.rpmList = rpmList;
        this.powerList = powerList;
        this.speedList = speedList;


        if (this.fcList.Any())
        {
            fcMax = fcList.Max();
            fcAvg = fcList.Average();
        }
        else {
            fcMax = -1;
            fcAvg = -1;
        }


        if (this.rpmList.Any())
        {
            rpmMax = rpmList.Max();
            rpmAvg = rpmList.Average();
        }
        else
        {
            rpmMax = -1;
            rpmAvg = -1;
        }


        if (this.powerList.Any())
        {
            powerMax = powerList.Max();
            powerAvg = powerList.Average();
        }
        else
        {
            powerMax = -1;
            powerAvg = -1;
        }


        if (this.speedList.Any())
        {
            speedMax = speedList.Max();
            speedAvg = speedList.Average();
        }
        else
        {
            speedMax = -1;
            speedAvg = -1;
        }
    }
}
