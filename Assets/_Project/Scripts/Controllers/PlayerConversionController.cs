using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerConversionController : MonoBehaviour
{
    [SerializeField] float _conversionDuration;
    [SerializeField] LearnObject _learnObject;
    [SerializeField] private SideScrollerCamera _camera;
    [SerializeField] private UnityEvent _onChooseBegin;
    [SerializeField] private UnityEvent _onChooseEnd;
    [SerializeField] private GameObject _currentForm;
    private bool _canConvert = true;

    private void Start() 
    {
        _currentForm = _learnObject.gameObject;
    }
    
    private void Update()
    {
        if (!_canConvert || _learnObject.LearnedObjects.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(Convert(_learnObject.LearnedObjects[0].Data.LearnedPrefab));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(Convert(_learnObject.gameObject));
        }
    }

    IEnumerator Convert(GameObject newForm)
    {
        _canConvert = false;
        GameObject newFormObject;
        if (newForm != _learnObject.gameObject)
        {
            newFormObject = Instantiate(newForm, _learnObject.transform.position, _learnObject.transform.rotation);
        }
        else
        {
            newFormObject = _learnObject.gameObject;
            newFormObject.transform.position = _currentForm.transform.position;
            newFormObject.SetActive(true);
        }
        newFormObject.transform.localScale = Vector3.zero;

        if (_currentForm.TryGetComponent(out Rigidbody2D currentBody))
        {
            currentBody.simulated = false;
        }

        if (newFormObject.TryGetComponent(out Rigidbody2D newBody))
        {
            newBody.simulated = false;
        }

        float progress = 0;
        while (progress < _conversionDuration)
        {
            _currentForm.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, progress);
            newFormObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);

            progress += Time.deltaTime;
            yield return null;
        }
        _currentForm.transform.localScale = Vector3.zero;
        newFormObject.transform.localScale = Vector3.one;

        if (_currentForm != _learnObject.gameObject)
        {
            Destroy(_currentForm);
        }
        else 
        {
            _currentForm.SetActive(false);
        }

        _currentForm = newFormObject;
        newBody.simulated = true;
        _camera.SetTarget(newFormObject.transform);
        _canConvert = true;
        yield return null;
    }

    public void ChooseForm()
    {
        _onChooseBegin?.Invoke();
    }

    void Confirm()
    {
        _onChooseEnd?.Invoke();
    }
}
