using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLoad : MonoBehaviour
{
    [SerializeField] private GameObject MapPar;
    [SerializeField] private GameObject[] Map = new GameObject[3];
    [SerializeField] private GameObject Pade;
    private float fadeCount = 0f;

    private void Start()
    {
        //StartCoroutine(FadeOutStart());
        //StartCoroutine(FadeInStart());
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            //StartCoroutine(FadeOutStart());
            //StartCoroutine(FadeInStart());
            this.gameObject.SetActive(false);
            if (this.gameObject.transform.parent == Map[0])
            {
                GameObject temp = Instantiate(Map[Random.Range(0, 2)], 
                    new Vector3(this.transform.position.x, 0, 0), 
                    Quaternion.identity);
                temp.SetActive(true);
                temp.transform.parent = MapPar.transform;
            }
            else if (this.gameObject.transform.parent == Map[1])
            {
                GameObject temp2 = Instantiate(Map[Random.Range(0, 2)], 
                    new Vector3(this.transform.position.x, 0, 0), 
                    Quaternion.identity);
                temp2.SetActive(true);
                temp2.transform.parent = MapPar.transform;
            }
            else
            {
                GameObject temp3 = Instantiate(Map[Random.Range(0, 2)], 
                    new Vector3(this.transform.position.x, 0, 0), 
                    Quaternion.identity);
                temp3.SetActive(true);
                temp3.transform.parent = MapPar.transform;
            }
        }
    }

    /*IEnumerator FadeCorutine()
    {
        Debug.Log("시작");
        fadeCount = Pade.color.a;
        while(fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            Pade.color = new Color(0, 0, 0, fadeCount);
        }
    }*/

    public IEnumerator FadeInStart()
    {
        yield return new WaitForSeconds(2.0f);
        Pade.SetActive(true);
        for (float f = 1f; f > 0; f -= 0.02f)
        {
            Color c = Pade.GetComponent<Image>().color;
            c.a = f;
            Pade.GetComponent<Image>().color = c;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        Pade.SetActive(false);
    }

    //페이드 인
    public IEnumerator FadeOutStart()
    {
        Pade.SetActive(true);
        for (float f = 0f; f < 1; f += 0.02f)
        {
            Color c = Pade.GetComponent<Image>().color;
            c.a = f;
            Pade.GetComponent<Image>().color = c;
            yield return null;
        }
    }
}
