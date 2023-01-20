using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputSceneBehaviour : MonoBehaviour
{
    public float delay;
    public int splitId = 2, netId = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(delay < 0 || Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene(splitId);
        }

        delay -= Time.deltaTime;
    }
}
