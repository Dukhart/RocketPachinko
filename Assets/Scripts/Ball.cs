using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D ballRB;
    [SerializeField] GameObject glObject;
    [SerializeField] int speed = 5;
    bool isFalling;
    float inAxis;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigid body
        ballRB = GetComponent<Rigidbody2D>();
        // start in pick mode
        SetPickMode();
    }
    // Update is called once per frame
    void Update()
    {
        inAxis = Input.GetAxis("Horizontal");
        if (!isFalling)
        {
            GetMovement();
        }
        // return ball to center if below screen
        else if (transform.position.y < - 6)
        {
            SetPickMode();
        }
    }
    void GetMovement()
    {
        // moves the ball left right based on input
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        // prevent moving off screen
        if (pos.x < 0 || pos.x > 1) inAxis = -inAxis;
        // apply movment
        transform.Translate(Vector2.right * Time.deltaTime * speed * inAxis);
        if (Input.GetButton("Fire1"))
        {
            // drop the ball on release
            SetPlayMode();
        }
    }
    // lets the player pick where the ball should fall
    void SetPickMode()
    {
        ballRB.simulated = false;
        transform.position = new Vector3(0, 4.3f);
        transform.rotation = Quaternion.identity;
        isFalling = false;
    }

    // lets the ball fall
    void SetPlayMode()
    {
        isFalling = true;
        // reset velocity or the ball will shoot out with old velocity
        ballRB.velocity = Vector3.down * speed;
        ballRB.angularVelocity = inAxis;
        ballRB.simulated = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string colName = collision.gameObject.name;
        // increase score if we hit a hat
        if (colName.StartsWith("Hat"))
        {
            GameLogic gl = glObject.GetComponent<GameLogic>();
            gl.Score += 1;
            SetPickMode();
        }
    }
}
