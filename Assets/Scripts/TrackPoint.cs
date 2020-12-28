using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPoint 
{
    public double lat { get; set; }
    public double lon { get; set; }
    public double ele { get; set; }

    public TrackPoint(double lat, double lon, double ele)
    {
        this.lat = lat;
        this.lon = lon;
        this.ele = ele;
    }

}
