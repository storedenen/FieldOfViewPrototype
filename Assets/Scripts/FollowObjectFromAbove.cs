using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectFromAbove : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private float _followHeight = 15f;

    private void Start() 
    {
        if (_target == null)
        {
            Debug.LogWarning("Disabling component because of missing target!");
            this.enabled = false;
        }
    }

    private void Update()
    {
        this.transform.position = _target.transform.position + _target.transform.up * _followHeight;
    }
}
