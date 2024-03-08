using UnityEngine;

public class TransformableEntity : MonoBehaviour
{
    public LearnableObjectData LearnableData { get; set; }
    public Collider2D Collider2D { get => GetComponent<Collider2D>(); }
}
