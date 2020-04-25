using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    void Update()
    {
        transform.LookAt(_target);
    }
}
