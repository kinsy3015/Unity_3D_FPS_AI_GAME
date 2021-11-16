using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController2 : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    

    Camera cam;
    PlayerMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray =cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,100,movementMask))
            {
                motor.MoveToPoint(hit.point);

            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            Ray ray =cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    SetFocus(interactable);
                }
            }


        }
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
        motor.MoveToPoint(newFocus.transform.position);
    }

    void RemoveFocus ()
    {
        focus = null;
    }
}
