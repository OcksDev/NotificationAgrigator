using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HtmlAgilityPack;
using System.Xml.Linq;
using System.Threading;
using System.Xml;
using System.IO;
using TMPro;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;

public class Gamer : MonoBehaviour
{
    public GameObject Spawner;
    public List<GameObject> rere;
    public static List<Dictionary<string,string>> notifs = new List<Dictionary<string,string>>();
    public static int cummers = 0;
    public static int idealcummers = -1;
    public List<ImageSexNugget> AllImages = new List<ImageSexNugget>();
    public Dictionary<string, ImageSexNugget> reebankon = new Dictionary<string, ImageSexNugget>();

    private void Start()
    {
        bool wankoff = false;
        if (wankoff)
        {
            idealcummers = 0;
            cummers = 0;
        }
        else
        {
            foreach (var a in AllImages)
            {
                reebankon.Add(a.Name, a);
            }
            StartCoroutine(gamin());
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NotifSex();
        }
    }

    public void NotifSex()
    {
        Instantiate(rere[2], transform.position, Quaternion.identity, rere[1].transform);

        SoundSystem.Instance.PlaySoundWithClipping("n", false, 1, 1f + (continueoius * 0.2f));

        if (continueoius <= 10)
            continueoius++;
        if (BGShex != null) StopCoroutine(BGShex);
        BGShex = StartCoroutine(WankWank());
    }
    public Coroutine BGShex;

    public int continueoius = 0;
    public IEnumerator WankWank()
    {

        var a = rere[0].GetComponent<SpriteRenderer>();
        var c = new Color32(50,50,50,255);
        a.color = c;
        int steps = 25;
        float per = 1f / steps;
        for(int i = 0; i < 50; i++)
        {
            a.color = Color.Lerp(c,Color.black,per*i);
            yield return new WaitForFixedUpdate();
        }
        a.color = Color.black;
        yield return null;
    }


    public IEnumerator gamin()
    {
        var b = Directory.GetFiles($"{FileSystem.Instance.GameDirectory}\\Notifs");
        idealcummers = b.Length;
        foreach (var a in b)
        {
            new Thread(() => { GetUpdate(a); }).Start();
            yield return new WaitForSeconds(0.025f);
        }
    }


    int oldtesty = -1;
    public List<Noti> banas = new List<Noti>();
    private void FixedUpdate()
    {
        if(oldtesty != notifs.Count)
        {
            if(notifs.Count > oldtesty && notifs.Count > 0)
            {
                NotifSex();
            }
            var ewank = new List<Dictionary<string,string>>(notifs);
            oldtesty = ewank.Count;
            foreach (var a in banas)
            {
                Destroy(a.gameObject);
            }
            banas.Clear();
            foreach (var a in ewank)
            {
                CreateNoti(a);
            }
        }
        if (idealcummers == cummers)
        {
            Tags.refs["Counter"].GetComponent<TextMeshProUGUI>().text = $"Notifications: {oldtesty}";
        }
        else
        {
            Tags.refs["Counter"].GetComponent<TextMeshProUGUI>().text = $"Loading.. {cummers}/{idealcummers}";
        }
    }

    public void CreateNoti(Dictionary<string,string> data)
    {
        var cc = Instantiate(Spawner, transform.position, Quaternion.identity, Tags.refs["Content"].transform).GetComponent<Noti>();
        cc.Data = data;
        cc.Title.text = data["Title"];
        cc.Desc.text = $"New {data["MediaName"]}!";
        cc.NewMedia.text = data["Latest"];
        cc.Dispay.sprite = reebankon[data["Image"]].reebaka;
        banas.Add(cc);
    }


    static void Main(string[] args)
    {
        /*
        new Thread(() => { GetUpdate("RR", @"https://www.royalroad.com/fiction/73052/technomagica-progression-fantasy-litrpg-free-until"); }).Start();
        new Thread(() => { GetUpdate("RR", @"https://www.royalroad.com/fiction/77972/syl-a-slime-monster-evolution-litrpg"); }).Start();
        new Thread(() => { GetUpdate("RR", @"https://www.royalroad.com/fiction/39408/beware-of-chicken"); }).Start();
        new Thread(() => { GetUpdate("YT", @"https://www.youtube.com/@ocks_dev/videos"); }).Start();
        new Thread(() => { GetUpdate("YT", @"https://www.youtube.com/@cjthex/videos"); }).Start();
        new Thread(() => { GetUpdate("VIZ", @"https://www.viz.com/shonenjump/chapters/chainsaw-man"); }).Start();
        */

    }

    public static void GetUpdate(string aa)
    {
        var data = Converter.StringToDictionary(FileSystem.Instance.ReadFile(aa), System.Environment.NewLine, ": ");
        /*if (data["Type"] != "STM")
        {

            cummers++;
            return;
        }*/
        var e = GetHTMLFromWebsite(data["Website"], data["Type"]);
        string ee = "";
        //Console.WriteLine($"[[{GetLatest_RoyalRoad(e)}]]");
        switch (data["Type"])
        {
            case "VIZ": ee = GetLatest_VIZ(e); break;
            case "RR": ee = GetLatest_RoyalRoad(e); break;
            case "YT": ee = GetLatest_Youtube(e); break;
            case "YTM": ee = GetLatest_YoutubeMusic(e); break;
            case "YTT": ee = GetLatest_YoutubeMusicTopic(e); break;
            case "STM": ee = GetLatest_SteamUpdate(e); break;
            case "MF": ee = GetLatest_Mangafire(e); break;
            case "AUD": ee = GetLatest_Audible(e); break;
            //case "YT": ee = "GAMING"; break;
            default: Debug.LogError("Invalid type"); break;
        }
        //use ee
        if (!data.ContainsKey("Latest")) data.Add("Latest", "");
        if (!data.ContainsKey("Previous")) data.Add("Previous", "");
        if (!data.ContainsKey("Previous2")) data.Add("Previous2", "");
        if (!data.ContainsKey("TempPath"))
        {
            data.Add("TempPath", aa);
        }
        else
        {
            data["TempPath"] = aa;
        }
        if(ee != data["Latest"] || data["Latest"] != data["Previous"])
        {
            if(ee != data["Previous2"])
            {
                //use events to do a clalback?
                //if not then just a static list and just append self to its
                data["Latest"] = ee;
                notifs.Add(data);
            }
        }
        cummers++;
        FileSystem.Instance.WriteFile(aa, Converter.DictionaryToString(data, System.Environment.NewLine, ": "), true);
    }



