using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    [SerializeField] private float speed = 80f;
    [SerializeField] private float jumpingPower = 50f;
    private bool isFacingRight = true;
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 15f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private bool isWallSliding;
    [SerializeField] private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(12f, 30f);
    private bool doubleJump;
    private Vector3 checkpointPos;
    public GameObject fallDetector;
    public Text scoreText;
    public TMP_Text WinText;
    public GameObject WinPanel;
    public GameObject Tooltip;
    public TMP_Text Tooltiptext;
    public GameObject Remind;
    public TMP_Text Remindtext;
    public GameObject restartButton;
    public int totalScore = 0;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private int maxPoint;

    void Start()
    {
        checkpointPos = transform.position;
        scoreText.text = "Score: " + totalScore;
        Tooltip.SetActive(false);
        Remind.SetActive(false);
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded() && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        WallSlide();
        WallJump();
        if (!isWallJumping)
        {
            Flip();
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = checkpointPos;
        }
        else if (collision.tag == "bullet")
        {
            totalScore += 1;
            scoreText.text = "Score: " + totalScore;
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("spike"))
        {
            transform.position = checkpointPos;
        }
        else if (collision.tag == "flag")
        {
            if (totalScore == maxPoint)
            {
                Scene currentScene = SceneManager.GetActiveScene();
                int sceneNumber = currentScene.buildIndex;
              if(sceneNumber==1){
                Time.timeScale = 0f;
                SceneManager.LoadSceneAsync(3);
                } if(sceneNumber==4){
                Time.timeScale = 0f;
                SceneManager.LoadSceneAsync(5);
                }else{
                Time.timeScale = 0f;
                SceneManager.LoadSceneAsync(3);
            } if(sceneNumber==6){
                Time.timeScale = 0f;
                SceneManager.LoadSceneAsync(7);
                }
            }

        }
        else if (collision.tag == "guide")
        {
            Tooltip.SetActive(true);
            Invoke("HideTooltip", 5f);
        }

        else if (collision.tag == "remind")
        {
            Remind.SetActive(true);
            Invoke("HideRemind", 5f);

        }
    }

    private void HideTooltip()
    {
        Tooltip.SetActive(false);
    }

    private void HideRemind()
{
    Remind.SetActive(false);
}
private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = 1.5f;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}