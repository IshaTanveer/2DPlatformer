using System;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveRef;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private float speedMultiplier;
    [Range(1, 10)]
    [SerializeField] private float accelertaion;
    [SerializeField] private float stopDeceleration = 60f;
    private Animator animator;
    private WallCheck wallCheck;
    private bool IsWallcheck = true;
    private int moveDirection = 1;
    private bool wasWallCheck = false;
    private bool isOnPlatform;
    public Rigidbody2D platformRB;
    ParticleController particleController;
    AudioManager audioManager;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wallCheck = FindFirstObjectByType<WallCheck>();
        particleController = FindFirstObjectByType<ParticleController>();
        audioManager = FindFirstObjectByType<AudioManager>();
    }
    void Start()
    {
        moveRef.action.Enable();
    }
    void FixedUpdate()
    {
        IsWallcheck = wallCheck.getWallCheck()
;       if (IsWallcheck && !wasWallCheck)
        {
            moveDirection *= -1;
            Flip();
            wallCheck.SetFacingDirection(moveDirection); 
        }
        wasWallCheck = IsWallcheck;
        UpdateSpeedMultiplier();
        float targetSpeed = speed * speedMultiplier;
        MovePlayer(targetSpeed);
        MoveAnimation();
    }
    private void Flip()
    {
        particleController.PlayTouchParticles();
        audioManager.PlaySounds(AudioManager.Sound.WallTouch);
        transform.Rotate(0,180,0);
    }

    void MovePlayer(float targetSpeed)
{
    if (moveRef.action.IsPressed())
    {
        float x = targetSpeed * moveDirection;
        if (isOnPlatform) x += platformRB.linearVelocity.x;
        rb.linearVelocity = new Vector2(x, rb.linearVelocity.y);
    }
    else if (!isOnPlatform)
    {
        float newX = Mathf.MoveTowards(rb.linearVelocity.x, 0f, stopDeceleration * Time.fixedDeltaTime);
        rb.linearVelocity = new Vector2(newX, rb.linearVelocity.y);
    }
}
    private void UpdateSpeedMultiplier()
    {
        if(moveRef.action.IsPressed() && speedMultiplier < 1.0f)
        {
            speedMultiplier += Time.fixedDeltaTime * accelertaion;
        }
        else if(!moveRef.action.IsPressed() && speedMultiplier > 0.0f)
        {
            speedMultiplier -= Time.fixedDeltaTime * accelertaion;
        }
    }
    void MoveAnimation(){}
    public bool GetisOnPlatform()
    {
        return isOnPlatform;
    }
    public void setIsOnPlatform(bool value)
    {
        isOnPlatform = value;
    }

}

