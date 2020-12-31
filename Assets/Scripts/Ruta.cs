using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruta
{
    string name { get; set; }
    public List<TrackPoint> trackPoints { get; set; }
    float positiveElevation { get; set; }
    float negativeElevation { get; set; }
    public float totalDistance { get; set; }
    string description { get; set; }

    float[] distanciaPunts { get; set; }
    public float[] pendentPunts { get; set; }

    public Ruta(string name, List<TrackPoint> trackPoints, float positiveElevation, float negativeElevation, float totalDistance, float[] distanciaPunts, float[] pendentPunts)
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
