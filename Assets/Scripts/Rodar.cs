using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodar : MonoBehaviour,IInteract
{
    [SerializeField]
    bool OneTime = false;
    [SerializeField]
    bool Used = false;
    [SerializeField]
    float angulo = 45;
    [SerializeField]
    float tempo = 3;
    bool ARodar = false;
    public void Action()
    {
        if (OneTime && Used) return;
        if (ARodar) return;
        Used = true;
        StartCoroutine("Roda");
    }
    IEnumerator Roda()
    {
        float inc = angulo / tempo;
        float atual = 0;
        ARodar = true;
        while(atual<angulo)
        {
            transform.Rotate(0, inc*Time.deltaTime, 0);
            atual += inc*Time.deltaTime;
            yield return null;
        }
        ARodar = false;
    }
}
