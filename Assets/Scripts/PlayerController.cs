using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _maxDistance = 50f;
    
    [SerializeField]
    private LayerMask _clickRayMask = 0;
    
    [SerializeField]
    private Camera _playerCamera;

    
    private NavMeshAgent _navAgent;

    private void Start()
    {
        if (_playerCamera == null)
        {
            _playerCamera = Camera.main;
        }

        _navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray clickRay = _playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitResult;

            if (Physics.Raycast(clickRay, out hitResult, _maxDistance, _clickRayMask))
            {
                _navAgent.SetDestination(hitResult.point);
            }
        }
    }
}
