using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRotate : MonoBehaviour
{
    public GameObject cube;
    public float speed;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(RotateMe());
    }

    // Update is called once per frame
    void Update()
    {
        while (transform.rotation.z >= -180)
        {
            transform.RotateAround(cube.transform.position, new Vector3(0.0f, 0.0f, -0.1f), 100 * Time.deltaTime * speed);
        }

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
        //yield return new WaitForSeconds(3);

        //while (this.transform.rotation.z > -180)
        //{
        //    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, -180), moveSpeed * Time.time);
        //    yield return null;
        //}
        //this.transform.rotation = Quaternion.Euler(0, 0, -180);


        yield return null;
    }
}
