using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUtility : MonoBehaviour
{
    public bool isDestroy { get; set; }
    private void FixedUpdate()
    {
        if (isDestroy)
        {
            transform.Rotate(Vector3.back * 100 * Time.deltaTime * 20);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(100, 100), Time.deltaTime * 20);
        }
    }
}
