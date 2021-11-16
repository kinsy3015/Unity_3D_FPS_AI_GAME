using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  
    private Rigidbody Rigid;
    
    [SerializeField]
    private Camera Camera;

    [SerializeField]
    private float Speed;


    [SerializeField]
    private float camaraSensitivity;

    [SerializeField]
    private float camaraRotationLimit;
    private float currentCameraRotationX = 0;
    [SerializeField]
    public Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(2560,1440,true);
        Rigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CameraRotation();
        CharacterRotation();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    } 


    
    private void PlayerMovement()
    {

        float _moveX= Input.GetAxisRaw("Horizontal");
        float _moveZ= Input.GetAxisRaw("Vertical");

        Vector3 _moveHori = transform.right * _moveX;
        Vector3 _moveVerti = transform.forward *_moveZ;
        Vector3 _velocity = (_moveHori + _moveVerti).normalized * Speed;

        Rigid.MovePosition(transform.position + _velocity * Time.deltaTime );


    }

    private void CameraRotation()
    {
        float _xRtt = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRtt * camaraSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -camaraRotationLimit, camaraRotationLimit);

        Camera.transform.localEulerAngles = new Vector3(currentCameraRotationX,0f,0f);
    }


    private void CharacterRotation()
    {
        float _yRtt = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRtt, 0f) * camaraSensitivity;
        Rigid.MoveRotation(Rigid.rotation * Quaternion.Euler(_characterRotationY));
    }
    


    


}
