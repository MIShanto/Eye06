using System.Collections;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using TMPro;
using System.IO;
using CoordinateMapper;
using System.Collections.Generic;

[DefaultExecutionOrder(-90)]
public class LoadJSON : MonoBehaviour
{
/*    public static LoadJSON instance;

    //singleton..
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
*/

    public DefaultVisualizer trash1;
    public DefaultVisualizer trash2;
    public DefaultVisualizer trash3;
    public DefaultVisualizer trash4;

    string getDataUrlTrash1;
    string getDataUrlTrash2;
    string getDataUrlTrash3;
    string getDataUrlTrash4;

    string getPredictedDataUrlTrash2;

    void Start()
    {
        //lat, long, name, alt
        getDataUrlTrash1 = "https://mamunspacer.herokuapp.com/api/1/";
        getDataUrlTrash2 = "https://mamunspacer.herokuapp.com/api/2/";
        getDataUrlTrash3 = "https://mamunspacer.herokuapp.com/api/3/";
        getDataUrlTrash4 = "https://mamunspacer.herokuapp.com/api/4/";

        //getPredictedDataUrlTrash2 = "https://mamunspacer.herokuapp.com/api/p2";

        //StartCoroutine(GetJsonData());
        //InvokeRepeating("GetJsonData", 0f, 1f);
        GetJsonData();
/*
        StartCoroutine(RequestWebService(getDataUrlTrash1, 1));
        StartCoroutine(RequestWebService(getDataUrlTrash2, 2));
        StartCoroutine(RequestWebService(getDataUrlTrash3, 3));
        StartCoroutine(RequestWebService(getDataUrlTrash4, 4));

        StartCoroutine(RequestWebService(getPredictedDataUrlTrash2, 5));*/
    }

    /* IEnumerator GetJsonData()
     {
         yield return new WaitForSeconds(10f);

         StartCoroutine(RequestWebService(getDataUrlTrash1, 1));
         StartCoroutine(RequestWebService(getDataUrlTrash2, 2));
         StartCoroutine(RequestWebService(getDataUrlTrash3, 3));
         StartCoroutine(RequestWebService(getDataUrlTrash4, 4));

         StartCoroutine(RequestWebService(getPredictedDataUrlTrash2, 5));
     }*/

    void GetJsonData()
    {
        StartCoroutine(RequestWebService(getDataUrlTrash1, 1));
        StartCoroutine(RequestWebService(getDataUrlTrash2, 2));
        StartCoroutine(RequestWebService(getDataUrlTrash3, 3));
        StartCoroutine(RequestWebService(getDataUrlTrash4, 4));

       // StartCoroutine(RequestWebService(getPredictedDataUrlTrash2, 5));
    }

    IEnumerator RequestWebService(string url, int i)
    {
        using (UnityWebRequest webData = UnityWebRequest.Get(url))
        {

            yield return webData.SendWebRequest();
            if (webData.isNetworkError || webData.isHttpError)
            {
                //print("---------------- ERROR ----------------");
                print(webData.error);
                Invoke("GetJsonData", 10);
            }
            else
            {
                if (webData.isDone)
                {
                    JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data));

                    if (jsonData == null)
                    {
                        print("---------------- NO DATA ----------------");
                    }
                    else
                    {
                        print("---------------- JSON DATA ----------------");
                        if (i == 1)
                            trash1.StartParsing(jsonData);
                        else if (i == 2)
                        {
                            trash2.StartParsing(jsonData);
                        }
                        else if (i == 3)
                            trash3.StartParsing(jsonData);
                        else if (i == 4)
                            trash4.StartParsing(jsonData);
                        else if (i == 5)
                            trash2.TestDraw(jsonData);

                        Invoke("GetJsonData", 5);
                        
                    }
                }
            }
        }
        

        //StartCoroutine(GetJsonData());

    }
}