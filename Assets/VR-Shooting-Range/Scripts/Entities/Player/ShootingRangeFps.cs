
using UnityEngine;
using UnityEngine.InputSystem;

namespace ExitGames.SportShooting
{
	public class ShootingRangeFps : ShootingRangeBasePlayer
	{
		[Header("Player Specific")]
		public Transform gunPivot;

		public Camera playerCamera;
		
		[Range(0.1f, 10.0f)]
		public float mouseSensitivity = 1f;

		[SerializeField]
		private GameObject canvas;
		
		private float rotationX, rotationY;

        public void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Update()
        {
            MenuInteraction();
            LookAround();
        }


        public void OnGUI()
        {
            if (!GameController.Instance.IsInGame)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("Press 'RETURN' to begin.");
                GUILayout.EndVertical();
            }


            if (GameController.Instance.IsInGame)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("Press 'ESC' to leave the game.");               

                GUILayout.EndVertical();
            }
        }

        private void LookAround()
        {
            var mousePosition = Mouse.current.delta.ReadValue();
            rotationX += mousePosition.x * mouseSensitivity;
            rotationY += mousePosition.y * mouseSensitivity;

            rotationX = (rotationX < -360.0f) ? (rotationX + 360.0f) : ((rotationX > 360.0f) ? (rotationX - 360.0f) : rotationX);
            rotationY = (rotationY < -360.0f) ? (rotationY + 360.0f) : ((rotationY > 360.0f) ? (rotationY - 360.0f) : rotationY);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.rotation = Quaternion.identity * xQuaternion * yQuaternion;
        }

        private void MenuInteraction()
        {
            if (Input.GetKeyDown(KeyCode.Return) && !GameController.Instance.IsInGame)
            {

                GameController.Instance.StartMultiplayerGame();

            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameController.Instance.InitMainMenu();
            }
        }

		public override bool GunTriggerPressed(int gunIndex)
		{
            return Mouse.current.leftButton.isPressed;

		}

		public override bool AimingLaserPressed(int gunIndex)
        {
            return Mouse.current.rightButton.isPressed;
        }

		public override Vector3 GetGunPosition(int gunIndex)
		{
			return gunPivot.position;
		}

		public override Quaternion GetGunRotation(int gunIndex)
		{
			return gunPivot.rotation;
		}
	}
}