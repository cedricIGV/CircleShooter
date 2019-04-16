using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animAutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(KillOnAnimationEnd());
    }
}
