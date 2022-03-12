using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    /// <summary>
    /// The object to be created and thrown
    /// The SerializeField property is used to access the variable from the editor (can also be done by making it public)
    /// Using SerializeField is best practice as it means that the variable is meant to be set from the editor
    /// </summary>
    [SerializeField]
    GameObject boxPrefab;
    [SerializeField]
    GameObject forInp;
    [SerializeField]
    GameObject angInp;

    public Slider slider;
    public Camera camera;

    bool coolDown;
    float force = 5f;
    public InputField mainInputField;
    public InputField secInputField;

    /// <summary>
    /// Method to create and propel a box
    /// </summary>
    void start()
    {
        mainInputField.onEndEdit.AddListener(delegate { updateAngel(mainInputField); });
        secInputField.onEndEdit.AddListener(delegate { updateForce(secInputField); });
    }
    public void ThrowBox()
    {
        if (!coolDown)
        {
            force = slider.value * 15;
            slider.value = 0f;
            coolDown = true;

            // transform.position - transform.up * 0.33f Gives us the camera height - 0.33 meters
            GameObject go = Instantiate(boxPrefab, transform.position - transform.up * 0.33f, transform.rotation);
            // transform.forward + transform.up * 0.5f is the direction of the force. In this case its slightly angled up (transform.up * 0.5f) and away from the camera (transform.forward)
            //go.GetComponent<Rigidbody>().AddForce(new Vector3(0, force * Mathf.Sin(angel * Mathf.PI / 180), force * Mathf.Cos(anDegel * Mathf.PI / 180)), ForceMode.Impulse);
            go.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            StartCoroutine(CoolDownRoutine());
            go.transform.localScale = new Vector3(10, 10, 10);
        }
    }

    // IEnumerators are routines which will run over several frames
    // In this case the function will check every frame if it has waited for longer than 0.33 seconds and then set cooldown to false

    public void updateAngel(InputField inp)
    {
        Debug.Log(inp);
        Debug.Log(inp.text);
        camera.fieldOfView = float.Parse(inp.text);
    }

    public void updateForce(InputField inp)
    {
        Debug.Log(inp.text);
        camera.farClipPlane = float.Parse(inp.text);
    }


    IEnumerator CoolDownRoutine()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.33f);

        yield return waitForSeconds;
        coolDown = false;
    }
}
