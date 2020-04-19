using UnityEngine;

[CreateAssetMenu(menuName = "Items", fileName = "Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private GameObject _prefab;

    public GameObject Prefab => _prefab;
}