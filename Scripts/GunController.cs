using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun;
    [SerializeField]
    private Vector3 originPos;
    
    private float currentFireRate;

    private bool isReload = false;
    private bool isFineSight =false;
    
    private AudioSource audioSource;

    private RaycastHit hitInfo;

    [SerializeField]
    private Camera theCam;
    [SerializeField]
    private GameObject hit_effect_prefab;
  
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        TryFineSight();
    }
    
    private void GunFireRateCalc()
    {
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime; //1초의 역수
        }
    }
    private void TryFire()
    {
        if(Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload)
        {
            Fire();
        }
        else if(Input.GetButton("Fire1") && currentFireRate <= 0 && !isReload && (isFineSight == true))
        {
            Fire2();
            
        }
    }

    private void Fire()
    {
        if(!isReload)
        {
            if(currentGun.currentBulletCount > 0)
            {
            Shoot();
            }
            else
            {
            StartCoroutine(ReloadCoroutine());
            }
        }
        
    }

    private void Fire2()
    {
        if(!isReload)
        {
            if(currentGun.currentBulletCount > 0)
            {
            currentGun.animator.SetTrigger("Aim_Attack");
            Shoot();
            currentGun.animator.SetTrigger("Aim_Attack_Out");
            }
            else
            {
            StartCoroutine(ReloadCoroutine());
            }
        }
        
    }
    private void Shoot()
    {
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;
        PlayAudio(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        Hit();
        Debug.Log("총알 발사함");
    }

    private void Hit()
    {
        if(Physics.Raycast(theCam.transform.position, theCam.transform.forward, out hitInfo, currentGun.range))
        {
            if(hitInfo.transform != null)
            {
                GameObject clone = Instantiate(hit_effect_prefab,hitInfo.point,Quaternion.LookRotation(hitInfo.normal));
                if(clone != null)
                {
                    HPcontroller hp = hitInfo.transform.GetComponent<HPcontroller>();
                    if(hp != null)
                    {
                        hp.TakeDamage(10f);
                        Destroy(clone,2f);
                    }
                
                   
                }
                
            }
            
        }
    }

    private void TryReload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReload && currentGun.currentBulletCount < currentGun.reloadBulletCount)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }
    IEnumerator ReloadCoroutine()
    {
        if(currentGun.carryBulletCount > 0)
        {
            isReload = true;

            currentGun.animator.SetTrigger("Reload");

            currentGun.carryBulletCount += currentGun.currentBulletCount;
            currentGun.currentBulletCount = 0;
            
            yield return new WaitForSeconds(currentGun.reloadTime);

            if(currentGun.carryBulletCount >= currentGun.reloadBulletCount)
            {
                currentGun.currentBulletCount = currentGun.reloadBulletCount;
                currentGun.carryBulletCount -= currentGun.reloadBulletCount;
            }
            else{
                currentGun.currentBulletCount = currentGun.carryBulletCount;
                currentGun.carryBulletCount = 0;

            }

            isReload = false;
        }
        else 
        {
            Debug.Log("소유한 총알이 없습니다.");
        }
    }

    private void TryFineSight()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            FineSight();
        }
    }
    private void FineSight()
    {
        

        isFineSight = !isFineSight;
        currentGun.animator.SetBool("FineSight", isFineSight);
        
        

        if(isFineSight)
        {
            StartCoroutine(FineSightActivateCoroutine());
        }
        else
        {
            StartCoroutine(FineSightDeactivateCoroutine());
            
        }  
    }

    IEnumerator FineSightActivateCoroutine()
    {
        while(currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.2f);
            yield return null;
        }
    }
    IEnumerator FineSightDeactivateCoroutine()
    {
        while(currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.2f);
            yield return null;
        }
    }
    private void PlayAudio(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }   

    public Gun GetGun()
    {
        return currentGun;
    }
}
