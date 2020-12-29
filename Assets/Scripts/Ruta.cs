using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruta
{
    string name { get; set; }
    List<TrackPoint> trackPoints { get; set; }
    int poitiveElevation { get; set; }
    int negativeElevation { get; set; }
    float distance { get; set; }
    string description { get; set; }

    double[,] pendent { get; set; }

    public Ruta(string name, List<TrackPoint> trackPoints, int poitiveElevation, int negativeElevation, float distance, double[,] pendent)
    {
        this.name = name;
        this.trackPoints = trackPoints;
        this.poitiveElevation = poitiveElevation;
        this.negativeElevation = negativeElevation;
        this.distance = distance;
        this.pendent = pendent;
    }
}
