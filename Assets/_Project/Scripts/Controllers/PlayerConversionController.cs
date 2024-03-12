using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerConversionController : MonoBehaviour
{
    [SerializeField] LearnObject _playerInstance;
    [SerializeField] LearnedObjectData _learnedObjects;
    [SerializeField] private SideScrollerCamera _camera;
    [SerializeField] float _conversionDuration;
    [SerializeField] private UnityEvent _onConversionBegin;
    [SerializeField] private UnityEvent _onConversionEnd;
    private LearnableObjectData _currentForm;
    public LearnableObjectData CurrentForm { get => _currentForm; }
    private GameObject _currentInstance;
    private bool _canConvert = true;

    private void Start() 
    {
        _currentForm = _playerInstance.LearnableData;
        _currentInstance = _playerInstance.gameObject;
    }
    
    public void ConvertTo(LearnableObjectData nextForm)
    {
        if (!_canConvert 
        || _learnedObjects.LearnedObjects.Count == 0 
        || nextForm == _currentForm) 
        return;

        StartCoroutine(ConversionRoutine(nextForm));
    }

    IEnumerator ConversionRoutine(LearnableObjectData newForm)
    {
        _canConvert = false;
        _onConversionBegin?.Invoke();
        GameObject newFormObject;
        if (newForm != _playerInstance.LearnableData)
        {
            newFormObject = Instantiate(newForm.LearnedEntity.gameObject, _currentInstance.transform.position, Quaternion.identity);
        }
        else
        {
            newFormObject = _playerInstance.gameObject;
            newFormObject.transform.position = _currentInstance.transform.position;
            newFormObject.SetActive(true);
        }
        newFormObject.transform.localScale = Vector3.zero;

        if (_currentInstance.TryGetComponent(out Rigidbody2D currentBody))
        {
            currentBody.isKinematic = true;
        }

        if (newFormObject.TryGetComponent(out Rigidbody2D newBody))
        {
            newBody.isKinematic = true;
        }

        float progress = 0;
        while (progress < 1)
        {
            _currentInstance.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, progress);
            newFormObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);

            progress += Time.deltaTime / _conversionDuration;

            if (currentBody != null) currentBody.velocity = Vector2.zero;
            if (newBody != null) newBody.velocity = Vector2.zero;
            yield return null;
        }
        _currentInstance.transform.localScale = Vector3.zero;
        newFormObject.transform.localScale = Vector3.one;

        if (_currentInstance != _playerInstance.gameObject)
        {
            Destroy(_currentInstance);
        }
        else 
        {
            _currentInstance.SetActive(false);
        }
        
        _currentForm = newForm;
        _currentInstance = newFormObject;
        if (newBody != null) newBody.isKinematic = false;
        _camera.SetTarget(newFormObject.transform);
        _onConversionEnd.Invoke();
        _canConvert = true;
        yield return null;
    }

    public void StopPlayer()
    {
        if (_currentInstance.TryGetComponent(out Rigidbody2D _body))
        {
            _body.simulated = false;
        }
    }

    public void ReleasePlayer()
    {
        if (_currentInstance.TryGetComponent(out Rigidbody2D _body))
        {
            _body.simulated = true;
        }
    }
}
