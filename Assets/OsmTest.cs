using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq; // for from clauses
using UnityEngine;
using OsmSharp.Streams;
using OsmSharp.Complete;



public class OsmTest : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        CompleteWay[] buildingWays = GetBuildings(@".\Assets\map.osm");
        foreach (CompleteWay way in buildingWays)
        {
            Debug.Log(way.Id);
            Debug.Log(way.Tags);
            Debug.Log($"Nodes: {way.Nodes.Length}");
        }
    }

    // Gets building ways from the osm file
    private CompleteWay[] GetBuildings(string osmFilePath)
    {
        using (var fileStream = new FileInfo(osmFilePath).OpenRead())
        {
            XmlOsmStreamSource source = new XmlOsmStreamSource(fileStream);

            // Get all building ways and nodes 
            var buildings = from osmGeo in source
                            where osmGeo.Type == OsmSharp.OsmGeoType.Node ||
                            (osmGeo.Type == OsmSharp.OsmGeoType.Way && osmGeo.Tags != null && osmGeo.Tags.ContainsKey("building"))
                            select osmGeo;

            // Should filter before calling ToComplete() to reduce memory usage
            var completes = buildings.ToComplete(); // Create Complete objects (for Ways gives them a list of Node objects)
            var ways = from osmGeo in completes
                       where osmGeo.Type == OsmSharp.OsmGeoType.Way
                       select osmGeo;
            CompleteWay[] completeWays = ways.Cast<CompleteWay>().ToArray();
            return completeWays;
        }
    }
}