    public static string GetLatest_RoyalRoad(string rawhtml)
    {
        var e = rawhtml;
        e = e.Substring(e.LastIndexOf("cursor: pointer"));
        e = e.Substring(0, e.IndexOf("</a>"));
        e = e.Substring(e.IndexOf("<a href"));
        e = e.Substring(e.IndexOf("\">") + 3);

        return CleanText(e);
    }

    public static string GetLatest_Youtube(string rawhtml)
    {
        var e = rawhtml;
        e = e.Substring(e.IndexOf("\"title\":{\"runs\":[{\"text\":\""));
        e = e.Substring(0, e.IndexOf("\"}],\"accessibility\""));
        e = e.Substring(e.IndexOf("text") + 7);

        return CleanText(e);
    }
    public static string GetLatest_YoutubeMusic(string rawhtml)
    {
        var e = rawhtml;
        e = e.Substring(e.IndexOf("richItemRenderer"));
        e = e.Substring(0, e.IndexOf("thumbnails"));
        e = e.Substring(0, e.IndexOf("\"},"));
        e = e.Substring(e.IndexOf("simpleText") + "simpleText".Length + 3);


        return CleanText(e);
    }
    public static string GetLatest_YoutubeMusicTopic(string rawhtml)
    {
        var e = rawhtml;
        e = e.Substring(e.IndexOf("{\"title\":{\"content\":\"") + "{\"title\":{\"content\":\"".Length);
        e = e.Substring(0, e.IndexOf("\"},\"metadata\""));

        return CleanText(e);
    }

    public static string GetLatest_SteamUpdate(string rawhtml)
    {
        var e = rawhtml;
        e = e.Substring(e.IndexOf("&quot;event_name&quot;:&quot;") + "&quot;event_name&quot;:&quot;".Length);
        e = e.Substring(0, e.IndexOf("appid&quot"));
        e = e.Substring(0, e.IndexOf("&quot;"));

        return CleanText(e);
    }

    public static string GetLatest_VIZ(string rawhtml)
    {
        var e = rawhtml;
        e = e.Substring(e.IndexOf("o_sortable brdr-dotted-lid"));
        e = e.Substring(0, e.IndexOf("o_read-link-label"));
        e = e.Substring(e.IndexOf("class=\"o_chapter\""));
        e = e.Substring(e.IndexOf("-list-spacing\">") + "-list-spacing\">".Length);
        e = e.Substring(e.IndexOf("div>") + 4);
        e = e.Substring(0, e.IndexOf("</div"));

        return CleanText(e);
    }
    public static string GetLatest_Mangafire(string rawhtml)
    {
        // Initialize a ChromeDriver (make sure you have the Chrome WebDriver installed)
        IWebDriver driver = new ChromeDriver();
        driver.Manage().Window.Minimize();
        // Navigate to the page with dynamic content
        driver.Navigate().GoToUrl(rawhtml);


        // Wait for the dynamic content to load (you could use explicit waits here)
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2))
        {
            PollingInterval = TimeSpan.FromMilliseconds(300),
        };
        wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));

        wait.Until(d => {
            return true;
        });

        // Get the page source once the dynamic content has loaded
        string pageSource = driver.PageSource;

        // Parse the page source with Html Agility Pack
        HtmlDocument document = new HtmlDocument();


        // Use Html Agility Pack to parse the document as usual
        // ...

        // Clean up: close the browser
        driver.Quit();


        var e = pageSource;
        e = e.Substring(e.IndexOf("<button class=\"btn\" type=\"submit\">"));
        e = e.Substring(0, e.IndexOf("</span>"));
        e = e.Substring(e.IndexOf("<span>") + "<span>".Length);


        return CleanText(e);
    }


    public static string GetLatest_Audible(string rawhtml)
    {
        var e = rawhtml;
        e = e.Substring(e.LastIndexOf("product-list-item-"));
        e = e.Substring(0, e.IndexOf(">") - 1);
        e = e.Substring(e.IndexOf("label=") + "label=a".Length);
        return CleanText(e);
    }



    public static string CleanText(string text)
    {
        int x = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == ' ' || text[i] == '\n' || text[i] == '\r')
            {
                continue;
            }
            x = i;
            break;
        }
        text = text.Substring(x);
        x = 0;
        for (int j = 0; j < text.Length; j++)
        {
            int i = (text.Length - 1) - j;
            if (text[i] == ' ' || text[i] == '\n' || text[i] == '\r')
            {
                continue;
            }
            x = j;
            break;
        }

        text = text.Substring(0, text.Length - x);


        return text;
    }



    public static string GetHTMLFromWebsite(string html, string type) // html = https://html-agility-pack.net/from-web
    {
        switch (type)
        {
            case "MF": return html;
            default:
                HtmlWeb web = new HtmlWeb();
                return web.Load(html).Text;
        }
    }

}
[System.Serializable]
public class ImageSexNugget
{
    public string Name;
    public Sprite reebaka;
}