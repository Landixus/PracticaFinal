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

        Debug.Log(CalcPositiveElevation(trackPoints));
        Debug.Log(CalcNegativeElevation(trackPoints));

        //Dividiem entre 1000 ja que el resultat ens el dona en metres
        Debug.Log(CalcDistance(trackPoints)/1000 + " KM");
        //calc Elevation
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

    private double CalcDistance(List<TrackPoint> trackPoints)
    {
        double baseLat = trackPoints[1].lat;
        double baseLon = trackPoints[1].lon;
        double targetLat = trackPoints[0].lat;
        double targetLon = trackPoints[0].lon;

        //La primera crida la fem posant el target primer ja que aixi podem simplificar després l'assignació als nous punts
        double distance = DistanceTo(targetLat, targetLon, baseLat, baseLon);

        for (int i = 2; i < trackPoints.Count; i++)
        {
            targetLat = trackPoints[i].lat;
            targetLon = trackPoints[i].lon;

            distance += DistanceTo(baseLat, baseLon, targetLat, targetLon);

            baseLat = targetLat;
            baseLon = targetLon;
        }

        return distance;
    }

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

        var d1 = baseLat * (Math.PI / 180.0);
        var num1 = baseLon * (Math.PI / 180.0);
        var d2 = targetLat * (Math.PI / 180.0);
        var num2 = targetLon * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

        var d = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        Debug.Log(d);
        return d;
    }

}
