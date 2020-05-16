using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTimer : MonoBehaviour
{
    public float time;
    public float timeScale;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        time += timeScale * Time.deltaTime;
    }
}
