using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSelectorController : MonoBehaviour
{
    [SerializeField] GameLevelData _levelData;
    [SerializeField] TMP_Text _levelText;
    [SerializeField] Image _leftArrow;
    [SerializeField] Image _rightArrow;
    [SerializeField] int _releaseLevelIndex;
    [SerializeField] UnityEvent<int> _onLevelSelect;
    [SerializeField] UnityEvent _onRelease;
    private int _currentSelection;
    private bool _selectLock;

    private void Start() 
    {
        UpdateUI();
    }

    void Update()
    {
        int deltaSelect = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

        if (deltaSelect != 0)
        {
            if (_selectLock == true) return;
            _selectLock = true;
            _currentSelection += deltaSelect;
            _currentSelection = Mathf.Clamp(_currentSelection, 0, _levelData.GetNextLevel());
            UpdateUI();
        }
        else
        {
            _selectLock = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
        {
            if (_currentSelection != _releaseLevelIndex) EnterLevel();
            else _onRelease?.Invoke();
        }
    }

    public void UpdateUI()
    {
        GameLevelData.GameLevel selectedLevel = _levelData.Levels[_currentSelection];

        _levelText.text = selectedLevel.Name;
        _leftArrow.gameObject.SetActive(_currentSelection > 0);
        _rightArrow.gameObject.SetActive(_currentSelection < _levelData.GetNextLevel());
    }

    public void EnterLevel()
    {
        _levelData.CurrentLevel = _currentSelection; 
        _onLevelSelect?.Invoke(_levelData.Levels[_currentSelection].SceneBuildIndex);
    }
}
