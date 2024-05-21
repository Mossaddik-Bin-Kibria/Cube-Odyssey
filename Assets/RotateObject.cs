using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 rotation;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotation *1 * Time.deltaTime);
    }
}
