using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _animator; 

    private bool OpenBool = false;
    private bool CloseBool = false;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            _animator.SetBool("OpenBool", true);
            _animator.SetBool("CloseBool", true);
            Debug.Log ("OPENING THE DOOR");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("OpenBool", false);
            Debug.Log ("OPENING THE DOOR");
          
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E)){
           
            _animator.SetBool("CloseBool", false);
            
            
            
            

        }
    }

}
