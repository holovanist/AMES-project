using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool crouch;
		public bool jump;
		public bool sprint;
		public bool shoot;
        public bool Reload;
        public bool interact;
		public bool Climb;
		public bool Slide;
		public bool Dash;
		public bool Grapple;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

        public void OnGrapple(InputValue value)
        {
            GrappleInput(value.isPressed);
        }

        public void OnJump(InputValue value)
		{
            JumpInput(value.isPressed);
		}

        public void OnDash(InputValue value)
        {
            DashInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

        public void OnShoot(InputValue value)
        {
            ShootInput(value.isPressed);
        }

        public void OnReload(InputValue value)
        {
            ReloadInput(value.isPressed);
        }

        public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}
        public void OnClimb(InputValue value)
        {
            ClimbInput(value.isPressed);
        }
        public void OnCrouch(InputValue value)
        {
            CrouchInput(value.isPressed);
        }
        public void OnSlide(InputValue value)
        {
            SlideInput(value.isPressed);
        }
#endif
        public void GrappleInput(bool newGrappleState)
        {
            Grapple = newGrappleState;
        }
        public void DashInput(bool newDashState)
        {
            Dash = newDashState;
        }
        public void CrouchInput(bool newcrouchState)
        {
            crouch = newcrouchState;
        }
        public void SlideInput(bool newSlideState)
        {
            Slide = newSlideState;
        }
        public void ClimbInput(bool newClimbState)
        {
            Climb = newClimbState;
        }
        public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}

		public void ShootInput(bool newShootState)
        {
            shoot = newShootState;
        }
        public void ReloadInput(bool newReloadState)
        {
            Reload = newReloadState;
        }

        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}