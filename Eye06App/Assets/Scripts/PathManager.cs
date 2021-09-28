using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using CoordinateMapper;

public class PathManager : MonoBehaviour
{
    [SerializeField] private TextAsset _dataFile;

    public TextAsset dataFile { get { return _dataFile; } set { _dataFile = value; } }

    void Start()
    {
            string json = dataFile.ToString();
            var N = JSON.Parse(json);
            TestDraw(N);

    }

    public void TestDraw(JSONNode myJSON_file)
    {
        Vector3[] points = new Vector3[43200];
        for (int i = 0; i < 43200; i++)
        {
            var point = PlanetUtility.VectorFromLatLng(myJSON_file[0][0][i].AsFloat, myJSON_file[0][1][i].AsFloat, Vector3.right);
            var hitInfo = PlanetUtility.LineFromOriginToSurface(transform, point, LayerMask.GetMask("Planet"));
            points[i] = hitInfo.Value.point + (point * myJSON_file[0][2][i].AsFloat / 2000f);

        }
        LineController.instance.SetupLines(points);

    }
}
