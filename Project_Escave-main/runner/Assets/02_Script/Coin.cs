using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ScoreUp(1));
    }
    IEnumerator ScoreUp(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        DataManager.Instance.score += 1;

        StartCoroutine(ScoreUp(1));
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.CompareTo("Player") == 0)
        {
            DataManager.Instance.score += 1;
            gameObject.SetActive(false);
        }
    }*/
}
