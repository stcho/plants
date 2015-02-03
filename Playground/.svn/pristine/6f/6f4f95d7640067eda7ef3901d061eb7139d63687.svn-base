using UnityEngine;
using System.Collections;
//The stick swirling for this particular controller is damn seemingly broken. rotatingStickController deserves
public class swirlingJumpController : BaseController {	//re-evaluation after its performance here in the logic.

	const int Hovering = 4;
	const int Swirling = 5;

	/**************SWIRLING_JUMP_VARIABLES**************/
	//Ground control variables
	private Vector3 lookDirection;

	private float forwardDotThreshold = 0.65f;
	private float bankingDotThreshold = 0.0f;
	private float cuttingDotThreshold = -0.65f;

	private float forwardAccelerationBonus = 0.1f;
	private float bankingAccelerationPenalty = -0.01f;
	private float cuttingAccelerationPenalty = -0.05f;
	private float brakeMagnitude = -0.1f;

	private float forwardRotationalSpeed = 0.5f;
	private float bankingRotationalSpeed = 0.2f;
	private float cuttingRotationalSpeed = 1.0f;
	private float stationaryRotationalSpeed = 2.0f;

	private float groundDrag = 0.01f;
	public float currentAcceleration;
	private float maxAcceleration = 2.0f;

	//Hovering variables
	private float hoverTimerLength = 5.0f;
	public float hoverTimer = 0.0f;
	private float hoverHeight = 2.0f;
	//--Hovering activation in ground-state
		public float hoveringParameter;
		private float hoveringIntervalModifier = 0.5f;
		private float hoveringThreshold = 5.0f;
		private float hoveringDegradation = 0.01f;
		private float swirlBufferLength = 1.0f;
		public float swirlBuffer;

	//Swirling variables
	private float directionChangePenalty = 1.0f;
	private float swirlingAcceleration = 0.01f;
	private float accelerationEquilibrium = 0.01f;
	private float swirlingMagnitude = 0.3f;
	private float maxSwirlSpeed = 15.0f;
	private float terminalVelocity = -25.0f;
	public float swirlingTime;
	private float swirlingGravityRelief = 1.0f;
	private float swirlingGravityCoefficient = 0.5f;
	private float rotationalDistance = 0.5f;
	private float swirlingRotateSpeed = 10.0f;
	private Vector3 lookTarget;

	//Stick swirling variables
	public float lastRadians, currentRadians, interval;
	private bool sign;
	private float RotationResolution = Mathf.PI/16.0f;
	private float RotationSkip = Mathf.PI/4;

	//Raycasting information
	private RaycastHit info;

	//L_SYSTEM
	private L_System_Animated headEmblem;
	private GameObject lsysObject;
	private float spinParameter;
	/***************************************************/
	
	void Awake ()
	{
		moveDirection = transform.TransformDirection(Vector3.forward);
		lookDirection = moveDirection;
		_animation = GetComponent<Animation>();
		_characterState = Idle;
		lastState = Idle;
		swirlBuffer = 0;

		lsysObject = (GameObject)Instantiate(Resources.Load("Prefabs/L_Systems/Special/bushAnimated"),
			                                     new Vector3(transform.position.x, 2.0f, transform.position.z), 
			                                     Quaternion.identity);
		lsysObject.transform.parent = transform;
	
		headEmblem = lsysObject.GetComponent<L_System_Animated> ();
		headEmblem.maxEdge = 0.1f;
		headEmblem.maxAngle = 25.0f;
		headEmblem.generations = 3;
		                               
		if(!_animation)
			Debug.Log("The character you would like to control doesn't have animations. Moving her might look weird.");
		if(!idleAnimation) {
			_animation = null;
			Debug.Log("No idle animation found. Turning off animations.");
		}
		if(!walkAnimation) {
			_animation = null;
			Debug.Log("No walk animation found. Turning off animations.");
		}
		if(!runAnimation) {
			_animation = null;
			Debug.Log("No run animation found. Turning off animations.");
		}
		if(!jumpPoseAnimation && canJump) {
			_animation = null;
			Debug.Log("No jump animation found and the character has canJump enabled. Turning off animations.");
		}
		
	}
	
