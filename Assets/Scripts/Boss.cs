using UnityEngine;

public class Boss : MonoBehaviour
{
    private float boundZ = 11.0f;
    private float boundX = 23.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(ChangePosition), 2.0f, Random.Range(1.0f, 3.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangePosition()
    {
        transform.position = new Vector3(Random.Range(-boundX, boundX), transform.position.y, Random.Range(-boundZ, boundZ));
    }
}
