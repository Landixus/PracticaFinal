using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Globalization;
using UnityEngine;

namespace LinqXMLTester
{
    public class GPXLoader
    {

        /// <summary>
        /// Load the Xml document for parsing
        /// </summary>
        /// <param name="sFile">Fully qualified file name (local)</param>
        /// <returns>XDocument</returns>
        private XDocument GetGpxDoc(string sFile)
        {
            bool fail = true;
            int tries = 0;
            while (fail && tries < 20) {
                try
                {
                    XDocument gpxDoc = XDocument.Load(sFile);
                    return gpxDoc;
                    fail = false;
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Eror al obrir fitxer: " + e.StackTrace);
                    fail = true;
                    tries++;
                }
            }
            return null;
        }

        /// <summary>
        /// Load the namespace for a standard GPX document
        /// </summary>
        /// <returns></returns>
        private XNamespace GetGpxNameSpace()
        {
            XNamespace gpx = XNamespace.Get("http://www.topografix.com/GPX/1/1");
            return gpx;
        }

        /// <summary>
        /// When passed a file, open it and parse all waypoints from it.
        /// </summary>
        /// <param name="sFile">Fully qualified file name (local)</param>
        /// <returns>string containing line delimited waypoints from
        /// the file (for test)</returns>
        /// <remarks>Normally, this would be used to populate the
        /// appropriate object model</remarks>
        public string LoadGPXWaypoints(string sFile)
        {
            XDocument gpxDoc = GetGpxDoc(sFile);
            XNamespace gpx = GetGpxNameSpace();

            var waypoints = from waypoint in gpxDoc.Descendants(gpx + "wpt")
                            select new
                            {
                                Latitude = waypoint.Attribute("lat").Value,
                                Longitude = waypoint.Attribute("lon").Value,
                                Elevation = waypoint.Element(gpx + "ele") != null ?
                                  waypoint.Element(gpx + "ele").Value : null,
                                Name = waypoint.Element(gpx + "name") != null ?
                                  waypoint.Element(gpx + "name").Value : null,
                                Dt = waypoint.Element(gpx + "cmt") != null ?
                                  waypoint.Element(gpx + "cmt").Value : null
                            };

            StringBuilder sb = new StringBuilder();
            foreach (var wpt in waypoints)
            {
                // This is where we'd instantiate data
                // containers for the information retrieved.
                sb.Append(
                  string.Format("Name:{0} Latitude:{1} Longitude:{2} Elevation:{3} Date:{4}\n",
                  wpt.Name, wpt.Latitude, wpt.Longitude,
                  wpt.Elevation, wpt.Dt));
            }

            return sb.ToString();
        }

        /// <summary>
        /// When passed a file, open it and parse all tracks
        /// and track segments from it.
        /// </summary>
        /// <param name="sFile">Fully qualified file name (local)</param>
        /// <returns>string containing line delimited waypoints from the
        /// file (for test)</returns>
        public List<@int> LoadGPXTracks(string sFile)
        {

            Debug.Log(sFile);
            XDocument gpxDoc = GetGpxDoc(sFile);
            if (gpxDoc != null)
            {
                XNamespace gpx = GetGpxNameSpace();
                var tracks = from track in gpxDoc.Descendants(gpx + "trk")
                             select new
                             {
                                 Name = track.Element(gpx + "name") != null ?
                                track.Element(gpx + "name").Value : null,
                                 Segs = (
                                    from trackpoint in track.Descendants(gpx + "trkpt")
                                    select new
                                    {
                                        Latitude = trackpoint.Attribute("lat").Value,
                                        Longitude = trackpoint.Attribute("lon").Value,
                                        Elevation = trackpoint.Element(gpx + "ele") != null ?
                                        trackpoint.Element(gpx + "ele").Value : null,
                                        Time = trackpoint.Element(gpx + "time") != null ?
                                        trackpoint.Element(gpx + "time").Value : null
                                    }
                                  )
                             };

                //StringBuilder sb = new StringBuilder();
                List<@int> trackPoints = new List<@int>();
                foreach (var trk in tracks)
                {
                    // Populate track data objects.
                    foreach (var trkSeg in trk.Segs)
                    {

                        //Necessitem passar els punts a , dels valors dek GPX ja que sino no es fa la conversio correctament
                        //Com no sabem si l'ususari utilitza , o . per separar els decimals utilitzem una cultutra invariant
                        //per normalitzar tots els valors, així tots es convertiran de manera correcta ja tinguin , o .


                        float lat = Convert.ToSingle(trkSeg.Latitude, CultureInfo.InvariantCulture);
                        float lon = Convert.ToSingle(trkSeg.Longitude, CultureInfo.InvariantCulture);

                        float ele = Convert.ToSingle(trkSeg.Elevation, CultureInfo.InvariantCulture);

                        trackPoints.Add(new @int(lat, lon, ele));
                    }
                }
                //return sb.ToString();
                return trackPoints;
            }
            return null;
        }

        public String GetName(string sFile)
        {
            XDocument gpxDoc = GetGpxDoc(sFile);
            XNamespace gpx = GetGpxNameSpace();
            var tracks = from track in gpxDoc.Descendants(gpx + "trk")
                         select new
                         {
                             Name = track.Element(gpx + "name") != null ?
                             track.Element(gpx + "name").Value : null,
                          };

            string name = tracks.First().Name;
           
            //return sb.ToString();
            return name;
        }
    }
}
