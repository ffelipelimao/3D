using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public Animator anim;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    private float vSpeed = 0f;
    public float jumpSpeed = 15f;
    public KeyCode keyCodeJump = KeyCode.Space;
    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;
    public List<VFXFlashColor> vFXFlashColor;
    public HealthBase healthBase;
    private bool _isAlive = true;
    public List<Collider> colliders;


    void Awake()
    {
        OnValidate();
        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            vSpeed = 0;

            if (Input.GetKeyDown(keyCodeJump))
            {
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        var isWalking = inputAxisVertical != 0;

        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                anim.speed = speedRun;
            }
            else
            {
                anim.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        anim.SetBool("Run", inputAxisVertical != 0);

        /* if (inputAxisVertical != 0)
         {
             anim.SetBool("Run", true);
         }
         else
         {
             anim.SetBool("Run", false);
         }
         */
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.GetComponentInParent<EnemyBase>() != null)
        {
            healthBase.Damage(5f);
        }
    }

    public void Damage(HealthBase healthBase)
    {
        Debug.Log("Player Damage Flash");
        vFXFlashColor.ForEach(i => i.Flash());
    }

    public void OnKill(HealthBase healthBase)
    {
        if (_isAlive)
        {
            _isAlive = false;
            anim.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckPointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckPointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void Revive()
    {
        _isAlive = true;
        healthBase.ResetLife();
        anim.SetTrigger("Revival");
        Respawn();
        Invoke(nameof(TurnOnColliders), 3f);
    }

    void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }
}
