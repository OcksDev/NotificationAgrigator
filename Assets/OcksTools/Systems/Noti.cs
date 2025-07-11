using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        UnityEngine.Debug.Log("Removed" + Gamer.notifs.Count);

        Data["Previous2"] = Data["Previous"];
        Data["Previous"] = Data["Latest"];
        FileSystem.Instance.WriteFile(Data["TempPath"], Converter.DictionaryToString(Data, System.Environment.NewLine, ": "), true);

    }
    public void OpenLink()
    {
        Process.Start(new ProcessStartInfo(Data["Website"]) { UseShellExecute = true });
    }

}
