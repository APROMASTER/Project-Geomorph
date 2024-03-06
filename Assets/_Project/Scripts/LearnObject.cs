using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LearnObject : MonoBehaviour
{
    [SerializeField] private float _enteringTime = 0.5f;
    [SerializeField] private float _learningTime = 1f;
    [SerializeField] private LayerMask _learnableLayer;
    [SerializeField] private List<LearnableObject> _learnedObjects = new List<LearnableObject>();
    [SerializeField] private UnityEvent _onLearnStart;
    [SerializeField] private UnityEvent<Vector3> _onLearning;
    [SerializeField] private UnityEvent<Vector3> _onLearnComplete;
    [SerializeField] private UnityEvent _onLearnEnd;
    private bool _canLearn = true;

    IEnumerator LearnTransition(LearnableObject targetObject)
    {
        _canLearn = false;
        _onLearnStart?.Invoke();

        float enteringDelta = 1 / _enteringTime;

        // Mover el objeto hacia el centro del objeto Target
        while (Vector3.Distance(transform.position, targetObject.transform.position) > 0.001f)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetObject.transform.position, Time.deltaTime * enteringDelta);
            _onLearning?.Invoke(newPosition);
            yield return null;
        }
        // Aprender el objeto
        yield return new WaitForSeconds(_learningTime);

        _learnedObjects.Add(targetObject);
        while (Physics.SphereCast(transform.position, 0.5f, Vector3.up, out RaycastHit hitInfo, 0.5f, _learnableLayer.value))
        {
            _onLearnComplete?.Invoke(targetObject.Collider.bounds.extents.y * targetObject.transform.up);
            yield return null;
        }
        _onLearnEnd?.Invoke();
        _canLearn = true;
        yield return null;
    }

    public void Learn(LearnableObject learnableObject)
    {
        if (_learnedObjects.Find(x => x.Data.Name == learnableObject.Data.Name) != null 
        || !_canLearn) 
            return;

        StartCoroutine(LearnTransition(learnableObject));
    }

    
}
