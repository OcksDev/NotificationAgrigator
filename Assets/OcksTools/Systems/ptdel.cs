using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ptdel : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(MyDeath());
    }
    public IEnumerator MyDeath()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
