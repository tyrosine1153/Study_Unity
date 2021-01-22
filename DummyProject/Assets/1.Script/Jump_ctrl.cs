using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump_ctrl : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            //asdf
        }
    }
}
