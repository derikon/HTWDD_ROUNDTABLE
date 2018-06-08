using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotate_around : MonoBehaviour
{
    public GameObject cube;
    public float speed;



    private Vector3 targetAngles;
    private int i = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //while (this.transform.rotation.z < 180)
        //{
        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, 0, 180), speed * Time.deltaTime);
        //    yield return null;
        //}
        //this.transform.rotation = Quaternion.Euler(0, 0, 180);
        transform.RotateAround(cube.transform.position, new Vector3(0.0f, 0.0f, 0.1f), 100 * Time.deltaTime * speed);


        //if (Input.GetKeyDown(KeyCode.S)) // some condition to rotate 180

        //    targetAngles = transform.eulerAngles + 180f * Vector3.down; // what the new angles should be


        //    transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
        //    i++;



    }
}
