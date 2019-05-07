using UnityEngine;

[RequireComponent(typeof(UnitMotor))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private LayerMask _movementMask;

	private Camera _cam;
	private UnitMotor _motor;

	private void Start()
	{
		_cam = Camera.main;
		_motor = GetComponent<UnitMotor>();
		_cam.GetComponent<CameraController>().Target = transform;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out var hit,
				100f, _movementMask))
			{
				_motor.MoveToPoint(hit.point);
			}
		}

		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out _, 100f))
			{

			}
		}
	}
}
