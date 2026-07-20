using UnityEngine;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private Transform PointHp;
    public void SetState(float currentValue, float Value)
    {
        Debug.Log(" đã vào hàm setStatus");
        float state = currentValue/Value;
        PointHp.transform.localScale = new Vector3(state, 1f, 1f);
    }
}