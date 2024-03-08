using TMPro;
using UnityEngine;

public class ConversionSelectorUIController : MonoBehaviour
{
    [SerializeField] private PlayerConversionController _conversionController;
    [SerializeField] private ConversionSelectorController _selectorController;
    [SerializeField] private UIRotator2D selectionCircle;
    [SerializeField] private TMP_Text _centerText;
    [SerializeField] private TMP_Text _leftText;
    [SerializeField] private TMP_Text _rightText;

    public void OpeningSelector()
    {
        selectionCircle.Reset();
        _centerText.text = _conversionController.CurrentForm.name;
    }

    public void UpdatingSelector(bool canSelect)
    {
        if (canSelect)
        {
            selectionCircle.Reset();
            _centerText.text = _selectorController.GetCurrentLearnableData().name;
        }
    }

    public void ChangingSelection(int deltaSelection)
    {
        selectionCircle.RotateToInAngles(90 * deltaSelection);
        int nextSelectionIndex = _selectorController.Selection + deltaSelection;
        string nextSelection = _selectorController.GetLearnableDataByIndex(nextSelectionIndex).name;

        if (deltaSelection > 0)
        {
            _rightText.text = nextSelection;
        }
        else
        {
            _leftText.text = nextSelection;
        }
    }
}
