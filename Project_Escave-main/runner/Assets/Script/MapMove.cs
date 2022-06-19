using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    [SerializeField] private GameObject Character;
    public float mapSpeed = 9f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GameStart()
    {
        StartCoroutine(SpeedUp(3));
        Character.transform.position = new Vector3(-2.8f, -1.15f, 0);
        this.gameObject.transform.position = new Vector3(0, 0, 0);
        DataManager.Instance.score = 0;
    }

    IEnumerator SpeedUp(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        mapSpeed += 0.3f;

        StartCoroutine(SpeedUp(3));
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(-mapSpeed * Time.deltaTime, 0, 0);
    }
}
