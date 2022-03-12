using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelayed : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidBody;
    float lifetime = 10;
    bool collision = true;

    void Start()
    {
        // destroys the object after a set time
        Destroy(gameObject, lifetime);
        Debug.Log(Quaternion.Euler(0, 90, 0));
        Debug.Log(rigidBody.velocity.normalized);
        Debug.Log(Mathf.Sqrt(0f));
    }
    void Update()
    {
        if (collision)
        {
            /*Debug.Log(rigidBody.velocity);
            Debug.Log(rigidBody.velocity);
            Debug.Log(-180 / Mathf.PI * Mathf.Atan(rigidBody.velocity.y / Mathf.Sqrt(Mathf.Pow(rigidBody.velocity.z, 2) + Mathf.Pow(rigidBody.velocity.x, 2))));
            if (rigidBody.velocity.z == 0 && rigidBody.velocity.x == 0)
            {
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, -90);

            }
            else transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, -180 / Mathf.PI * Mathf.Atan(rigidBody.velocity.y / Mathf.Sqrt(Mathf.Pow(rigidBody.velocity.z, 2) + Mathf.Pow(rigidBody.velocity.x, 2))));
            */
            transform.forward = rigidBody.velocity.normalized;
            transform.Rotate(0, 90, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collision = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.useGravity = false;
    }


}
