using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Noti : MonoBehaviour
{
    public Dictionary<string, string> Data;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Desc;
    public TextMeshProUGUI NewMedia;
    public Image Dispay;
    public Image SnooserMenu;
    public Transform YearMenu;
    public Transform MonthMenu;
    public Transform DayMenu;
    public TextMeshProUGUI SnooserDisplay;
    private void Awake()
    {
        SnooserMenu.gameObject.SetActive(false);
    }
    public void MarkAsRead()
    {
        Gamer.notifs.Remove(Data);
        UnityEngine.Debug.Log("Removed" + Gamer.notifs.Count);

        Data["Previous2"] = Data["Previous"];
        Data["Previous"] = Data["Latest"];
        FileSystem.Instance.WriteFile(Data["TempPath"], Converter.DictionaryToString(Data, System.Environment.NewLine, ": "), true);
    }
    public void Snoose(bool shart)
    {
        monthbuilder = 0;
        yearbuilder = 0;
        string s = $"0/0/0";
        SnooserMenu.gameObject.SetActive(shart);
        YearMenu.gameObject.SetActive(true);
        MonthMenu.gameObject.SetActive(false);
        DayMenu.gameObject.SetActive(false);
        SnooserDisplay.text = s;
    }
    private int yearbuilder;
    private int monthbuilder;
    public void SetYear(int shart)
    {
        yearbuilder = shart;
        string s = $"0/{monthbuilder}/{yearbuilder + DateTime.Now.Year}";
        YearMenu.gameObject.SetActive(false);
        MonthMenu.gameObject.SetActive(true);
        DayMenu.gameObject.SetActive(false);
        SnooserDisplay.text = s;
    }
    public void SetMonth(int shart)
    {
        monthbuilder = shart;
        string s = $"0/{monthbuilder}/{yearbuilder + DateTime.Now.Year}";
        YearMenu.gameObject.SetActive(false);
        MonthMenu.gameObject.SetActive(false);
        DayMenu.gameObject.SetActive(true);
        SnooserDisplay.text = s;
    }
    public void SetDay(int shart)
    {
        Gamer.notifs.Remove(Data);
        UnityEngine.Debug.Log("Removed" + Gamer.notifs.Count);

        string s = $"{shart}/{monthbuilder}/{yearbuilder + DateTime.Now.Year}";
        if (Data.ContainsKey("Snoose"))
        {
            Data["Snoose"] = s;
        }
        else
        {
            Data.Add("Snoose", s);
        }
        FileSystem.Instance.WriteFile(Data["TempPath"], Converter.DictionaryToString(Data, System.Environment.NewLine, ": "), true);
    }
    public void OpenLink()
    {
        Process.Start(new ProcessStartInfo(Data["Website"]) { UseShellExecute = true });
    }

}
