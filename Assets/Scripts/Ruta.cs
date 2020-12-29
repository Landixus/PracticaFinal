using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruta
{
    string name { get; set; }
    List<TrackPoint> trackPoints { get; set; }
    double positiveElevation { get; set; }
    double negativeElevation { get; set; }
    double totalDistance { get; set; }
    string description { get; set; }

    double[] distanciaPunts { get; set; }
    public double[] pendentPunts { get; set; }

    public Ruta(string name, List<TrackPoint> trackPoints, double positiveElevation, double negativeElevation, double totalDistance, double[] distanciaPunts, double[] pendentPunts)
    {
        this.name = name;
        this.trackPoints = trackPoints;
        this.positiveElevation = positiveElevation;
        this.negativeElevation = negativeElevation;
        this.totalDistance = totalDistance;
        this.distanciaPunts = distanciaPunts;
        this.pendentPunts = pendentPunts;
    }

    public string toString()
    {
        return string.Format("name: {0}, positiveElev: {1}, negativeElev: {2}, totalDistance: {3}", name, positiveElevation, negativeElevation, totalDistance);
    }
}
