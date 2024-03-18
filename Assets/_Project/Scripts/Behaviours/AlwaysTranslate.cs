using UnityEngine;

public class AlwaysTranslate : MonoBehaviour
{
    [SerializeField] private Vector3 _translate;
    
    void Update()
    {
        transform.Translate(Time.deltaTime * _translate);
    }
}
