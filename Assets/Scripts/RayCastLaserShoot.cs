using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCastLaserShoot : MonoBehaviour
{
    public int laserDamage = 1;
    public float fireRate = .5f;
    public float weaponRange = 10f;
    public float hitForce = 100f;

    public Transform head;
    private WaitForSeconds shotDuration = new WaitForSeconds(1f);
    private AudioSource laserAudio;
    private LineRenderer laserLine;
    private float nextFire; 
    // Start is called before the first frame update TODO
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserAudio = GetComponent<AudioSource>();
        head = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        var gamepad = Gamepad.current;
        bool isLeftMouseCLicked = mouse.leftButton.isPressed;
        bool isRightTriggerPulled = gamepad.rightTrigger.isPressed;
        
        if ( (isLeftMouseCLicked || isRightTriggerPulled) && Time.time > nextFire) 
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = head.position; //this needs to be location of eyeball or head
            RaycastHit hit;
            laserLine.SetPosition(0, head.position);
            if (Physics.Raycast(rayOrigin, head.forward, out hit, weaponRange))
            { 
                laserLine.SetPosition (1, hit.point);
                ShootableArmature health = hit.collider.GetComponent<ShootableArmature>();

                if (health != null)
                {
                    health.Damage (laserDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce (-hit.normal * hitForce);
                }
            }
            else
            { 
                laserLine.SetPosition(1, rayOrigin + (head.forward * weaponRange));
            }
        }
        

    }
    
    private IEnumerator ShotEffect()
    {
        laserAudio.Play();
        laserLine.enabled = true;
        laserLine.startColor = Color.white;
        laserLine.endColor = Color.magenta;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