	/*Calculate moveDirection, moveSpeed, inAirVelocity based on user input,
	which compose the movement vector passed to CharacterController.Move() in Update()*/
	void UpdateSmoothedMovementDirection ()
	{
		headEmblem.stop = true;
		Transform cameraTransform = Camera.main.transform;
		grounded = IsGrounded();
		
		//transform.localScale = new Vector3(1, 1, 1);
		
		// Forward vector relative to the camera along the x-z plane	
		forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		
		// Right vector relative to the camera
		// Always orthogonal to the forward vector
		Vector3 right = new Vector3(forward.z, 0, -forward.x);
		
		//Store unsmoothed keyboard input
		v = Input.GetAxis("Vertical");
		h = Input.GetAxis ("Horizontal");
		
		// Are we moving backwards or looking backwards
		if (v < -0.2)
			movingBack = true;
		else
			movingBack = false;

		//Determine if keyboard input has occured (and hence movement as a result)
		isMoving = Mathf.Abs (h) > 0.1 || Mathf.Abs (v) > 0.1;

		float rotV = Input.GetAxis("Mouse Y");
		currentRadians = Mathf.Atan2(rotV, Input.GetAxis("Mouse X"));
		if(rotV < 0)
			currentRadians = (Mathf.PI*2) + currentRadians;
		interval = currentRadians - lastRadians;

		if(Physics.Raycast (transform.position, -transform.up, out info, hoverHeight))
		{
			//The ray is only as long as the hoverHeight. If this raycast is true, then we are below
			//the hover height.
			if(_characterState == Jumping && info.distance < 1.0f)
			{
				_characterState = Idle;
			}
			else if(_characterState == Swirling && swirlingTime > 2.0f)
				//Having sunk this low during swirling, the player is
			{ 										//returned to the ground. If too high of a verticalspeed
				_characterState = Jumping;			//is attained it is possible for the player to raycast
				lastState = Swirling;			//himself and give an erroneous state change. We will 
				//clamp the vertical speed to 20 to prevent this.
			}
			else if(_characterState == Hovering) //Here we can find out how far below hoverHeight we are 
			{ 										//and readjust based on that value
				transform.position += info.normal*(hoverHeight - info.distance);
			}
		}
		
		// Grounded controls
		if (_characterState != Swirling && _characterState != Jumping)
		{
			// Target direction relative to the camera
			Vector3 targetDirection = v * forward + h * right;

			//*******MOVEMENT CONTROLS APPLIED WITH INPUT TO GROUND AND HOVERING STATES********//
			if (targetDirection != Vector3.zero) //i.e. if v or h is not 0, meaning there has been input
			{
				//Calculate the dot product of the target and the current movement directions
				float dot = Vector3.Dot(targetDirection.normalized, lookDirection.normalized);
				if(dot > forwardDotThreshold)
				{
					currentAcceleration = forwardAccelerationBonus;
					moveDirection = Vector3.RotateTowards(moveDirection, targetDirection, 
					                                      		forwardRotationalSpeed*Time.deltaTime, 0.0F);
				}
				else if(dot > bankingDotThreshold)
				{
					currentAcceleration = bankingAccelerationPenalty;
					lookDirection = Vector3.RotateTowards(lookDirection, targetDirection, 
					                                      bankingRotationalSpeed*Time.deltaTime, 0.0F);
				}
				else if(dot > cuttingDotThreshold)
				{
					currentAcceleration = cuttingAccelerationPenalty;
					moveSpeed += cuttingAccelerationPenalty;
					lookDirection = Vector3.RotateTowards(lookDirection, targetDirection, 
					                                      cuttingRotationalSpeed*Time.deltaTime, 0.0F);
				}
				else
				{
					currentAcceleration = brakeMagnitude;
					moveSpeed += brakeMagnitude;
					if(moveSpeed <= 1.0f)
					{
						lookDirection = Vector3.RotateTowards(lookDirection, targetDirection, 
						                                      stationaryRotationalSpeed*Time.deltaTime, 0.0F);
						moveDirection = Vector3.RotateTowards(moveDirection, lookDirection, 
						                                      stationaryRotationalSpeed*Time.deltaTime, 0.0F);
					}
				}
			}//******************************************************************************************//

			//***************HOVERING REGULATIONS*******************************************************//
			if(_characterState == Hovering)
			{
				hoverTimer -= Time.deltaTime;
				if(hoverTimer < 0)				//Time to start falling to the ground
				{
					lastState = Hovering;
					_characterState = Jumping;
				}
				else if(currentRadians != 0 && swirlBuffer < 0)
				{
					_characterState = Swirling;
					lastState = Hovering;
					swirlingTime = 0.0f;
				}
				swirlBuffer -= Time.deltaTime;
			}
			else //Apply stick rotation logics to convert to the hovering state
			{
				headEmblem.stop = false;
				headEmblem.angle = (hoveringParameter/hoveringThreshold)*headEmblem.maxAngle;
				headEmblem.edgeLength = (hoveringParameter/hoveringThreshold)*headEmblem.maxEdge;
				if(hoveringParameter > hoveringThreshold)
				{
					hoveringParameter = 0;
					hoverTimer = hoverTimerLength;
					swirlBuffer = swirlBufferLength;
					_characterState = Hovering;
						lastState = Idle;
				}
				else
				{
					hoveringParameter -= hoveringDegradation;
					hoveringParameter = Mathf.Clamp(hoveringParameter, 0, hoveringThreshold+1);
					interval = Mathf.Abs(interval);	//permit rotation either way
					
					if(interval > RotationSkip)
					{}
					else if(interval > RotationResolution)
					{
						hoveringParameter += interval*hoveringIntervalModifier;
					}
				}
			}//*********************************************************************************************//


			moveSpeed -= groundDrag;

			moveDirection.Normalize();
		}
		// In air controls
		else
		{
			Vector3 targetDirection = v * forward + h * right;
			
			//We need to figure out when we are falling e.g. when we've walked off a ledge because collision flags are complete and utter shit
			if((_characterState == Idle || _characterState == Walking || _characterState == Running) && verticalSpeed <= -5.0f)
				_characterState = Jumping;
			
			if (isMoving)
			{
				if(Vector3.Dot(moveDirection, targetDirection) < 0)
				{
					if(moveSpeed > 0)
					{
						currentAcceleration -= accelerationEquilibrium;
						moveSpeed -= currentAcceleration + directionChangePenalty;
					}
					else
						moveDirection = targetDirection;
				}
				else
				{
					currentAcceleration += swirlingAcceleration;
					moveDirection = Vector3.RotateTowards
						(moveDirection, targetDirection, swirlingRotateSpeed*Time.deltaTime, 0);
				}
			}

			if(Input.GetButton("LeftShoulder1"))
			{
				lookTarget = 
					Quaternion.AngleAxis(-rotationalDistance, Vector3.up) * lookDirection;
			}
			else if(Input.GetButton("RightShoulder1"))
			{
				lookTarget = 
					Quaternion.AngleAxis(rotationalDistance, Vector3.up) * lookDirection;
			}

			if(lookTarget != Vector3.zero)
				lookDirection = Vector3.RotateTowards(lookDirection, lookTarget, 
				swirlingRotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);

			if(_characterState == Swirling)
			{
				if(interval == 0)
				{}
				else
				{
					interval = Mathf.Abs(interval);	//permit rotation either way

					if(interval > RotationSkip)
					{}
					else if(interval > RotationResolution)
					{
						float risingSpeed = swirlingMagnitude;
						if(verticalSpeed < 0)
						{
							risingSpeed *= 2/swirlingGravityCoefficient;
							swirlingTime = 0.0f;
						}
						verticalSpeed += risingSpeed;
						moveSpeed += swirlingMagnitude * 0.1f;
					}
				}
				swirlingTime += Time.deltaTime;
				verticalSpeed = Mathf.Clamp(verticalSpeed, terminalVelocity, maxSwirlSpeed);

				spinParameter = verticalSpeed;
				lsysObject.transform.Rotate(Vector3.up, spinParameter);

				if(currentAcceleration > 1.0f)
						currentAcceleration -= accelerationEquilibrium;
			}
		}
		currentAcceleration = Mathf.Clamp(currentAcceleration, 0, maxAcceleration);
		moveSpeed += currentAcceleration;
		moveSpeed = Mathf.Clamp (moveSpeed, 0, runSpeed);
		lastRadians = currentRadians;
	}
	
