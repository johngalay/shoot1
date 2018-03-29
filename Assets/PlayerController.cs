using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody myRigidbody;

	private Vector3 moveInput;
	private Vector3 moveVelocity;

	private Camera mainCamera;

	public GunController theGun;

	public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Start () {
		// Gets the component type rigidbody attached to the script.
		myRigidbody = GetComponent<Rigidbody>(); 
		mainCamera = FindObjectOfType<Camera>();
		OnMouseEnter();
	}
	
	// Update is called once per frame
	void Update () {
		// 
		moveInput = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0f);
		moveVelocity = moveInput * moveSpeed;

		pointLook();
		
		// left-click
		/*
		if(Input.GetMouseButtonDown(0)) {
			theGun.isFiring = true;
		}
		if(Input.GetMouseButtonUp(0)) {
			theGun.isFiring = false;
		}*/
	}

	void FixedUpdate () {
		myRigidbody.velocity = moveVelocity;
	}

	void pointLook () {
		Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.forward, Vector2.zero);
		float rayLength;

		if(groundPlane.Raycast(cameraRay, out rayLength)) {
			Vector3 pointToLook = cameraRay.GetPoint(rayLength);
			Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
			
			Vector2 direction = new Vector2(
				pointToLook.x - transform.position.x,
				pointToLook.y - transform.position.y
			);

			transform.up = direction;
		}
	}

	void OnMouseEnter() {
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}
}
