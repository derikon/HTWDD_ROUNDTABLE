using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotate_around : MonoBehaviour
{
    public GameObject cube;
    public float speed;

    public float rotationSpeed = 8f;
    public float startPosition;
    public float deltaPosition;

    private Vector3 targetAngles;
    private int i = 0;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(RotateMe());


    }

    IEnumerator RotateMe()
    {
        float moveSpeed = 0.1f;
        while (this.transform.rotation.z > 0)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, 0), moveSpeed * Time.time);
            yield return null;
        }
        this.transform.rotation = Quaternion.Euler(0, 0, 0);

        //Wait for 4 seconds
        yield return new WaitForSeconds(3);

        while (this.transform.rotation.z > -180)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, -180), moveSpeed * Time.time);
            yield return null;
        }
        this.transform.rotation = Quaternion.Euler(0, 0, -180);


        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        //while (this.transform.rotation.z < 179)
        //{
        //    transform.RotateAround(cube.transform.position, new Vector3(0.0f, 0.0f, -0.1f), 100 * Time.deltaTime * speed);
        //}
        transform.RotateAround(cube.transform.position, new Vector3(0.0f, 0.0f, -0.1f), 100 * Time.deltaTime * speed);








    }
}
