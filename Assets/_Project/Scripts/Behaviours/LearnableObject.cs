using UnityEngine;

public class LearnableObject : InteractableObject
{
    public LearnableObjectData Data;
    [HideInInspector] public Collider Collider;
    [HideInInspector] public Collider2D Collider2D;

    public override void Interact(Transform interactor)
    {
        if (interactor.TryGetComponent(out LearnObject learnComponent))
        {
            learnComponent.Learn(this);
        }
    }

    private void Start() 
    {
        Collider = GetComponent<Collider>();
        Collider2D = GetComponent<Collider2D>();
    }
}
