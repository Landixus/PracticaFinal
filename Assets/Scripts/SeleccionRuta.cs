using LinqXMLTester;
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

    public void getTrack()
    {
        List<TrackPoint> trackPoints = gpx.LoadGPXTracks(routePath);
        string name = gpx.GetName(routePath);
        Debug.Log(name);

        Debug.Log(CalcPositiveElevation(trackPoints));
        //Debug.Log(CalcNegativeElevation(trackPoints) / 1000);
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

        //Com només volem la elevacio positiva només sumem si l'altura del següent punt és major que la de l'anterior punt
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
}
