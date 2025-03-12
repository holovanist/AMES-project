using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NewMovment
{
    //https://www.youtube.com/watch?v=WfW0k5qENxM&t=322
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        private float moveSpeed;
        public float walkSpeed;
        public float sprintSpeed;
        public float wallRunSpeed;
        public float slideSpeed;
        public float climbSpeed;
        [Tooltip("The difference in speed of the last way of moving and the current")]
        public float MaxSpeedDifference = 4f;

        public float speedIncreaseMultiplier;
        public float slopeIncreaseMultiplier;

        private float desiredMoveSpeed;
        private float lastDesiredMoveSpeed;

        public float GroundDrag;
        [Header("Jumping")]
        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump;

        [Header("Crouching")]
        public float crouchSpeed;
        public float crouchYScale;
        private float startYScale;
        

        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask whatIsGround;
        public float maxGroudTime;
        [HideInInspector]
        public bool grounded;

        [Header("Slope Handling")]
        public float maxSlopeAngle;
        private RaycastHit slopeHit;
        private bool ExitingSlope;



        [Header("References")]
        public Transform orientation;
        public TextMeshProUGUI Speed;

        float horizontalInput;
        float verticalInput;

        float timer;

        Vector3 moveDir;
        Rigidbody rb;
        private StarterAssetsInputs it;
        //private Climbing cs;
        
        public MovementState state;
        public enum MovementState
        {
            freeze,
            unlimited,
            walking,
            sprinting,
            wallrunning,
            climbing,
            crouching,
            sliding,
            air
        }
        [HideInInspector]
        public bool sliding;
        [HideInInspector]
        public bool crouching;
        [HideInInspector]
        public bool wallrunning;
        [HideInInspector]
        public bool climbing;
        [HideInInspector]
        public bool freeze;
        [HideInInspector]
        public bool unlimited;
        [HideInInspector]
        public bool restricted;

        void Start()
        {
            //cs = GetComponent<Climbing>();
            it = GetComponent<StarterAssetsInputs>();
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true; 
            readyToJump = true;
            startYScale = transform.localScale.y;
            crouching = false;
        }
        private void Update()
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround); 
            MyInput();
            SpeedControl();
            StateHandler();
            if (grounded)
                rb.linearDamping = GroundDrag;
            else
                rb.linearDamping = 0f;
            if(Speed != null)
            Speed.SetText("Speed: " + rb.linearVelocity.magnitude);
        }

        private void FixedUpdate()
        {
            if (grounded)
                timer += Time.fixedDeltaTime;
            else timer = 0;
            MovePlayer();
        }

        private void MyInput()
        {
            horizontalInput = it.move.x;
            verticalInput = it.move.y;

            if(it.jump && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
            if(it.crouch && !crouching && !wallrunning)
            {
                crouching = true;
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
                if (grounded)
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            }
            else if(!it.crouch)
            {
                crouching = false;
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            }
        }
        private void StateHandler()
        {
            if(freeze)
            {
                state = MovementState.freeze;
                rb.linearVelocity = Vector3.zero;
            }
            else if (unlimited)
            {
                state = MovementState.unlimited;
                moveSpeed = 999f;
                return;
            }
            else if(climbing)
            {
                state = MovementState.climbing;
                desiredMoveSpeed = climbSpeed;
            }
            else if(sliding)
            {
                state = MovementState.sliding;

                if(OnSlope() && rb.linearVelocity.y < 0.1f)
                    desiredMoveSpeed = slideSpeed;
                else 
                    desiredMoveSpeed = sprintSpeed;
            }
            else if (wallrunning)
            {
                state = MovementState.wallrunning;
                desiredMoveSpeed = wallRunSpeed;
            }
            else if (it.crouch)
            {
                state = MovementState.crouching;
                desiredMoveSpeed = crouchSpeed;
            }
            else if (grounded && it.sprint)
            {
                state = MovementState.sprinting;
                desiredMoveSpeed = sprintSpeed;
            }
            else if(grounded)
            {
                state = MovementState.walking;
                desiredMoveSpeed = walkSpeed;
            }
            else
            {
                state = MovementState.air;
            }

            if(Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > MaxSpeedDifference && moveSpeed!= 0)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                moveSpeed = desiredMoveSpeed;
            }

            lastDesiredMoveSpeed = desiredMoveSpeed;
        }

        private IEnumerator SmoothlyLerpMoveSpeed()
        {
            float time = 0;
            float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
            float startValue = moveSpeed;

            while(time < difference)
            {
                moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);
                if (OnSlope())
                {
                    float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                    float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                    time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
                }
                else
                    time += Time.deltaTime * speedIncreaseMultiplier;

                //time += Time.deltaTime;
                yield return null;
            }

            moveSpeed = desiredMoveSpeed;
        }



        private void MovePlayer()
        {
            if(restricted) return;

            //if (cs.exitingWall) return;

            moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if(OnSlope() && !ExitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection(moveDir) * moveSpeed * 20f, ForceMode.Force);

                if (rb.linearVelocity.y > 0)
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

            else if (grounded)
                rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
            else if(!grounded)
                rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            if(!wallrunning)
            rb.useGravity = !OnSlope();
        }
        private void SpeedControl()
        {
            if (OnSlope() && !ExitingSlope)
            {
                if(rb.linearVelocity.magnitude > moveSpeed)
                    rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
            }
            else
            {
                Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                if(flatVel.magnitude > moveSpeed)
                {
                        Vector3 limetedVel = flatVel.normalized * moveSpeed;
                        rb.linearVelocity = new Vector3(limetedVel.x, rb.linearVelocity.y, limetedVel.z);
                }

            }

        }
        private void Jump()
        {
            ExitingSlope = true;

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f , rb.linearVelocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        private void ResetJump()
        {
            readyToJump = true;

            ExitingSlope = false;
        }

        public bool OnSlope()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            else return false;
        }

        public Vector3 GetSlopeMoveDirection(Vector3 Dir)
        {
            return Vector3.ProjectOnPlane(Dir, slopeHit.normal).normalized;
        }







    }
}