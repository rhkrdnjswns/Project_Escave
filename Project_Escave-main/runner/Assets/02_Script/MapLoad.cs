using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] Map = new GameObject[3];

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (this.gameObject.transform.parent == Map[0])
            {
                Instantiate(Map[Random.Range(0, 2)], new Vector3(this.transform.position.x, 0, 0), Quaternion.identity);
            }
            else if (this.gameObject.transform.parent == Map[1])
            {
                Instantiate(Map[Random.Range(0, 2)], new Vector3(this.transform.position.x, 0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(Map[Random.Range(0, 2)], new Vector3(this.transform.position.x, 0, 0), Quaternion.identity);
            }
        }
    }
}
