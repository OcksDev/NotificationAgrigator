using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using TMPro;
using UnityEngine;

public class Gamer : MonoBehaviour
{
    public GameObject Spawner;
    public List<GameObject> rere;
    public static List<Dictionary<string, string>> notifs = new List<Dictionary<string, string>>();
    public static Queue<Dictionary<string, string>> notif_q = new Queue<Dictionary<string, string>>();
    public static int cummers = 0;
    public static int idealcummers = -1;
    public List<ImageSexNugget> AllImages = new List<ImageSexNugget>();
    public Dictionary<string, ImageSexNugget> reebankon = new Dictionary<string, ImageSexNugget>();
    public static long Runs = -1;
    public static Dictionary<string, GameObject> Nerds = new Dictionary<string, GameObject>();
    public static List<string> TBDnerds = new List<string>();
    public static List<string> RerollReady = new List<string>();
    public static List<string> Goodies = new List<string>();
    private static bool has_auto_rerolled = false;
    public static Dictionary<int, List<string>> interlacing = new Dictionary<int, List<string>>();
    private void Start()
    {
        bool wankoff = false;
        StartCoroutine(QSmeg());
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
    public IEnumerator QSmeg()
    {
        while (true)
        {
            yield return new WaitUntil(() => { return notif_q.Count > 0; });
            var d = notif_q.Dequeue();
            notifs.Add(d);
            cummers++;
            yield return new WaitForSeconds(0.15f);
        }
    }
    public void NotifSex()
    {
        Instantiate(rere[2], transform.position, Quaternion.identity, rere[1].transform);

        SoundSystem.Instance.PlaySoundWithClipping("n", false, 1, 1f + (continueoius * 0.15f));

        if (continueoius <= 20)
            continueoius++;
        if (BGShex != null) StopCoroutine(BGShex);
        BGShex = StartCoroutine(WankWank());
    }
    public Coroutine BGShex;

    public int continueoius = 0;
    public IEnumerator WankWank()
    {

        var a = rere[0].GetComponent<SpriteRenderer>();
        var c = new Color32(50, 50, 50, 255);
        a.color = c;
        int steps = 25;
        float per = 1f / steps;
        for (int i = 0; i < 50; i++)
        {
            a.color = Color.Lerp(c, Color.black, per * i);
            yield return new WaitForFixedUpdate();
        }
        a.color = Color.black;
        yield return null;
    }
    public void Reroll()
    {
        foreach (var a in Nerds)
        {
            Destroy(a.Value);
        }
        Nerds.Clear();

        StartCoroutine(gamin());
    }
    public static bool hascld = false;
    public IEnumerator gamin()
    {
        var b = Directory.GetFiles($"{FileSystem.Instance.GameDirectory}\\Notifs");
        idealcummers = b.Length;
        foreach (var aa in b)
        {
            if (aa.EndsWith("_wl.txt") || aa.EndsWith("_bl.txt"))
            {
                idealcummers--;
            }
            else
            {
                var data = Converter.StringToDictionary(FileSystem.Instance.ReadFile(aa), System.Environment.NewLine, ": ");
                if (data.ContainsKey("Interlace"))
                {
                    var x = int.Parse(data["Interlace"]);
                    if (!interlacing.ContainsKey(x)) interlacing.Add(x, new List<string>());
                    interlacing[x].Add(data["Website"]);
                }
            }
        }
        hascld = true;
        yield return new WaitForSeconds(0.05f);
        foreach (var a in b)
        {
            //if (!a.Contains("EliB")) continue;
            if (a.EndsWith("_wl.txt") || a.EndsWith("_bl.txt"))
            {
                continue;
            }
            if (Goodies.Contains(a))
            {
                continue;
            }
            else
            {
                Goodies.Add(a);
            }
            new Thread(() => { GetUpdate(a); }).Start();
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(0.1f);
        Debug.Log("waiting...");
        yield return new WaitUntil(() => cummers + RerollReady.Count == idealcummers);
        Debug.Log("reeee now");
        yield return new WaitForSeconds(0.1f);
        has_auto_rerolled = true;
        foreach (var a in RerollReady)
        {
            new Thread(() => { GetUpdate(a); }).Start();
            Debug.Log("Rerollling: " + a);
            yield return new WaitForSeconds(0.025f);
        }

    }


    private int oldtesty = -1;
    public List<Noti> banas = new List<Noti>();
    private void FixedUpdate()
    {
        foreach (var a in TBDnerds)
        {
            var sex = Instantiate(rere[3], Tags.refs["Tack"].transform);
            sex.GetComponent<TextMeshProUGUI>().text = a;
            if (!Nerds.ContainsKey(a)) Nerds.Add(a, sex);
        }
        TBDnerds.Clear();

        rere[4].SetActive(Nerds.Count > 0);

        if (oldtesty != notifs.Count)
        {
            if (notifs.Count > oldtesty && notifs.Count > 0)
            {
                NotifSex();
            }
            var ewank = new List<Dictionary<string, string>>(notifs);
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

    public void CreateNoti(Dictionary<string, string> data)
    {
        var cc = Instantiate(Spawner, transform.position, Quaternion.identity, Tags.refs["Content"].transform).GetComponent<Noti>();
        cc.Data = data;
        cc.Title.text = data["Title"];
        cc.Desc.text = $"New {data["MediaName"]}!";
        cc.NewMedia.text = data["Latest"];
        cc.Dispay.sprite = reebankon[data["Image"]].reebaka;
        banas.Add(cc);
    }


    private static void Main(string[] args)
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
        if (aa.EndsWith("_wl.txt") || aa.EndsWith("_bl.txt"))
        {
            return;
        }
        var data = Converter.StringToDictionary(FileSystem.Instance.ReadFile(aa), System.Environment.NewLine, ": ");

        bool addedtoQ = false;
        var dointer = data.ContainsKey("Interlace");
        var dosnoose = data.ContainsKey("Snoose");
        string ee = "";
        if (dointer && !dosnoose)
        {
            try
            {
                var dd = int.Parse(data["Interlace"]);
                int cur = (int)((Runs + interlacing[dd].IndexOf(data["Website"])) % dd);
                if (cur != 0)
                {
                    ee = data["Latest"];
                    goto gamersmeg;
                }
            }
            catch (Exception eer)
            {
                Debug.LogWarning(eer);
            }
        }

        //Debug.LogError(Converter.DictionaryToString(data, System.Environment.NewLine));

        if (dosnoose)
        {
            List<string> d = Converter.StringToList(data["Snoose"], "/");
            Debug.Log("has snoose!");
            if (d[0] == "") d.RemoveAt(0);
            if (DateTime.Now.Year > int.Parse(d[2]))
            {
                Debug.Log("rem snoose, year");
                data.Remove("Snoose");
            }
            else if (DateTime.Now.Year < int.Parse(d[2]))
            {
                Debug.Log("snoosed year");
                ee = data["Previous"];
                data["Latest"] = ee;
                goto gamersmeg;
            }
            else
            {
                if (DateTime.Now.Month > int.Parse(d[1]))
                {
                    Debug.Log("rem snoose, month");
                    data.Remove("Snoose");
                }
                else if (DateTime.Now.Month < int.Parse(d[1]))
                {
                    Debug.Log("snoosed month");
                    ee = data["Previous"];
                    data["Latest"] = ee;
                    goto gamersmeg;
                }
                else
                {
                    if (DateTime.Now.Day >= int.Parse(d[0]))
                    {
                        Debug.Log("rem snoose, day");
                        data.Remove("Snoose");
                    }
                    else
                    {
                        ee = data["Previous"];
                        data["Latest"] = ee;
                        Debug.Log("snoosed day");
                        goto gamersmeg;
                    }
                }
            }
        }
        /*if (data["Type"] != "STM")
        {
        
            cummers++;
            return;
        }*/
        var e = GetHTMLFromWebsite(data["Website"], data["Type"]);




        //Console.WriteLine($"[[{GetLatest_RoyalRoad(e)}]]");

        int retry = 2; // disabled
    rett:
        try
        {
            switch (data["Type"])
            {
                case "VIZ": ee = GetLatest_VIZ(e); break;
                case "RR": ee = GetLatest_RoyalRoad(e); break;
                case "LVC": ee = GetLatest_Livechart(e); break;
                case "YT": ee = GetLatest_Youtube(data["Website"], e); break;
                case "YTM": ee = GetLatest_YoutubeMusic(e); break;
                case "YTT": ee = GetLatest_YoutubeMusicTopic(e); break;
                case "STM": ee = GetLatest_SteamUpdate(e); break;
                case "MF": ee = GetLatest_Mangafire(e); break; // requires baldness
                case "AUD": ee = GetLatest_Audible(e); break;
                case "STMS": ee = GetLatest_SteamSale(e); break;
                //case "YT": ee = "GAMING"; break;
                default: Debug.LogError("Invalid type"); break;
            }
            //WebsiteSattsus[data["Type"]] = "<B><color=green>Good</color></B>";
        }
        catch (Exception eeez)
        {
            if (retry <= 1)
            {
                Debug.LogWarning(data["Title"] + ", " + data["Website"]);
                Thread.Sleep(500);
                retry++;
                goto rett;
            }
            else
            {
                if (has_auto_rerolled)
                {
                    TBDnerds.Add(data["Title"]);
                    Debug.LogError(data["Title"] + ", " + data["Website"] + "\n" + eeez);
                }
                else
                {
                    RerollReady.Add(aa);
                    Debug.LogWarning(data["Title"] + ", " + data["Website"]);
                    Goodies.Remove(aa);
                }
                return;
            }
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

        var examp = aa.Substring(0, aa.IndexOf(".txt"));
        string white = $"{examp}_wl.txt";
        string black = $"{examp}_bl.txt";
        if (File.Exists(white))
        {
            bool yeet = true;
            var d = File.ReadAllText(white);
            var l = d.Split(Environment.NewLine);
            foreach (var reg in l)
            {
                if (reg == "" || reg == " ") continue;
                var dd = Regex.Match(ee, reg);
                if (dd.Success)
                {
                    yeet = false;
                    break;
                }
            }
            if (yeet) goto yeetus;
        }
        if (File.Exists(black))
        {
            bool yeet = false;
            var d = File.ReadAllText(black);
            var l = d.Split(Environment.NewLine);
            foreach (var reg in l)
            {
                if (reg == "" || reg == " ") continue;
                var dd = Regex.Match(ee, reg);
                if (dd.Success)
                {
                    yeet = true;
                    break;
                }
            }
            if (yeet) goto yeetus;
        }
    gamersmeg:
        if (ee != data["Latest"] || data["Latest"] != data["Previous"])
        {
            if (ee != data["Previous2"])
            {
                //use events to do a clalback?
                //if not then just a static list and just append self to its
                data["Latest"] = ee;
                if (ee != "<NOUPDATE>")
                {
                    addedtoQ = true;
                    notif_q.Enqueue(data);
                }
            }
        }
    yeetus:
        if (!addedtoQ)
        {
            cummers++;
        }
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

    public static string GetLatest_Livechart(string rawhtml)
    {
        var e = rawhtml;
        e = e.Substring(0, e.IndexOf("<meta name"));
        e = e.Substring(0, e.IndexOf(" Anime"));
        e = e.Substring(e.IndexOf("<title>") + "<title>".Length);
        return CleanText(e);
    }
    public static string GetLatest_Youtube(string w, string rawhtml)
    {
        try
        {
            var e = rawhtml;
            e = e.Substring(e.IndexOf("\"title\":{\"runs\":[{\"text\":\""));
            e = e.Substring(0, e.IndexOf("\"}],\"accessibility\""));
            e = e.Substring(e.IndexOf("text") + 7);

            return CleanText(e);
        }
        catch
        {
            var e = rawhtml;
            e = e.Substring(e.IndexOf("lockupMetadataViewModel"));
            e = e.Substring(0, e.IndexOf("\"},\""));
            e = e.Substring(e.IndexOf("\"content\":\"") + "\"content\":\"".Length);
            return CleanText(e);
        }
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

    public static string GetLatest_SteamSale(string rawhtml)
    {
        var e = rawhtml;
        e = e.Substring(e.IndexOf("<div class=\"game_area_purchase_game_wrapper\">") + "<div class=\"game_area_purchase_game_wrapper\">".Length);
        e = e.Substring(0, e.IndexOf("<div data-panel=\"[]\""));
        if (!e.Contains("discount_block"))
        {
            return "<NOUPDATE>";
        }
        var perc = e.Substring(e.IndexOf("discount_pct\">") + "discount_pct\">".Length);
        perc = perc.Substring(0, perc.IndexOf("</div>"));
        var orig = e.Substring(e.IndexOf("discount_original_price\">") + "discount_original_price\">".Length);
        orig = orig.Substring(0, orig.IndexOf("</div>"));
        var newp = e.Substring(e.IndexOf("discount_final_price\">") + "discount_final_price\">".Length);
        newp = newp.Substring(0, newp.IndexOf("</div>"));

        return $"[{perc}]: {orig} -> {newp}";
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

        wait.Until(d =>
        {
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