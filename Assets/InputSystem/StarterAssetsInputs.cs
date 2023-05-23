using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using UWAK.GAME.PLAYER;
using UWAK.ITEM;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool crouch;
		public bool interact;
		public bool useItem;
		public bool satu;
		public bool dua;
		public bool tiga;
		public bool empat;
		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnPause(InputValue value)
		{
			PauseInput(value.isPressed);
		}
		public void OnCrouch(InputValue value)
		{
			CrouchInput(value.isPressed);
		}
		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}
		public void OnUseItem(InputValue value)
		{
			UseItemInput(value.isPressed);
		}
		public void OnInvenIndex1(InputValue value)
		{
			Character.Instance.SetInventoryIndex(0);
		}
		public void OnInvenIndex2(InputValue value)
		{
			Character.Instance.SetInventoryIndex(1);
		}
		public void OnInvenIndex3(InputValue value)
		{
			Character.Instance.SetInventoryIndex(2);
		}
		public void OnInvenIndex4(InputValue value)
		{
			Character.Instance.SetInventoryIndex(3);
		}
		public void OnOpenInventory(InputValue value)
		{
			OpenInventory(value.isPressed);
		}

#endif
		private void UseItemInput(bool isPressed)
		{
			Item temp = Player.Instance.GetItemInHand();
			if(temp != null)
            {
                switch (temp.itemName)
                {
                    case NamaItem.KUNCI:
						break;
                    case NamaItem.SENTER:
						if (!temp.GetSenter().GetState())
                        {
							temp.Use(true);
                        }
                        else
                        {
							temp.Use(false);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

		private void InteractInput(bool isPressed)
		{
			interact = isPressed;
		}
		private void CrouchInput(bool isPressed)
		{
			crouch = isPressed;
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

		private void OpenInventory(bool isPressed)
		{
			if (GameManager.Instance.GetGameState() != GameState.OPENINVENTORY)
			{
				GameManager.Instance.ChangeState(GameState.OPENINVENTORY);
			}
			else
			{
				GameManager.Instance.ChangeState(GameState.GAMERESUME);
			}
		}

		private void PauseInput(bool isPressed)
		{
			if (GameManager.Instance.GetGameState() == GameState.GAMERESUME || GameManager.Instance.GetGameState() == GameState.GAME)
			{
				GameManager.Instance.ChangeState(GameState.GAMEPAUSED);
			}
			else if (GameManager.Instance.GetGameState() == GameState.OPENINVENTORY || GameManager.Instance.GetGameState()==GameState.GAMEPAUSED)
            {
				GameManager.Instance.ChangeState(GameState.GAMERESUME);
            }
		}
		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
	}
	
}