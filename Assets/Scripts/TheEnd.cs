using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
