using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform _elevatorGround;
    [SerializeField] private float _travelSpeed = 5;
    private bool _canElevate = true;

    public void TravelToBase(Transform baseTransform)
    {
        if (!_canElevate) return;

        StartCoroutine(ElevatorTransition(baseTransform.position));
        _canElevate = false;
    }

    IEnumerator ElevatorTransition(Vector3 newPosition)
    {
        while (Vector3.Distance(_elevatorGround.position, newPosition) > 0)
        {
            _elevatorGround.position = Vector3.MoveTowards(_elevatorGround.position, newPosition, _travelSpeed * Time.deltaTime);
            yield return null;
        }
        _canElevate = true;
    }
}
