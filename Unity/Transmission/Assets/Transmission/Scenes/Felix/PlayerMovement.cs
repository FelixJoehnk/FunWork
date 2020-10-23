using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 mouse_pos_old;
    private float speed;

    
    // Start is called before the first frame update
    void Start()
    {
        mouse_pos_old = Input.mousePosition;
        speed = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += Vector3.forward;
        

        transform.Rotate(Vector3.right, mouse_pos_old.y - Input.mousePosition.y);
        
        transform.Rotate(Vector3.up, Input.mousePosition.x - mouse_pos_old.x);
        
        mouse_pos_old = Input.mousePosition;

        transform.position +=  speed * transform.forward * Time.deltaTime;

        if(Input.GetKey(KeyCode.Mouse0)){
            speed *=1.1f;
        }else if(Input.GetKey(KeyCode.Mouse1)){
            speed /=1.1f;
        }
        
    }
}
