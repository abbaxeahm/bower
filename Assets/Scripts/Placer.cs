using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

/// <summary>
/// Class for placing object 
/// </summary>
public class Placer : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager _raycastManager;
    [SerializeField]
    private GameObject _targetPrefab;
    [SerializeField]
    private GameObject _danskPrefab;
    [SerializeField]
    private Transform _AROrigin;
    [SerializeField]
    private Material[] _targetMaterial;
    private bool _ghost = false;
    private bool _wantsToBePlaced = false;
    private GameObject _target;
    private GameObject _slider;
    private GameObject _bow;
    List<ARRaycastHit> hits = new();


    private void Start()
    {
        GlobalScript.Start();
        _slider = GameObject.Find("Slider");
        _slider.SetActive(false);
        _bow = GameObject.Find("Bow");
        _bow.SetActive(false);
    }
    private void Update()
    {


        if ((_ghost || _wantsToBePlaced) && _raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits))
        {
            if (hits.Count > 0)
            {
                if (_wantsToBePlaced)
                {
                    _target = Instantiate(_targetPrefab, hits[0].pose.position, transform.rotation * Quaternion.Euler(0, 270, 0), _AROrigin);
                    //place where the ray collide with plane and rotate 270 deg.
                    _ghost = true;
                    _target.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
                    foreach (Material material in _targetMaterial)
                    {
                        material.SetColor("_Color", new Color(1, 1, 1, .4f));
                    }
                    //change alpha of all materials in target
                    _wantsToBePlaced = false;
                }
                if (_ghost)
                {
                    _target.transform.position = hits[0].pose.position;
                    _target.transform.rotation = transform.rotation * Quaternion.Euler(0, 270, 0);
                    // moves target to new intersect betw 
                }
            }
        }
    }
    public void Place()
    {
        if (_ghost)
        {
            if ((_target.transform.position - transform.position).sqrMagnitude > 4)
            {
                GlobalScript.Info(GlobalScript.SlideInfo[4]);
                _ghost = false;
                foreach (Material material in _targetMaterial)
                {
                    material.SetColor("_Color", new Color(1, 1, 1, 1));
                }
                GameObject.Find("PlaceBtn").SetActive(false);
                _slider.SetActive(true);
                _bow.SetActive(true);
            }
            else GlobalScript.Err("för nära (minst 2 meter ifrån)\n");
        }
        else
        {
            _wantsToBePlaced = true;
            GlobalScript.Info(GlobalScript.SlideInfo[3]);
        }
    }
    public void PlaceDansk()
    {
        foreach (GameObject arrow in GameObject.FindGameObjectsWithTag("arrow"))
        {
            Destroy(arrow);
        }
        Instantiate(_danskPrefab, _target.transform.position, _target.transform.rotation * Quaternion.Euler(0, 270, 0), _AROrigin);
        Destroy(_target);
    }
}