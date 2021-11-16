using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPcontroller : MonoBehaviour
{

    [SerializeField]
    private float max_health =100f;

    private float current_health;
    // Start is called before the first frame update
    void Start()
    {
        current_health =max_health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {    
        current_health -=damage;
        if(current_health <= 0)
        {
            if(gameObject.tag == "Player"){
                current_health = max_health;
                gameObject.transform.position = new Vector3(5.945473f,2f,-6.39f);
            }
            else{
                Destroy(gameObject);
            }
            
        }
    }
}
