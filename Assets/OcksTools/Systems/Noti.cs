using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Noti : MonoBehaviour
{
    public Dictionary<string,string> Data;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Desc;
    public TextMeshProUGUI NewMedia;
    public Image Dispay;
    public void MarkAsRead()
    {
        Gamer.notifs.Remove(Data);
        Debug.Log("Removed" + Gamer.notifs.Count);

        Data["Previous"] = Data["Latest"];
        FileSystem.Instance.WriteFile(Data["TempPath"], Converter.DictionaryToString(Data, System.Environment.NewLine, ": "), true);

    }

}
