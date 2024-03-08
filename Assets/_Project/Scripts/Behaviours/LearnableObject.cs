using UnityEngine;

public class LearnableObject : InteractableObject
{
    [SerializeField] private LearnableObjectData _learnableData;
    public LearnableObjectData LearnableData { get => _learnableData; }
    public Collider2D Collider2D { get => GetComponent<Collider2D>(); }

    public override void Interact(Transform interactor)
    {
        if (interactor.TryGetComponent(out LearnObject learnComponent))
        {
            learnComponent.Learn(this);
        }
    }
}
