using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private Hand currentHand;

    private bool isAttack =false;
    
    private RaycastHit hitinfo;

    // Update is called once per frame
    void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if(Input.GetButton("Fire1"))
        {
            if(!isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentHand.animator.SetTrigger("Attack");

        //공격 활성화

        yield return new WaitForSeconds(currentHand.attackDelayA);
        



        isAttack =false;
    }

    IEnumerator HitCoroutine()
    {
        while(isAttack)
        {
            if(CheckObject())
            {
                isAttack = false;
                Debug.Log(hitinfo.transform.name);
            }
            yield return null;
        }
    }
    private bool CheckObject()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),out hitinfo, currentHand.range ))
        {
            return true;
        }

        return false;
    }
}
