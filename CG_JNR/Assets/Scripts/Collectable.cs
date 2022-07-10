using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public Transform myPrefab;
    public float speed = 50;
    GameObject[] collectiblearr;
    GameObject go;

    public Text CurScore;
    public float Score;

    // Start is called before the first frame update
    void Start()
    {
        collectiblearr = new GameObject[10];
        for (int i = 0; i < 10; i++)
        {
            var position = new Vector3(Random.Range(-5.0f, 5.0f), 0.5f, Random.Range(-5.0f, 5.0f));
            go = (Instantiate(myPrefab, position, Quaternion.identity)).gameObject;
            collectiblearr[i] = go;
        }
    }

    public void Update()
    {

    }

    // Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}