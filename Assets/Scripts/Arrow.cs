using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    private bool _collision = false;
    public float ScalePerFrame = 0.1f;

    void Update()
    {
        if (!_collision)
        {
            if (_rigidbody.velocity != new Vector3(0, 0, 0))
            {
                if (transform.localScale.x < 10)
                {
                    transform.localScale += Vector3.one * ScalePerFrame;
                }
                transform.forward = _rigidbody.velocity.normalized;
                transform.Rotate(0, 90, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _collision = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.useGravity = false;
        switch (other.tag)
        {
            case "bullseye":
                GlobalScript.IncressPointsBy(10f);
                break;
            case "target":
                GlobalScript.IncressPointsBy(5 * (1f - (other.transform.position - transform.position).magnitude));
                break;
            case "dansk":
                GlobalScript.ChangeScene("EndScene");
                GlobalScript.Points = 0f;
                break;
        }
    }
}