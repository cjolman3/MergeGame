namespace MoenenGames.VoxelRobot {
	using UnityEngine;
	using System.Collections;
	using UnityEngine.InputSystem;


	public sealed class PlayerMovement : RobotMovement {

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;


		[SerializeField]
		private bool MouseFacing = false;


		private float prevFTime = -1f;
		private float prevBTime = -1f;
		private float prevLTime = -1f;
		private float prevRTime = -1f;

		// cinemachine
		private float _cinemachineTargetPitch;


		protected override void Update () {

			// base
			base.Update();

			// Input
			bool? moveLR;
			bool? moveFB;

			GetPlayerInput(out moveLR, out moveFB);


			// Rot
			if (Keyboard.current.shiftKey.isPressed || MouseFacing) {
				RotateToMouse();
			} else if (moveFB != null || moveLR != null) {
				RotateToMovingDirction(moveFB, moveLR);
			}

			// Move
			MoveBasedOnCamera(moveFB, moveLR);

		}

		private void LateUpdate()
		{
			// Cinemachine will follow this target
			CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);
		}


		void GetPlayerInput (out bool? moveLR, out bool? moveFB) {

			moveLR = null;
			moveFB = null;

			if (Keyboard.current.wKey.isPressed) {
				if (prevFTime < 0f) {
					prevFTime = Time.time;
				}
				if (prevFTime > prevBTime) {
					moveFB = true;
				}
			} else {
				prevFTime = -1f;
			}

			if (Keyboard.current.sKey.isPressed) {
				if (prevBTime < 0f) {
					prevBTime = Time.time;
				}
				if (prevBTime > prevFTime) {
					moveFB = false;
				}
			} else {
				prevBTime = -1f;
			}

			if (Keyboard.current.aKey.isPressed) {
				if (prevLTime < 0f) {
					prevLTime = Time.time;
				}
				if (prevLTime > prevRTime) {
					moveLR = false;
				}
			} else {
				prevLTime = -1f;
			}

			if (Keyboard.current.dKey.isPressed) {
				if (prevRTime < 0f) {
					prevRTime = Time.time;
				}
				if (prevRTime > prevLTime) {
					moveLR = true;
				}
			} else {
				prevRTime = -1f;
			}

		}




		void RotateToMouse () {
			Vector3 mousePos = GetMouseWorldPosition(transform.position, Vector3.up);
			Rotate(Quaternion.LookRotation(
				mousePos - transform.position
			));
		}



		void RotateToMovingDirction (bool? moveFB, bool? moveLR) {
			Rotate(
				Quaternion.LookRotation(
					new Vector3(
						moveLR == null ? 0f : moveLR.Value ? 1f : -1f,
						0f,
						moveFB == null ? 0f : moveFB.Value ? 1f : -1f
					),
					Vector3.up
				) * Quaternion.Euler(
					0f,
					Camera.main.transform.rotation.eulerAngles.y,
					0f
			));
		}


		void MoveBasedOnCamera (bool? moveFB, bool? moveLR) {
			Move(
				(Camera.main.transform.forward + Camera.main.transform.up) * (moveFB == null ? 0f : moveFB.Value ? 1f : -1f) +
				Camera.main.transform.right * (moveLR == null ? 0f : moveLR.Value ? 1f : -1f)
			);
		}




		private Vector3 GetMouseWorldPosition (Vector3 groundPosition, Vector3 groundNormal) {
			Plane plane = new Plane(groundNormal, groundPosition);
			Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
			float distance;
			if (plane.Raycast(ray, out distance)) {
				return ray.origin + ray.direction * distance;
			}
			return groundPosition;
		}


	}
}