using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoraRodar : MonoBehaviour
{
    public float velocidadeRodar = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, velocidadeRodar*Time.deltaTime, 0);
    }
}
