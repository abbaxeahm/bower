using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GlobalScript : ScriptableObject
{
    private static TextMeshProUGUI _err;
    private static TextMeshProUGUI _info;
    private static TextMeshProUGUI _pointText;
    private static Placer _placer;
    public static string[] SlideInfo = {
         "",
         "Innan strid är det bra med träning",
         "tryck på placera för att få ut piltavlan",
         "placera på lämpligt ställe genom att trycka placera",
         "skjut genom att dra nedåt och sedan släpp",
         "få 10 poäng och möt danskarna i strid"
         };
    public static float Points;
    public static void Info(string inp)
    {
        _info.text = inp;
    }
    public static void Start()
    {
        _err = GameObject.Find("Err").GetComponent<TextMeshProUGUI>();
        _info = GameObject.Find("Info").GetComponent<TextMeshProUGUI>();
        _pointText = GameObject.Find("Points").GetComponent<TextMeshProUGUI>();
        _placer = GameObject.Find("AR Camera").GetComponent<Placer>();
        Info(SlideInfo[1]);
    }
    public static void Err(string inp)
    {
        _err.text = inp;
    }
    public static void IncressPointsBy(float inp)
    {
        Points += inp;
        _pointText.text = Mathf.Round(Points).ToString();
        if (Points >= 10)
        {
            Info("");
            _placer.PlaceDansk();
        }
    }
    public static void ChangeScene(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}