using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;
    private GameObject[] _bowJointsL;
    private GameObject[] _bowJointsR;
    private float _stringY;
    private float _slideValue = 0f;
    public int rotation;
    // Start is called before the first frame update
    void Start()
    {
        _bowJointsL = GameObject.FindGameObjectsWithTag("bowJointL");
        _bowJointsR = GameObject.FindGameObjectsWithTag("bowJoint");
        _stringY = transform.localPosition.y;

    }
    public void UpdateBow()
    {
        transform.position += transform.up * (_slider.value - _slideValue) * 0.0625f;
        foreach (GameObject bowJoint in _bowJointsR)
        {
            bowJoint.transform.localRotation = bowJoint.transform.localRotation * Quaternion.Euler(0, 0, rotation * (transform.localPosition.y - _stringY));
        }
        foreach (GameObject bowJoint in _bowJointsL)
        {
            bowJoint.transform.localRotation = bowJoint.transform.localRotation * Quaternion.Euler(0, 0, rotation * (_stringY - transform.localPosition.y));
        }

        _stringY = transform.localPosition.y;
        _slideValue = _slider.value;
    }
}
