using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)){
            var outro = other.GetComponent<IInteract>();
            if(outro!=null)
                outro.Action();
        }
    }
}
