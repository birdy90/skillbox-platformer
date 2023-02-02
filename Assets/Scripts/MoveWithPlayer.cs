using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MoveWithPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.CompareTag(Constants.PlayerTag))
        {
            col.collider.transform.SetParent(transform, true);
        }
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.gameObject.CompareTag(Constants.PlayerTag))
        {
            col.collider.transform.SetParent(null, true);
        }
    }
}
