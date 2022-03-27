using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    private GameObject _placeBtn;

    void Start()
    {
        _placeBtn = GameObject.Find("PlaceBtn");
        _placeBtn.SetActive(false);
    }
    public void NextSlide()
    {
        GlobalScript.Info(GlobalScript.SlideInfo[2]);
        _placeBtn.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
