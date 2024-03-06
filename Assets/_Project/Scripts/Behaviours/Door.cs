using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Door : InteractableObject
{
    [SerializeField] Transform _doorPivot;
    [SerializeField] private bool _locked = false;
    [SerializeField] private int _doorId;
    [SerializeField] private float _rotationOffset = 0;
    [SerializeField] private float _openCloseDuration = 0.5f;
    private bool _opened = false;
    [SerializeField] private UnityEvent _onOpen;
    [SerializeField] private UnityEvent _onClose;
    [SerializeField] private UnityEvent _onUnlock;
    [SerializeField] private UnityEvent _onTryingLock;

    private void Start() 
    {
        GameEvents.Instance.OnUnlockDoor += Unlock;
        // Debug.Log("Subscribing");
    }

    private void OnDestroy() 
    {
        GameEvents.Instance.OnUnlockDoor -= Unlock;
        // Debug.Log("Desubscribing");
    }

    public void Unlock(int doorId)
    {
        if (doorId != this._doorId) return;

        _locked = false;
        _onUnlock?.Invoke();
    }

    public override void Interact(Transform interactor)
    {
        if (_locked)
        {
            _onTryingLock?.Invoke();
            return;
        }

        // Open Door
        _opened = !_opened;

        if (_opened) 
        {
            _onOpen?.Invoke();
        }
        else 
        {
            _onClose?.Invoke();
        }

        float doorAngle = _opened ? Mathf.Sign(interactor.position.x - transform.position.x) * 90 : 0;
        StartCoroutine(RotateDoorToAngle(doorAngle + _rotationOffset));
    }

    IEnumerator RotateDoorToAngle(float angle)
    {
        Interactable = false;

        float progress = 0;
        float deltaFromDuration = 1 / _openCloseDuration;

        while (progress < 1)
        {
            progress += Time.deltaTime * deltaFromDuration;
            _doorPivot.rotation = Quaternion.Slerp(_doorPivot.rotation, Quaternion.AngleAxis(angle, Vector3.up), progress);
            yield return null;
        }
        _doorPivot.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        Interactable = true;
    }
}
