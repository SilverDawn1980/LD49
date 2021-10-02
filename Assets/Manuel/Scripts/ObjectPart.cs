using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPart : MonoBehaviour
{
    private void OnMouseDown()
    {
        transform.parent.GetComponent<FragileObject>().AddDamage(3);
    }
}
