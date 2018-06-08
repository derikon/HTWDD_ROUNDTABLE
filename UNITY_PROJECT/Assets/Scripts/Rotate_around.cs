using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotate_around : MonoBehaviour
{
    public GameObject cube;
    public float fireRate = 5.0f;
    private float nextFire = 0.0f;
    public float smooth = 1f;
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
        if (Time.time > nextFire)
        {
            transform.RotateAround(cube.transform.position, new Vector3(0.0f, 0.0f, 0.1f), 100 * Time.deltaTime * speed);
            nextFire = Time.time - fireRate;
        }

        //if (Input.GetKeyDown(KeyCode.S)) // some condition to rotate 180

        //    targetAngles = transform.eulerAngles + 180f * Vector3.down; // what the new angles should be


        //    transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime); // lerp to new angles
        //    i++;



    }
}
