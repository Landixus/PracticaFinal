using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Session
{
    public Ruta ruta { get; set; }
    public Workout workout { get; set; }
    public float tempsTotal { get; set; }
    public List<float> fcList { get; set; }
    public List<int> rpmList { get; set; }
    public List<int> powerList { get; set; }
    public List<float> speedList { get; set; }

    public float fcMax { get; set; }
    public int rpmMax { get; set; }
    public int powerMax { get; set; }
    public float speedMax { get; set; }

    public float fcAvg { get; set; }
    public double rpmAvg { get; set; }
    public double powerAvg { get; set; }
    public float speedAvg { get; set; }

    public DateTime data;


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

        data = DateTime.Now;
    }
}
