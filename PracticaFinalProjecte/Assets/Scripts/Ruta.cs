using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruta
{
    public string name { get; set; }
    public List<@int> trackPoints { get; set; }
    public float positiveElevation { get; set; }
    public float negativeElevation { get; set; }
    
    public float totalDistance { get; set; } //en Km
    public string description { get; set; }
    public float[] distanciaPunts { get; set; }
    public float[] pendentPunts { get; set; }

    public float[] distAcomuladaSector { get; set; }

    public Ruta(string name, List<@int> trackPoints, float positiveElevation, float negativeElevation, float totalDistance, float[] distanciaPunts, float[] pendentPunts, float[] distAcomuladaSector)
    {
        this.name = name;
        this.trackPoints = trackPoints;
        this.positiveElevation = positiveElevation;
        this.negativeElevation = negativeElevation;
        this.totalDistance = totalDistance;
        this.distanciaPunts = distanciaPunts;
        this.pendentPunts = pendentPunts;
        this.distAcomuladaSector = distAcomuladaSector;
    }

    public string toString()
    {
        return string.Format("name: {0}, positiveElev: {1}, negativeElev: {2}, totalDistance: {3}", name, positiveElevation, negativeElevation, totalDistance);
    }
}
