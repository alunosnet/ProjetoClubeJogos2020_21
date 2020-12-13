using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMensagem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Messages.instance.showMessage("Don't go in the red water.", Color.red, 3);
    }
}