	void ApplyJumping ()
	{
		// Prevent jumping too fast after each other
		if (lastJumpTime + jumpRepeatTime > Time.time)
		{
			return;
		}
		
		if (IsGrounded()) 
		{
			// Jump
			// - Only when pressing the button down
			// - With a timeout so you can press the button slightly before landing		
			if (canJump && Time.time < lastJumpButtonTime + jumpTimeout) 
			{
				lastState = Idle;	//The exact last state doesn't matter, so long as it is a grounded state
				verticalSpeed = CalculateJumpVerticalSpeed (jumpHeight);
				SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	
	
	void ApplyGravity ()
	{
		if (isControllable)	// don't move player at all if not controllable.
		{
			// Apply gravity
			//var jumpButton = Input.GetButton("Jump");
			
			
			// When we reach the apex of the jump we send out a message
			if (IsJumping() && !jumpingReachedApex && verticalSpeed <= 1.0)
			{
				jumpingReachedApex = true;
				//set hovering here?
				SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
			}
			
			if (IsGrounded ())
			{
				verticalSpeed = 0.0f;
			}
			else if(_characterState == Hovering)
				verticalSpeed = 0.0f;
			else if(_characterState == Swirling && swirlingTime > swirlingGravityRelief)
			{
				verticalSpeed -= gravity*swirlingGravityCoefficient * Time.deltaTime;
			}
			else if(_characterState != Swirling)
			{
				verticalSpeed -= gravity * Time.deltaTime;
			}
		}
	}
	
	float CalculateJumpVerticalSpeed (float targetJumpHeight)
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * targetJumpHeight * gravity);
	}
	
	void DidJump ()
	{
		jumpingReachedApex = false;
		lastJumpTime = Time.time;
		lastJumpButtonTime = -10;
		
		_characterState = Jumping;
	}
	
	void Update() 
	{	
		if (!isControllable)
		{
			// kill all inputs if not controllable.
			Input.ResetInputAxes();
		}

		UpdateSmoothedMovementDirection();
		
		// Apply gravity
		// - extra power jump modifies gravity
		// - controlledDescent mode modifies gravity
		ApplyGravity ();
		
		// Calculate actual motion
		Vector3 movement = moveDirection * moveSpeed + new Vector3 (0, verticalSpeed, 0) /*+ inAirVelocity*/;
		movement *= Time.deltaTime;
		
		// Move the controller
		CharacterController  controller = GetComponent<CharacterController>();
		collisionFlags = controller.Move(movement);
		
		// ANIMATION sector
		if(_animation) {
			if(_characterState == Jumping) 
			{
				if(!jumpingReachedApex) {
					_animation[jumpPoseAnimation.name].speed = jumpAnimationSpeed;
					_animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
					_animation.CrossFade(jumpPoseAnimation.name);
				} else {
					_animation[jumpPoseAnimation.name].speed = -landAnimationSpeed;
					_animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
					_animation.CrossFade(jumpPoseAnimation.name);				
				}
			} 
			else 
			{
				if(controller.velocity.sqrMagnitude < 0.1) {
					_animation.CrossFade(idleAnimation.name);
				}
				else 
				{
					if(_characterState == Running) {
						_animation[runAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0f, runMaxAnimationSpeed);
						_animation.CrossFade(runAnimation.name);	
					}
					else if(_characterState == Walking) {
						_animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0f, walkMaxAnimationSpeed);
						_animation.CrossFade(walkAnimation.name);	
					}
					
				}
			}
		}
		// ANIMATION sector
		
		// Set rotation to the move direction
		if (IsGrounded())
		{
			transform.rotation = Quaternion.LookRotation(lookDirection);
		}	
		else
		{
			Vector3 xzMove = movement;
			xzMove.y = 0;
			if (xzMove.sqrMagnitude > 0.001)
			{
				transform.rotation = Quaternion.LookRotation(lookDirection);
			}
		}	
		
		// We are in jump mode but just became grounded
		if (IsGrounded())
		{
			inAirVelocity = Vector3.zero;
			if (IsJumping ())
			{
				lastState = Jumping; 
				SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		
	}
	
	float GetSpeed () {
		return moveSpeed;
	}
	
	/*Uses CollisionFlag bitmask xxxx (None(0,1), Sides(0,1), Above(0,1), Below(0,1)) and 
	performs AND comparison with 0001. If Below is 0, the operation returns 0 and the
	character is believed to be in the air. If it is 1, the bitmask should be xxx1, meaning
	a collision has occured below the controller*/
	bool IsGrounded () {
		return ((collisionFlags & CollisionFlags.CollidedBelow) != 0);
	}
	
	Vector3 GetDirection () {
		return moveDirection;
	}
	
	bool IsMoving ()
	{
		return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5;
	}
	
	bool HasJumpReachedApex ()
	{
		return jumpingReachedApex;
	}
	
	void Reset ()
	{
		gameObject.tag = "Player";
	}
}