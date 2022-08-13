using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  private Rigidbody2D rb;
  private Animator anim;
  private SpriteRenderer spriteRenderer;
  private float dirX;
  private MovementState state;
  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private float jumpForce = 14f;

  private enum MovementState { idle, run, jump, fall }

  // Start is called before the first frame update
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  private void Update()
  {
    dirX = Input.GetAxisRaw("Horizontal");

    rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

    if (Input.GetButtonDown("Jump") && (int)state != 2 && (int)state != 3)
    {
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    UpdateAnimationState();
  }

  private void UpdateAnimationState()
  {
    if (dirX != 0f)
    {
      state = MovementState.run;
      spriteRenderer.flipX = dirX < 0f;
    }
    else
    {
      state = MovementState.idle;
    }

    if (rb.velocity.y > .1f)
    {
      state = MovementState.jump;
    }
    else if (rb.velocity.y < -.1f)
    {
      state = MovementState.fall;
    }

    anim.SetInteger("state", (int)state);
  }

  private bool IsGrounded()
  {
    
  }
}
