using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D ballRB;
    public GameObject glObject;
    public int speed = 5;
    public float fallSpeed = 1;
    public float bounce = 10;
    bool isFalling;
    bool changeVelocity;
    private float inAxis;

    private Vector3 lastVelocity;
    private Vector3 goalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        changeVelocity = false;
        // get the rigid body
        ballRB = GetComponent<Rigidbody2D>();
        // start in pick mode
        SetPickMode();
    }
    void FixedUpdate()
    {
        lastVelocity = ballRB.velocity;
        if (changeVelocity)
        {
            changeVelocity = false;
            ballRB.velocity = goalVelocity;
        }
    }
    // Update is called once per frame
    void Update()
    {
        inAxis = Input.GetAxis("Horizontal");
        if (!isFalling)
        {
            // moves the ball left right based on input
            transform.Translate(Vector2.right * Time.deltaTime * speed * inAxis);
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // drop the ball on release
                SetPlayMode();
            }
        }
        // return ball to center if off screen
        if (Mathf.Abs(transform.position.x) > 12 || transform.position.y < - 6)
        {
            SetPickMode();
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
        goalVelocity = new Vector2(inAxis, -fallSpeed);
        changeVelocity = true;
        ballRB.angularVelocity = inAxis;
        ballRB.simulated = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string colName = collision.gameObject.name;
        // bounce off wheel
        if (colName.StartsWith("Wheel"))
        {
            goalVelocity = Vector3.Reflect(lastVelocity * bounce, collision.GetContact(0).normal);
            changeVelocity = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string colName = collision.gameObject.name;
        // increase score if we hit a hat
        if (colName.StartsWith("Hat"))
        {
            GameLogic gl = glObject.GetComponent<GameLogic>();
            gl.score++;
            gl.UpdateScore();
            SetPickMode();
        }
    }
}
