using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float distance = 12;
    [SerializeField] Vector2 direction = Vector2.right;
    [SerializeField] Vector2 offset = Vector2.zero;
    private Vector2 endPos;
    // Start is called before the first frame update
    void Start()
    {
        endPos = (Vector2)transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        if (Vector2.Distance(transform.position, endPos) >= distance)
        {
            transform.Rotate(new Vector2(0, 180));
            endPos = transform.position;
        }
    }
}
