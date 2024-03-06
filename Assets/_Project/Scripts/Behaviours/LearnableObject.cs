using UnityEngine;

public class LearnableObject : InteractableObject
{
    public LearnableObjectData Data;
    [HideInInspector] public Collider Collider;

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
    }
}
