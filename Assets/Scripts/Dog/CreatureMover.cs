using System;
using UnityEditor;
using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    [DisallowMultipleComponent]
    public class CreatureMover : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float m_WalkSpeed = 1f;
        [SerializeField]
        private float m_RunSpeed = 1f;
        [SerializeField, Range(0f, 360f)]
        private float m_RotateSpeed = 90f;
        [SerializeField]
        private Space m_Space = Space.Self;
        [SerializeField]
        private float m_JumpHeight = 5f;

        [Header("Animator")]
        [SerializeField]
        private string m_VerticalID = "Vert";
        [SerializeField]
        private string m_StateID = "State";
        [SerializeField]
        private LookWeight m_LookWeight = new(1f, 0.3f, 0.7f, 1f);

        private Transform m_Transform;
        private CharacterController m_Controller;
        private Animator m_Animator;

        private MovementHandler m_Movement;
        private AnimationHandler m_Animation;

        private Vector2 m_Axis;
        private Vector3 m_Target;
        private bool m_IsRun;

        private bool m_IsMoving;

        public Vector2 Axis => m_Axis;
        public Vector3 Target => m_Target;
        public bool IsRun => m_IsRun;
        private bool m_IsRotatingInPlace;


        private void OnValidate()
        {
            m_WalkSpeed = Mathf.Max(m_WalkSpeed, 0f);
            m_RunSpeed = Mathf.Max(m_RunSpeed, m_WalkSpeed);

            m_Movement?.SetStats(m_WalkSpeed / 3.6f, m_RunSpeed / 3.6f, m_RotateSpeed, m_JumpHeight, m_Space);
        }

        private void Awake()
        {
            m_Transform = transform;
            m_Controller = GetComponent<CharacterController>();
            m_Animator = GetComponent<Animator>();

            m_Movement = new MovementHandler(m_Controller, m_Transform, m_WalkSpeed, m_RunSpeed, m_RotateSpeed, m_JumpHeight, m_Space);
            m_Animation = new AnimationHandler(m_Animator, m_VerticalID, m_StateID);
            
        }

           private void Update()
{
    HandleInput();
    
    Shader.SetGlobalVector("_PositionMoving", transform.position);

    m_Movement.Move(Time.deltaTime, in m_Axis, in m_Target, m_IsRun, m_IsMoving, out var animAxis, out var isAir);

    // 🔥 Inject fake forward input when rotating in place
            if (m_IsRotatingInPlace)
        {
            m_Animation.ForceAnimateInPlace();
        }
        else
        {
            m_Animation.Animate(in animAxis, m_IsRun ? 1f : 0f, Time.deltaTime);
        }

            if (Input.GetMouseButtonDown(0))
                    {
                        m_Animation.TriggerIdlePose();
                    }
        }




            
            private void HandleInput()
        {
            Vector2 moveAxis = Vector2.zero;
            float rotationInput = 0f;
            m_IsRotatingInPlace = false;

            if (Input.GetKey(KeyCode.W)) moveAxis.y += 1;
            if (Input.GetKey(KeyCode.S)) moveAxis.y -= 1;

            if (Input.GetKey(KeyCode.A)) rotationInput -= 1;
            if (Input.GetKey(KeyCode.D)) rotationInput += 1;

            bool isRun = Input.GetKey(KeyCode.LeftShift);
            bool isJump = Input.GetKeyDown(KeyCode.Space);

            // Set rotation flag if A or D are pressed without movement
            if (rotationInput != 0 && moveAxis == Vector2.zero)
            {
                m_IsRotatingInPlace = true;
            }

            // Manual rotation
            if (rotationInput != 0)
            {
                transform.Rotate(Vector3.up, rotationInput * m_RotateSpeed * Time.deltaTime);
            }

            Vector3 target = transform.forward;
            SetInput(moveAxis, target, isRun, isJump);
        }




        private void OnAnimatorIK()
        {
            m_Animation.AnimateIK(in m_Target, m_LookWeight);
        }

        public void SetInput(in Vector2 axis, in Vector3 target, in bool isRun, in bool isJump)
        {
            m_Axis = axis;
            m_Target = target;
            m_IsRun = isRun;

            if (m_Axis.sqrMagnitude < Mathf.Epsilon)
            {
                m_Axis = Vector2.zero;
                m_IsMoving = false;
            }
            else
            {
                m_Axis = Vector3.ClampMagnitude(m_Axis, 1f);
                m_IsMoving = true;

                
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if(hit.normal.y > m_Controller.stepOffset)
            {
                m_Movement.SetSurface(hit.normal);
            }
        }

        [Serializable]
        private struct LookWeight
        {
            public float weight;
            public float body;
            public float head;
            public float eyes;

            public LookWeight(float weight, float body, float head, float eyes)
            {
                this.weight = weight;
                this.body = body;
                this.head = head;
                this.eyes = eyes;
            }
        }


        #region Handlers
        private class MovementHandler
        {
            private readonly CharacterController m_Controller;
            private readonly Transform m_Transform;

            private float m_WalkSpeed;
            private float m_RunSpeed;
            private float m_RotateSpeed;

            private Space m_Space;

            private readonly float m_Luft = 75f;

            private float m_TargetAngle;
            private bool m_IsRotating = false;

            private Vector3 m_Normal;
            private Vector3 m_GravityAcelleration = Physics.gravity;

            private float m_jumpTimer;
            private Vector3 m_LastForward;

            public MovementHandler(CharacterController controller, Transform transform, float walkSpeed, float runSpeed, float rotateSpeed, float jumpHeight, Space space)
            {
                m_Controller = controller;
                m_Transform = transform;

                m_WalkSpeed = walkSpeed;
                m_RunSpeed = runSpeed;
                m_RotateSpeed = rotateSpeed;

                m_Space = space;
            }


            public void SetStats(float walkSpeed, float runSpeed, float rotateSpeed, float jumpHeight, Space space)
            {
                m_WalkSpeed = walkSpeed;
                m_RunSpeed = runSpeed;
                m_RotateSpeed = rotateSpeed;

                m_Space = space;
            }

            public void SetSurface(in Vector3 normal)
            {
                m_Normal = normal;
            }

            public void Move(float deltaTime, in Vector2 axis, in Vector3 target, bool isRun, bool isMoving, out Vector2 animAxis, out bool isAir)
            {
                Vector3 forward = m_Transform.forward;
        ConvertMovement(in axis, in forward, out var movement);

             if (movement.sqrMagnitude > 0.001f)
                {
                    m_LastForward = new Vector3(movement.x, 0f, movement.z).normalized;
                }

                CaculateGravity(deltaTime, out isAir);
                Displace(deltaTime, in movement, isRun);
                // Turn(in targetForward, isMoving);
                // UpdateRotation(deltaTime);

                GenAnimationAxis(in movement, out animAxis);
            }

            private void ConvertMovement(in Vector2 axis, in Vector3 targetForward, out Vector3 movement)
            {
                Vector3 forward;
                Vector3 right;

                if (m_Space == Space.Self)
                {
                    forward = new Vector3(targetForward.x, 0f, targetForward.z).normalized;
                    right = Vector3.Cross(Vector3.up, forward).normalized;
                }
                else
                {
                    forward = Vector3.forward;
                    right = Vector3.right;
                }

                movement = axis.x * right + axis.y * forward;
                movement = Vector3.ProjectOnPlane(movement, m_Normal);
            }

            private void Displace(float deltaTime, in Vector3 movement, bool isRun)
            {
                Vector3 displacement = (isRun ? m_RunSpeed : m_WalkSpeed) * movement;
                displacement += m_GravityAcelleration;
                displacement *= deltaTime;

                m_Controller.Move(displacement);
            }

            private void CaculateGravity(float deltaTime, out bool isAir)
            {
                m_jumpTimer = Mathf.Max(m_jumpTimer - deltaTime, 0f);

                if (m_Controller.isGrounded)
                {
                    m_GravityAcelleration = Physics.gravity;
                    isAir = false;

                    return;
                }

                isAir = true;

                m_GravityAcelleration += Physics.gravity * deltaTime;
                return;
            }

            private void GenAnimationAxis(in Vector3 movement, out Vector2 animAxis)
            {
                if (m_Space == Space.Self)
                {
                    animAxis = new Vector2(Vector3.Dot(movement, m_Transform.right), Vector3.Dot(movement, m_Transform.forward));
                }
                else
                {
                    animAxis = new Vector2(Vector3.Dot(movement, Vector3.right), Vector3.Dot(movement, Vector3.forward));
                }
            }

        private void Turn(in Vector3 targetForward, bool isMoving)
            {
                if (targetForward == Vector3.zero)
                    return;

                var flatTarget = Vector3.ProjectOnPlane(targetForward, Vector3.up).normalized;
                var angle = Vector3.SignedAngle(m_Transform.forward, flatTarget, Vector3.up);

                if (!m_IsRotating)
                {
                    if (!isMoving && Mathf.Abs(angle) < m_Luft)
                    {
                        m_IsRotating = false;
                        return;
                    }

                    m_IsRotating = true;
                }

                m_TargetAngle = angle;
            }


            private void UpdateRotation(float deltaTime)
            {
                if (!m_IsRotating)
                {
                    return;
                }

                var rotDelta = m_RotateSpeed * deltaTime;
                if (rotDelta + Mathf.PI * 2f + Mathf.Epsilon >= Mathf.Abs(m_TargetAngle))
                {
                    rotDelta = m_TargetAngle;
                    m_IsRotating = false;
                }
                else
                {
                    rotDelta *= Mathf.Sign(m_TargetAngle);
                }

                m_Transform.Rotate(Vector3.up, rotDelta);
            }

        }

        private class AnimationHandler
        {
            private readonly Animator m_Animator;
            private readonly string m_VerticalID;
            private readonly string m_StateID;

            private readonly float k_InputFlow = 4.5f;

            private float m_FlowState;
            private Vector2 m_FlowAxis;
            
            public void ForceAnimateInPlace()
            {
                // Simulate light walk input flow
                m_FlowAxis = new Vector2(0f, 0.2f);
                m_FlowState = Mathf.Clamp01(m_FlowState + Time.deltaTime * k_InputFlow);

                // Force Animator parameters to trigger walk blend
                m_Animator.SetFloat(m_VerticalID, 0.2f); // Small forward movement
                m_Animator.SetFloat(m_StateID, 0f);      // Ensure not running
            }

            public AnimationHandler(Animator animator, string verticalID, string stateID)
            {
                m_Animator = animator;
                m_VerticalID = verticalID;
                m_StateID = stateID;



            }
            public void TriggerIdlePose()
            {
                m_Animator.SetTrigger("IdlePose");
            }
            public void Animate(in Vector2 axis, float state, float deltaTime)
            {
                        Vector2 axisDelta = axis - m_FlowAxis;

            if (axisDelta.sqrMagnitude > 0.0001f)
            {
                m_FlowAxis += k_InputFlow * deltaTime * axisDelta.normalized;
            }
            else
            {
                m_FlowAxis = axis; // Fully snap when small
            }

            m_FlowAxis = Vector2.ClampMagnitude(m_FlowAxis, 1f);


                float maxValue = state > 0.5f ? 1f : 0.5f; // Only allow run when explicitly running
                float vertical = Mathf.Clamp(m_FlowAxis.magnitude, 0f, maxValue);


                m_Animator.SetFloat(m_VerticalID, vertical);
                m_Animator.SetFloat(m_StateID, Mathf.Clamp01(m_FlowState));
            }


            public void AnimateIK(in Vector3 target, in LookWeight lookWeight)
            {
                m_Animator.SetLookAtPosition(target);
                m_Animator.SetLookAtWeight(lookWeight.weight, lookWeight.body, lookWeight.head, lookWeight.eyes);
            }
        }
        #endregion
    }
}