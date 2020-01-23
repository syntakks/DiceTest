using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    private Rigidbody rigidBody;
    private Vector3 diceVelocity;
    public float upForce = 500f;
    public Transform[] sides;
    public float magnitudeThreshold = 0.3f;
    public int torqueForceBottom = 0;
    public int torqueForceTop = 500; 
    bool isRolling = true;
    bool hasChecked = false;
    Vector3 originalPosition; 
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        originalPosition = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rigidBody.velocity;
        isRolling = diceVelocity.magnitude > magnitudeThreshold;

        if(Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            print("Rolling..."); 
            isRolling = true;
            hasChecked = false; 
            diceVelocity = Vector3.zero; 
            float dirX = Random.Range(0, torqueForceTop);
            float dirY = Random.Range(0, torqueForceTop);
            float dirZ = Random.Range(0, torqueForceTop);
            transform.position = new Vector3(originalPosition.x, originalPosition.y + 2, originalPosition.z); // Reset the transform before adding force. 
            transform.rotation = Quaternion.identity;
            rigidBody.AddForce(transform.up * upForce);
            rigidBody.AddTorque(dirX, dirY, dirZ); 
        }
         
        if (!isRolling && !hasChecked)
        {
            hasChecked = true; 
            CheckSides();
        }
        
    }

    private void CheckSides()
    {
        float yHeight = 0f;
        int side = 0; 
        for(int i = 0; i < sides.Length; i++)
        {
            // We have a larger value, store them. 
            if (sides[i].position.y > yHeight)
            {
                yHeight = sides[i].position.y;
                side = i;
            }
        }
        print("Landed on: " + (side + 1)); 
    }
}
