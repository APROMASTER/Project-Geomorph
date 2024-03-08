using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConversionSelectorController : MonoBehaviour
{
    [SerializeField] private LearnObject _playerInstance;
    [SerializeField] private float _openCloseTime = 0.6f;
    [SerializeField] private float _selectingTime = 0.2f;
    [SerializeField] private UnityEvent _onOpenSelectMenu;
    [SerializeField] private UnityEvent _onCloseSelectMenu;
    [SerializeField] private UnityEvent<bool> _onSelectionStateChange;
    [SerializeField] private UnityEvent<int> _onSelectionDelta;
    [SerializeField] private UnityEvent<LearnableObjectData> _onSelection;
    private int _selection;
    [SerializeField] private bool _toggleLock = false;
    public int Selection { get => _selection; }
    public LearnableObjectData GetCurrentLearnableData() => (_selection == 0) ? _playerInstance.LearnableData : _playerInstance.LearnedObjects[_selection - 1];
    public LearnableObjectData GetLearnableDataByIndex(int indexSelection) 
    {
        if (_playerInstance.LearnedObjects.Count > 0)
        { 
            if (indexSelection > _playerInstance.LearnedObjects.Count) indexSelection = 0;
            else if (indexSelection < 0) indexSelection = _playerInstance.LearnedObjects.Count;
        }
        else indexSelection = 0;

        return (indexSelection == 0) ? _playerInstance.LearnableData : _playerInstance.LearnedObjects[indexSelection - 1];
    }

    private bool _canToggleMenu = true, _canSelect = false;

    void Update()
    {
        if (!_canToggleMenu) return;
        if (Input.GetKey(KeyCode.Q)) 
        {
            if (!_toggleLock) StartCoroutine(ToggleMenu());
        }
        else
        {
            _toggleLock = false;
        }
        if (!_canSelect || !_canToggleMenu) return;

        int deltaSelection = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        if (deltaSelection == 0) return;

        StartCoroutine(Select(deltaSelection));
    }

    IEnumerator ToggleMenu()
    {
        _canToggleMenu = false;
        _toggleLock = true;

        _canSelect = !_canSelect;
        if (_canSelect == false) // NEW FORM SELECTED!
        {
            LearnableObjectData selectedForm = GetCurrentLearnableData();
            _onCloseSelectMenu?.Invoke();
            _onSelection?.Invoke(selectedForm);
        }
        else 
        {
            _onOpenSelectMenu?.Invoke();
        }
        _onSelectionStateChange?.Invoke(_canToggleMenu);
        
        float progress = 0, deltaProgress = 1 / _openCloseTime;
        while (progress < 1)
        {
            //Transition Event?
            progress += Time.deltaTime * deltaProgress;
            yield return null;
        }
        _canToggleMenu = true;
        _onSelectionStateChange?.Invoke(_canToggleMenu);
        yield return null;
    }

    public IEnumerator Select(int deltaSelection)
    {
        _canToggleMenu = false;
        
        int newSelection = _selection + deltaSelection;
        _onSelectionDelta?.Invoke(deltaSelection);
        if (_playerInstance.LearnedObjects.Count > 0)
        { 
            if (newSelection > _playerInstance.LearnedObjects.Count) newSelection = 0;
            else if (newSelection < 0) newSelection = _playerInstance.LearnedObjects.Count;
        }
        else newSelection = 0;
        _onSelectionStateChange?.Invoke(_canToggleMenu);
        // Debug.Log("newSelection!: " + newSelection);

        float progress = 0, deltaProgress = 1 / _selectingTime;
        while (progress < 1)
        {
            //Transition Event?
            progress += Time.deltaTime * deltaProgress;
            yield return null;
        }
        _selection = newSelection;
        _canToggleMenu = true;
        _onSelectionStateChange?.Invoke(_canToggleMenu);
        yield return null;
    }
}
