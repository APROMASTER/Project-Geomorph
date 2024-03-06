using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LearnObject : MonoBehaviour
{
    [SerializeField] private float _enteringTime = 0.5f;
    [SerializeField] private float _learningTime = 1f;
    [SerializeField] private LayerMask _learnableLayer;
    public List<LearnableObject> LearnedObjects = new List<LearnableObject>();
    [SerializeField] private float _endImpulseMagnitude = 20f;
    [SerializeField] private UnityEvent _onLearnStart;
    [SerializeField] private UnityEvent<Vector3> _onLearning;
    [SerializeField] private UnityEvent<Vector3> _onLearnComplete;
    [SerializeField] private UnityEvent _onLearnEnd;
    private bool _canLearn = true;

    IEnumerator LearnTransition(LearnableObject targetObject)
    {
        _canLearn = false;
        _onLearnStart?.Invoke();

        Vector3 initialPosition = transform.position;
        float progress = 0;
        float enteringDelta = 1 / _enteringTime;

        Transform targetTransform = targetObject.transform;
        GameObject _fakeConverted = Instantiate(targetObject.Data.LearnedPrefab, targetTransform.position, targetObject.transform.rotation);
        _fakeConverted.transform.SetParent(targetTransform);
        if (_fakeConverted.TryGetComponent(out Collider2D collider2D))
        {
            collider2D.enabled = false;
        }
        if (_fakeConverted.TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            rigidbody2D.simulated = false;
        }

        // Mover el objeto hacia el centro del objeto Target
        while (Vector3.Distance(transform.position, targetObject.transform.position) > 0.001f)
        {
            progress += Time.deltaTime * enteringDelta;
            Vector3 newPosition = Vector3.Lerp(initialPosition, targetObject.transform.position, progress);
            _onLearning?.Invoke(newPosition);
            yield return null;
        }
        // Aprender el objeto
        yield return new WaitForSeconds(_learningTime);

        LearnedObjects.Add(targetObject);
        while (Physics2D.CircleCast(transform.position, 0.5f, Vector3.up, 0.5f, _learnableLayer.value))
        {
            // Debug.Log("Colliding");
            _onLearnComplete?.Invoke(transform.position + (targetObject.Collider2D.bounds.extents.y * _endImpulseMagnitude * Time.deltaTime * Vector3.up));
            yield return null;
        }
        Destroy(_fakeConverted);
        // Debug.Log("END");
        _onLearnEnd?.Invoke();
        _canLearn = true;
        yield return null;
    }

    public void Learn(LearnableObject learnableObject)
    {
        if (LearnedObjects.Find(x => x.Data.name == learnableObject.Data.name) != null 
        || !_canLearn) 
            return;

        StartCoroutine(LearnTransition(learnableObject));
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
    
}
