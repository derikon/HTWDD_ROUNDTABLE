using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRotate : MonoBehaviour
{
    public GameObject rotationpoint;
    public float speed = 0.5f;
    public float zPosition;
    private bool isRising;
    private bool prevRising;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(RotateName());
        zPosition = transform.rotation.z;
        //Debug.Log("Start Z:" + zPosition);
    }

    // Update is called once per frame
    void Update()
    {
        zPosition = transform.rotation.z;
        //Debug.Log("Aktuelle Z:" + zPosition);

    }

    IEnumerator RotateName()
    {
        yield return new WaitForSeconds(3);
        while (zPosition <= 0.99999)
        {

            transform.RotateAround(rotationpoint.transform.position, new Vector3(0.0f, 0.0f, 0.1f), 100 * Time.deltaTime * speed);
            //Debug.Log("Position Z: " + transform.rotation.z);
            yield return null;
        }
    }


}
