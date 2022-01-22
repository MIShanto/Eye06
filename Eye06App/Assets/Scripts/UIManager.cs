using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

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

    //[SerializeField] GameObject panel;
    public Animator UI_Controller;
    [SerializeField] TextMeshProUGUI total_text;
    [SerializeField] TextMeshProUGUI debris_name_text;
    [SerializeField] TextMeshProUGUI latitude_text;
    [SerializeField] TextMeshProUGUI longitude_text;
    [SerializeField] TextMeshProUGUI elivation_text;

    public void UpdateTotalText(string txt)
    {
        total_text.text = txt;
    }   
    public void UpdateDebrisNameText(string txt)
    {
        debris_name_text.text = txt;
    }    
    public void UpdateLatitudeText(string txt)
    {
        latitude_text.text = txt;
    }   
    public void UpdateLongitudeText(string txt)
    {
        longitude_text.text = txt;
    }  
    public void UpdateElevationText(string txt)
    {
        elivation_text.text = txt;
    }  

    public void HideUI()
    {
        UI_Controller.Play("textFadeOut");
    }

}
