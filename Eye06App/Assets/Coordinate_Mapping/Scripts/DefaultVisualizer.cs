using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System;

namespace CoordinateMapper {
    public enum DataKeyFormat {
        JsonLatAndLngKeys, //JSON has seperate Latitude and Longitude keys in objects for each location
        JsonSingleLatLngArray, //JSON has a single array with alternating Latitude and Longitude numbers
        JsonLatLngArrays, //JSON has two arrays, one for Latitude and one for Longitude
        Csv //CSV parsing
    };

    public class DefaultVisualizer : MonoBehaviour, IDataLoader {
        [SerializeField] private TextAsset _dataFile;
        public TextAsset dataFile { get { return _dataFile; } set { _dataFile = value; } }

        [SerializeField] private GameObject pointPrefab = null;

        [SerializeField] private DataKeyFormat keyFormat = DataKeyFormat.JsonLatAndLngKeys;

        [SerializeField] private string latitudeKey = null;
        [SerializeField] private string longitudeKey = null;
        [SerializeField] private string magnitudeKey = null;

        [SerializeField] private DataLoadedEvent _loadComplete;
        public DataLoadedEvent loadComplete { get { return _loadComplete; } set { _loadComplete = value; } }

        [HideInInspector] public JSONNode json_data_of_trash;
        // Start is called before the first frame update
        void Start()
        {
            /* string json = dataFile.ToString();
             Vector3[] points = new Vector3[2];
             var N = JSON.Parse(json);

             var point = PlanetUtility.VectorFromLatLng(N[0][0][0][0].AsFloat, N[0][0][1][0].AsFloat, Vector3.right);
             var hitInfo = PlanetUtility.LineFromOriginToSurface(transform, point, LayerMask.GetMask("Planet"));

             points[0] = hitInfo.Value.point;
             point = PlanetUtility.VectorFromLatLng(N[0][0][0][1].AsFloat, N[0][0][1][1].AsFloat, Vector3.right);
             //Debug.LogError(point);
             hitInfo = PlanetUtility.LineFromOriginToSurface(transform, point, LayerMask.GetMask("Planet"));

             points[1] = hitInfo.Value.point;

             LineController.instance.SetupLines(points);
             Debug.LogError(points[0]);
             Debug.LogError(points[1]);*/

            //GetJsonData();
            //InvokeRepeating("GetJsonData", 0.2f, 0.2f);
            //ParseFile(dataFile.text);
            //ParseFile(LoadJSON.instance.json_data);

           /* string json = dataFile.ToString();
            Debug.Log(json);
            var N = JSON.Parse(json);
            TestDraw(N);

            ParseFile(dataFile.text);*/

        }
        void GetJsonData()
        {
            StartParsing(dataFile.text);

        }

        public void TestDraw(JSONNode myJSON_file)
        {
            Vector3[] points = new Vector3[43500];
            for (int i = 0; i < 43200; i++)
            {
                var point = PlanetUtility.VectorFromLatLng(myJSON_file[0][0][i].AsFloat, myJSON_file[0][1][i].AsFloat, Vector3.right);
                var hitInfo = PlanetUtility.LineFromOriginToSurface(transform, point, LayerMask.GetMask("Planet"));
                points[i] = hitInfo.Value.point + (point * myJSON_file[0][2][i].AsFloat / 2000f);

            }
            LineController.instance.SetupLines(points);
            LineController.instance.startDrawing = true;

        }
        public void StartParsing(JSONNode myJSON_file)
        {

            json_data_of_trash = myJSON_file;

            ParseFile(myJSON_file.ToString());
        }
        public async void ParseFile(string fileText) {
            var parser = new DefaultParser(fileText, keyFormat, latitudeKey, longitudeKey, magnitudeKey);
            var infos = await parser.HandleDefaultParsing();

            for(int i = 0; i < infos.Count; i++) {
                var info = infos[i];
                var childCount = transform.childCount;
                info.pointPrefab = pointPrefab;
                if (i >= childCount)
                {
                    var plotted = info.Plot(transform, transform, 0, false, null, json_data_of_trash[3][i].AsFloat);

                    plotted.name = "Default Point " + i;

                }
                else
                {
                    var plotted = info.Plot(transform, transform, 0, true, transform.GetChild(i).gameObject, json_data_of_trash[3][i].AsFloat);

                }
            }

            if (loadComplete != null) { loadComplete.Invoke(infos); }
        }

     
    }

    
}
