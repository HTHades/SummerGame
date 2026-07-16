using Unity.VisualScripting;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [SerializeField] Vector2Int tilePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      GetComponentInParent<WorldSrolling>().Add(gameObject, tilePosition);    
    }

    
}
