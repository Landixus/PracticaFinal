using LinqXMLTester;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionRuta : MonoBehaviour
{
    public string routePath;
    public GPXLoader gpx;


    // Start is called before the first frame update
    void Start()
    {
        gpx = new GPXLoader();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetTrack()
    {
        List<TrackPoint> trackPoints = gpx.LoadGPXTracks(routePath);
        string name = gpx.GetName(routePath);
        Debug.Log(name);

        //calc Elevation
        Debug.Log(CalcPositiveElevation(trackPoints));
        Debug.Log(CalcNegativeElevation(trackPoints));

        //Dividiem entre 1000 ja que el resultat ens el dona en metres
        double[] ditancePoints = CalcDistance(trackPoints);

        Debug.Log(CalcTotalDistance(ditancePoints)/1000 + " KM");

        //Calc pendent
        double[] slope = CalcSlope(trackPoints, ditancePoints);

        AddSlopeInformation(slope);

    }

    private double CalcPositiveElevation(List<TrackPoint> trackPoints)
    {
        double actualElev = trackPoints[0].ele;
        double priorElev = trackPoints[1].ele;
        
        //Al principi ho fem al reves ja que després podem fer una assignació directe al nou punt i ja tenim l'1 ben guardat
        double positiveElev = 0;

        //Com només volem la elevacio positiva només sumem si l'altura del següent punt és major que la de l'anterior punt
        if (priorElev > actualElev)
        {
             positiveElev = priorElev - actualElev;
        }
       

        for (int i = 2; i < trackPoints.Count; i++)
        {
            actualElev = trackPoints[i].ele;
            if (actualElev > priorElev)
            {
                positiveElev += actualElev - priorElev;
                //Debug.Log(positiveElev);
            }
            priorElev = actualElev;
        }

        return positiveElev;
    }

    private double CalcNegativeElevation(List<TrackPoint> trackPoints)
    {
        double actualElev = trackPoints[0].ele;
        double priorElev = trackPoints[1].ele;

        //Al principi ho fem al reves ja que després podem fer una assignació directe al nou punt i ja tenim l'1 ben guardat
        double negativeElev = 0;

        //Com només volem la elevacio negativa només sumem si l'altura del següent punt és menor que la de l'anterior punt
        if (priorElev < actualElev)
        {
            negativeElev = priorElev - actualElev;
        }


        for (int i = 2; i < trackPoints.Count; i++)
        {
            actualElev = trackPoints[i].ele;
            if (actualElev < priorElev)
            {
                negativeElev += actualElev - priorElev;
            }
            priorElev = actualElev;
        }
        //Multipliquem per -1 per fer el número positiu
        return negativeElev * -1;
    }

    private double[] CalcDistance(List<TrackPoint> trackPoints)
    {
        double baseLat = trackPoints[1].lat;
        double baseLon = trackPoints[1].lon;
        double targetLat = trackPoints[0].lat;
        double targetLon = trackPoints[0].lon;

        //hem de restar 1 ja que volem la distancia entre dos punts i per tant hem de restar 1
        double[] distance = new double[trackPoints.Count-1];

        //La primera crida la fem posant el target primer ja que aixi podem simplificar després l'assignació als nous punts
        distance[0] = DistanceTo(targetLat, targetLon, baseLat, baseLon);

        for (int i = 2; i < trackPoints.Count; i++)
        {
            targetLat = trackPoints[i].lat;
            targetLon = trackPoints[i].lon;

            distance[i-1] = DistanceTo(baseLat, baseLon, targetLat, targetLon);

            baseLat = targetLat;
            baseLon = targetLon;
        }

        return distance;
    }

    //Funcio per calcular la distància entre dos punts donats en angle de coordenades
    private static double DistanceTo(double baseLat, double baseLon, double targetLat, double targetLon)
    {

        //Fórmula treta de https://www.movable-type.co.uk/scripts/latlong.html

        /* var R = 6371e3;
        var baseRad = Math.PI * baseLat / 180;
        var targetRad = Math.PI * targetLat / 180;
        var theta = (targetLat - baseLat) * Math.PI;
        var thetaRad = (targetLon - baseLon) * Math.PI;

        double a =
            Math.Sin(theta/2) * Math.Sin(theta/2) + 
            Math.Cos(baseRad) * Math.Cos(targetRad) * 
            Math.Sin(thetaRad/2) * Math.Sin(thetaRad/2);
        var c = 2 * Math.Atan2(Math.Sqrt(a),Math.Sqrt(1-a));

        var d = R * c; // En metres 

        Debug.Log("R: " + R + "c: "+ c + " d:" + d);

        return d;*/


        //Fórmula treta de https://stackoverflow.com/questions/6366408/calculating-distance-between-two-latitude-and-longitude-geocoordinates
        //És l'implementació de la funció GetCordinates de la classe GeoCoordinate

        var d1 = baseLat * (Math.PI / 180.0);
        var num1 = baseLon * (Math.PI / 180.0);
        var d2 = targetLat * (Math.PI / 180.0);
        var num2 = targetLon * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

        var d = Math.Round(6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3))), 2);
        //Debug.Log(d);
        return d;
    }

    private double CalcTotalDistance(double[] points)
    {
        double dist = 0;
        foreach (var point in points)
        {
            dist += point;
        }

        return dist;
    }

    //Funció per calcular el pendent entre dos punts, es calcula la pendent entre amb 5 punts de diferencia per ajustar els càlculs i que sigui més uniforme
    private double[] CalcSlope(List<TrackPoint> trackPoints, double[] distancePoints)
    {
        double actualElev = trackPoints[0].ele;
        double priorElev = trackPoints[5].ele;

        double distanceBetweenPoints = Calc5pointsDistance(distancePoints, 0);

        double[] slopePoints = new double[distancePoints.Length];

        //* 100 ja que volem el percentatge per després poder adequar la dificultat del rodillo
        slopePoints[0] = (priorElev - actualElev) / distanceBetweenPoints * 100;
        Debug.Log(slopePoints[0]);
        for (int i = 10; i < trackPoints.Count; i += 5)
        {
            actualElev = trackPoints[i].ele;
            distanceBetweenPoints = Calc5pointsDistance(distancePoints, i-5);

            double elevDiff = Math.Round(actualElev - priorElev);

            if (distanceBetweenPoints == 0)
            {
                slopePoints[i - 1] = 0;
            }
            else 
            {
                slopePoints[i - 1] = Math.Round(elevDiff / distanceBetweenPoints * 100, 1);
            }
          

            //Debug.Log("I: " + i +" i-1: " +(i-1)+ " Target Point :" + actualElev + " Piror elev: " + priorElev + " Diff: " + elevDiff + " Dist between points: " + distanceBetweenPoints + " Slope: " + slopePoints[i - 1]);
            //Debug.Log(slopePoints[i-1]);
            priorElev = actualElev;
        }
        
        return slopePoints;
    }

    //funció que serveix per calcular la distancia entre 5 punts
    private double Calc5pointsDistance(double[] distancePoints, int index)
    {
        ArraySegment<double> splitarray = new ArraySegment<double>(distancePoints, index, 5);

        double dist = 0;
        foreach (var item in splitarray)
        {
            dist += item;
        }

        return dist;
    }
    
    //Funció que serveix per calcular la pendent que ens falta. Agafem els dos punts on tenim informació i calculem la diferencia entre ells 
    // i calculem step per arriabar a l'altura seguent
    private void AddSlopeInformation(double[] slope)
    {
        double priorElev = slope[0];

        for (int i = 4; i < slope.Length; i += 5)
        {

            double actualElev = slope[i];
            double elevDiff = Math.Round(actualElev - priorElev, 2);
            double step = Math.Round(elevDiff / 4, 2);

            //Debug.Log("actual elev:" + actualElev + " prior elev:" + priorElev);

            //Per estalviar càlculs si no hi ha canvis d'altura vol dir que el desnivell és 0
            if (step != 0)
            {
                for (int j = i - 4; j < i; j++)
                {
                    slope[j] = Math.Round(slope[i] + step * (j - i), 1);
                    //Debug.Log("i:" + j + " value:" + slope[j]);
                }
                priorElev = actualElev;
            }
            //Debug.Log("i:" + i + " value:" + slope[i]);

        }
    }

}
