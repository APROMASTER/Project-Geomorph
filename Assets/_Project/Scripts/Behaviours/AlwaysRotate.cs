using UnityEngine;

public class AlwaysRotate : MonoBehaviour
{
    [SerializeField] Vector3 _applyRotation;
    [SerializeField] UpdateUtils.UpdateType updateType;

    // Update is called once per frame
    void Update()
    {
        float delta = UpdateUtils.UpdateBy(updateType);
        Quaternion xRot = Quaternion.AngleAxis(_applyRotation.x * delta, Vector3.right);
        Quaternion yRot = Quaternion.AngleAxis(_applyRotation.y * delta, Vector3.up);
        Quaternion zRot = Quaternion.AngleAxis(_applyRotation.z * delta, Vector3.forward);
        transform.rotation *= xRot * yRot * zRot;
    }
}
