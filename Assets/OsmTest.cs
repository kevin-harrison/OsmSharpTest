using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using OsmSharp.Streams;

public class OsmTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        using (var fileStream = new FileInfo(@".\Assets\map.osm").OpenRead())
        {
            var source = new XmlOsmStreamSource(fileStream);
            foreach (var element in source)
            {
                Debug.Log(element.ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
