using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotate_around : MonoBehaviour
{
    public GameObject rotationPoint;
    public float speed;
    public float zPosition;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(RotateMe());
        zPosition = transform.rotation.z;
        //Debug.Log("Topic Start:" + zPosition);
    }


    // Update is called once per frame
    void Update()
    {
        //transform.RotateAround(cube.transform.position, new Vector3(0.0f, 0.0f, -0.1f), 100 * Time.deltaTime * speed);
        //zPosition = transform.rotation.z;
        //Debug.Log("Topic aktuell:" + zPosition);
    }


    IEnumerator RotateMe()
    {
        while (this.transform.rotation.z >= -0.9999)
        {
            //yield return new WaitForSeconds(4);
            transform.RotateAround(rotationPoint.transform.position, new Vector3(0.0f, 0.0f, -0.1f), 100 * Time.deltaTime * speed);
            zPosition = transform.rotation.z;
            //Debug.Log("Topic aktuell:" + zPosition);
            yield return null;
        }
        yield return null;
    }
}
