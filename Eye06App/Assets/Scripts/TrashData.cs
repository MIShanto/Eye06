using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoordinateMapper;
using SimpleJSON;

public class TrashData : MonoBehaviour
{

    private void OnMouseDown()
    {

        UIManager.instance.UI_Controller.Play("textFadeIn");
        //get index
        int index = GetIndex();

        //get respected data
        JSONNode json_data = GetJsonData();

        //query and get info
        int total_count = json_data[0].Count; // total count
        string lat_text = json_data[0][index]; // lat
        string long_text = json_data[1][index]; // long
        string name_text = json_data[2]; // name
        string alt_text = json_data[3][index]; // alt

        //show 'em    :3
        UIManager uiManager = transform.parent.parent.gameObject.GetComponent<UIManager>();

        uiManager.UpdateTotalText("Total: " + total_count.ToString());

        uiManager.UpdateDebrisNameText("Name: " + name_text);

        uiManager.UpdateLatitudeText("Lat: " + lat_text);

        uiManager.UpdateLongitudeText("Long: " + long_text);

        uiManager.UpdateElevationText("Elevation: " + alt_text + " km");

       
    }

    int GetIndex()
    {
        return gameObject.transform.GetSiblingIndex();
    }

    JSONNode GetJsonData()
    {
        return transform.parent.gameObject.GetComponent<DefaultVisualizer>().json_data_of_trash;
    }
}
