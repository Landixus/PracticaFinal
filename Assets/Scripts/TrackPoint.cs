using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPoint 
{
    public float lat { get; set; }
    public float lon { get; set; }
    public float ele { get; set; }

    public TrackPoint(float lat, float lon, float ele)
    {
        this.lat = lat;
        this.lon = lon;
        this.ele = ele;
    }

}
