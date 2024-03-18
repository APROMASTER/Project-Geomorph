using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] InteractableObject _interactable;
    private void OnTriggerEnter(Collider other) => EvaluateTrigger(other);
    private void OnTriggerExit(Collider other) => DeCheckTrigger(other);
    private void OnTriggerEnter2D(Collider2D other) => EvaluateTrigger(other);
    private void OnTriggerExit2D(Collider2D other) => DeCheckTrigger(other);

    private void Update() 
    {
        if (_interactable == null) return;

        if (Input.GetKeyDown(KeyCode.E) && _interactable.Interactable == true)
        {
            _interactable.Interact(transform);
        }
    }

    private void EvaluateTrigger(Collider2D other)
    {
        // Check if is colliding with a interactable trigger
        if (other.TryGetComponent(out InteractableObject newInteractable))
        {
            if (newInteractable.Disabled == true) return;
            if (_interactable == null)
            {
                _interactable = newInteractable;
            }
            else
            {
                Vector2 currentPosition = transform.position;
                if (Vector2.Distance(currentPosition, other.transform.position) < Vector2.Distance(currentPosition, _interactable.transform.position))
                {
                    _interactable = newInteractable;
                }
            }
        }
    }

    private void EvaluateTrigger(Collider other)
    {
        // Check if is colliding with a interactable trigger
        if (other.TryGetComponent(out InteractableObject newInteractable))
        {
            if (newInteractable.Disabled == true) return;
            if (_interactable == null)
            {
                _interactable = newInteractable;
            }
            else
            {
                Vector2 currentPosition = transform.position;
                if (Vector2.Distance(currentPosition, other.transform.position) < Vector2.Distance(currentPosition, _interactable.transform.position))
                {
                    _interactable = newInteractable;
                }
            }
        }
    }

    private void DeCheckTrigger(Collider2D other)
    {
        // Check if is colliding with a interactable trigger
        if (other.TryGetComponent(out InteractableObject interactable))
        {
            if (_interactable == interactable)
            {
                _interactable = null;
            }
        }
    }

    private void DeCheckTrigger(Collider other)
    {
        // Check if is colliding with a interactable trigger
        if (other.TryGetComponent(out InteractableObject interactable))
        {
            if (_interactable == interactable)
            {
                _interactable = null;
            }
        }
    }

}
