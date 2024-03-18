using UnityEngine;

public class WarpToPosition : MonoBehaviour
{
    [ContextMenuItem("Set Target To Current Position", "SetTargetToCurrentPosition")]
    [SerializeField] private Vector3 _targetPosition;

    public void SetTargetToCurrentPosition()
    {
        _targetPosition = transform.position;
    }
    
    public void Warp()
    {
        transform.position = _targetPosition;
    }
}
