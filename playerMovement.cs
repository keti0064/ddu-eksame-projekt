using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    
    public Rigidbody2D rb;
    public float speed;

    
    Vector3 mousePosition;
    void Update()
    {
        // bevægelse
        rb.velocity = new Vector2(speed*Input.GetAxisRaw("Horizontal"),speed*Input.GetAxisRaw("Vertical"));
        // drejer spilleren mod musen
        mousePosition = Input.mousePosition;           
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward );
        transform.rotation = rot;  
        transform.eulerAngles = new Vector3(0, 0,transform.eulerAngles.z);

    }

}
