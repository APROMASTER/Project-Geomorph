using UnityEngine;

[CreateAssetMenu(fileName = "NewLearnableObjectData", menuName = "ScriptableObjects/LearnableObjectData")]
public class LearnableObjectData : ScriptableObject
{
    public TransformableEntity LearnedEntity;

    private void OnEnable() 
    {
        //LearnedEntity.LearnableData = this;
    }
}
