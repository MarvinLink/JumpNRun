using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectablerotation : MonoBehaviour
{
    void FixedUpdate()
    {
        float randomFloat = UnityEngine.Random.Range(-1f, 1f);
        transform.Rotate(new Vector3(0, 5, 0));
    }
}
