using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 4f;
    public float sprintMultiplier = 1.6f;
    public float gravity = -9.81f;

    [Header("Stamina")]
    public float sprintCostPerSecond = 15f;

    [Header("Camera")]
    public Transform cameraPivot;

    [Header("Head Bob")]
    public float bobSpeed = 10f;
    public float bobAmount = 0.05f;

    [Header("Footsteps")]
    public AudioSource footstepSource;
    public AudioClip[] stoneSteps;
    public float walkStepRate = 0.5f;
    public float sprintStepRate = 0.35f;
    
    public bool canMove = true;

    private CharacterController controller;
    private PlayerStats stats;

    private Vector3 velocity;

    // Head bob
    private float defaultCamY;
    private float bobTimer;
    private float stepTimer;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        if (cameraPivot == null)
        {
            enabled = false;
            return;
        }

        defaultCamY = cameraPivot.localPosition.y;
    }

    void Update()
    {
        if (!canMove) return;

        HandleMovement();
        HandleGravity();
        HandleFootsteps();
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        bool isMoving = move.magnitude > 0.1f;
        bool isMovingForward = Input.GetKey(KeyCode.W);
        bool wantsSprint = Input.GetKey(KeyCode.LeftShift);

        bool isSprinting = wantsSprint && isMoving && stats.stamina > 0f;

        float currentSpeed = isSprinting
            ? speed * sprintMultiplier
            : speed;

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (isSprinting)
            stats.ConsumeStamina((sprintCostPerSecond * 1.2f) * Time.deltaTime);


        HandleHeadBob(isMovingForward, move.magnitude, isSprinting);

    }

    void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleHeadBob(bool movingForward, float moveAmount, bool isSprinting)
    {
        if (controller.isGrounded && movingForward && moveAmount > 0.1f)
        {
            float speedMultiplier = isSprinting ? 1.5f : 1f;

            bobTimer += Time.deltaTime * bobSpeed * speedMultiplier;

            float yOffset = Mathf.Sin(bobTimer) * bobAmount;
            cameraPivot.localPosition = new Vector3(
                cameraPivot.localPosition.x,
                defaultCamY + yOffset,
                cameraPivot.localPosition.z
            );
        }
        else
        {
            bobTimer = 0f;
            cameraPivot.localPosition = new Vector3(
                cameraPivot.localPosition.x,
                Mathf.Lerp(cameraPivot.localPosition.y, defaultCamY, Time.deltaTime * 5f),
                cameraPivot.localPosition.z
            );
        }
    }
    void HandleFootsteps()
    {
        if (!controller.isGrounded)
            return;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isMoving = new Vector3(x, 0, z).magnitude > 0.1f;
        if (!isMoving)
            return;

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && stats.stamina > 0f;

        float stepRate = isSprinting ? sprintStepRate : walkStepRate;

        stepTimer += Time.deltaTime;

        if (stepTimer >= stepRate)
        {
            PlayFootstep();
            stepTimer = 0f;
        }
    }
    void PlayFootstep()
    {
        if (stoneSteps.Length == 0 || footstepSource == null)
            return;
        if (stats.stamina / stats.maxStamina < 0.4f)
        {
            footstepSource.pitch = Random.Range(0.95f, 1.05f);
            footstepSource.PlayOneShot(stoneSteps[1]);
            return;
        }
        footstepSource.pitch = Random.Range(0.95f, 1.05f);
        footstepSource.PlayOneShot(stoneSteps[0]);
    }
}
