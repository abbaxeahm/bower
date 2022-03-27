using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrowPrefab;
    private GameObject _arrow;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Transform _bowString;
    private bool _first = true;
    public float ForceMultiplier = 20f;
    public void ThrowArrow()
    {
        _arrow.GetComponent<Rigidbody>().AddForce((transform.forward + transform.up * 0.2f) * _slider.value * ForceMultiplier, ForceMode.Impulse);
        _arrow.GetComponent<Rigidbody>().useGravity = true;
        _arrow.transform.SetParent(null);
        _slider.value = 0f;
        Destroy(_arrow, 10);
        if (_first)
        {
            //run the first time
            _first = false;
            GlobalScript.Info(GlobalScript.SlideInfo[5]);
        }
    }

    public void PlaceArrow()
    {
        _arrow = Instantiate(_arrowPrefab, _bowString.position + transform.forward * 0.1f, transform.rotation * Quaternion.Euler(90, 0, 270), _bowString);
        //place arrow in bow
        _arrow.GetComponent<Rigidbody>().useGravity = false;
        _arrow.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
