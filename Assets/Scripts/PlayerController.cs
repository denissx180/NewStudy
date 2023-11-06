using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animator;
    private AudioSource audioSource;

    public float jumpForce;
    public float doubleJumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool isGameOver;
    public bool doubleJumpUsed;
    public bool isDash;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Dash();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            isGameOver = true;
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            audioSource.PlayOneShot(crashSound);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            audioSource.PlayOneShot(jumpSound, 0.5f);

            doubleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && !doubleJumpUsed)
        {
            doubleJumpUsed = true;
            rigidbody.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            animator.Play("Running_Jump", 3);
            audioSource.PlayOneShot(jumpSound, 0.5f);
        }
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDash = true;
            animator.SetFloat("Speed_ Multiplier", 2);
        }
        else
        {
            isDash = false;
            animator.SetFloat("Speed_ Multiplier", 1);
        }
    }
}
