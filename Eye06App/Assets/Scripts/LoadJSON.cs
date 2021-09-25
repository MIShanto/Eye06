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

    void Start()
    {
        getDataUrlTrash1 = "https://mamunspacer.herokuapp.com/api/data/";
        getDataUrlTrash2 = "https://mamunspacer.herokuapp.com/api/data/";
        getDataUrlTrash3 = "https://mamunspacer.herokuapp.com/api/data/";
        getDataUrlTrash4 = "https://mamunspacer.herokuapp.com/api/data/";

        InvokeRepeating("GetJsonData", 0f, 0.2f);
    }

    public void GetJsonData()
    {
        StartCoroutine(RequestWebService(getDataUrlTrash1, 1));
        StartCoroutine(RequestWebService(getDataUrlTrash2, 2));
        StartCoroutine(RequestWebService(getDataUrlTrash3, 3));
        StartCoroutine(RequestWebService(getDataUrlTrash4, 4));
    }

    IEnumerator RequestWebService(string url, int i)
    {
        using (UnityWebRequest webData = UnityWebRequest.Get(url))
        {

            yield return webData.SendWebRequest();
            if (webData.isNetworkError || webData.isHttpError)
            {
                //print("---------------- ERROR ----------------");
                //print(webData.error);
            }
            else
            {
                if (webData.isDone)
                {
                    JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data));

                    if (jsonData == null)
                    {
                        //print("---------------- NO DATA ----------------");
                    }
                    else
                    {
                        //print("---------------- JSON DATA ----------------");
                        if(i==1)
                            trash1.StartParsing(jsonData);
                        else if (i == 2)
                            trash2.StartParsing(jsonData);
                        else if (i == 3)
                            trash3.StartParsing(jsonData);
                        else if (i == 4)
                            trash4.StartParsing(jsonData);
                    }
                }
            }
        }
    }
}