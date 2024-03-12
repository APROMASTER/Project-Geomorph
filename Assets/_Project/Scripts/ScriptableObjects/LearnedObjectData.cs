using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LearnedObjectData : ScriptableObject
{
    [ContextMenuItem(name: "Forget All", function: "ForgetAll")]
    public List<LearnableObjectData> LearnedObjects = new List<LearnableObjectData>();
    // [AddComponentMenu("ForgetAll")]
    public void ForgetAll() => LearnedObjects.Clear();
}
