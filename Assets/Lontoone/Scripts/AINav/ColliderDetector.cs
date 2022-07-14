using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ColliderDetector : MonoBehaviour
{
    public List<GameObject> collidersInRange = new List<GameObject>();

    public LayerMask layerMask;
    public event Action<GameObject> mOnTriggerEnter;
    public event Action<GameObject> mOnTriggerStay;
    public event Action<GameObject> mOnTriggerLeave;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER");
        if (!(layerMask == (layerMask | (1 << other.gameObject.layer)))) { return; }
        if (!collidersInRange.Contains(other.gameObject))
        {
            collidersInRange.Add(other.gameObject);
        }
        if (mOnTriggerEnter != null)
            mOnTriggerEnter(other.gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (!(layerMask == (layerMask | (1 << other.gameObject.layer)))) { return; }
        if (mOnTriggerStay != null)
            mOnTriggerStay(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!(layerMask == (layerMask | (1 << other.gameObject.layer)))) { return; }
        if (mOnTriggerLeave != null)
            mOnTriggerLeave(other.gameObject);

        if (collidersInRange.Contains(other.gameObject))
        {
            collidersInRange.Remove(other.gameObject);
        }
    }
}
