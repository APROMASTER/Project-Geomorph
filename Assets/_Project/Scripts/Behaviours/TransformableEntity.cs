using UnityEngine;

public class TransformableEntity : MonoBehaviour
{
    [SerializeField] private LearnableObjectData _learnableData;
    public LearnableObjectData LearnableData { get => _learnableData; }
    public Collider2D Collider2D { get => GetComponent<Collider2D>(); }
}
