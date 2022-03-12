using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Class for finding plane info
/// </summary>
public class PlaneFinder : MonoBehaviour
{
    // The text component to write our findings too

    // Subsystem for casting raycasts in AR
    public ARRaycastManager raycastManager;
    public GameObject target;
    public Transform ArOrigin;
    List<ARRaycastHit> hits = new();



    // Subsystem for managing created planes 

    // Update is executed every frame
    // The logic here casts a ray to find out which plane we are looking at
    public void Place()
    {

        GameObject obj = Instantiate(target, new Vector3(transform.position.x, 0, transform.position.z + 3), Quaternion.Euler(0, 270, 0), ArOrigin);
        obj.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
        Transform[] components = obj.GetComponentsInChildren<Transform>();
        foreach (Transform trans in components)
        {
            Debug.Log(trans.position);
            Debug.Log(trans.localPosition);
            Debug.Log(trans.localScale);
        }
        // List for storing the objects that the ray intersects with

        // Casts a ray and returns true if something (TrackableType.PlaneWithinBounds in this case) has been hit
        if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits))
        {
            // Check for making sure the object is of correct type
            // Also checks the first item in the hits list as that should be the first object that intersects with the raycast
            if (hits.Count > 0)// (hits[0].pose.position - transform.position).sqrMagnitude > 9)
            {
                GameObject go = Instantiate(target, new Vector3(transform.position.x, hits[0].pose.position.y, transform.position.z + 3), Quaternion.Euler(90, 0, 0), ArOrigin);
                go.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
            }
        }

    }
}
