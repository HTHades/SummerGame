using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    private float floatSpeed;
    void Start()
    {
        floatSpeed = Random.Range(0.1f, 1.5f);
        Destroy(gameObject, 1f);
    }
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * floatSpeed;
    }
    public void SetValue( int value)
    {
        damageText.text = value.ToString();
    }
}
