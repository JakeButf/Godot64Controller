using Godot;
using System;
using n64proofofconcept.scripts.player.platformercontroller;
using n64proofofconcept.scripts.player.platformercontroller.states;

public partial class PlatformerController : CharacterBody3D
{
	//External
	[Export] public Node3D Gimbal;
	[Export] public AnimationPlayer Anim;
	[Export] public Node3D Model;
	[Export] public RayCast3D WallDetector;
	[Export] public RayCast3D LedgeDetector;
	[Export] public RayCast3D LedgeHeightDetector;
	[Export] public GpuParticles3D StepParticles;
	[Export] public GpuParticles3D WallSlideParticles;
	[Export] public GpuParticles3D SparkleParticles;
	[Export] public CollisionShape3D Col_Normal;
	[Export] public CollisionShape3D Col_Small;

	internal PlatformerState.PlayerStateEnumerator playerState;
	internal PlatformerState.PlayerActionStateEnumerator actionState;
	string lastActionState;
	internal PlatformerPhysics Physics;

	PlayerState currentState;
	PlayerState lastFrameState;

	al_debuginfo db;

	
	public override void _Ready()
	{
		actionState = PlatformerState.PlayerActionStateEnumerator.IDLE;
		playerState = PlatformerState.PlayerStateEnumerator.FREE;
		Physics = new PlatformerPhysics(this);
        currentState = PlatformerState.GetStateClass(actionState);
		WallDetector.Enabled = true;

		PlatformerData.GroundTimer = new PlatformerTimer();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//PlatformerData.Velocity = this.Velocity;
        PlatformerData.Grounded = this.IsOnFloor();

		//Timer Calls
		PlatformerTimer.ProcessTimers((float)delta);
		if (!PlatformerData.Grounded)
			PlatformerData.GroundTimer.Reset();
		else
			PlatformerData.DiveUsed = false;

		if (PlatformerData.GroundTimer.time > PlatformerData.JumpModWindow)
			PlatformerData.JumpIterator = 0;

        actionState = currentState.CheckStateSwitch();
        currentState = PlatformerState.GetStateClass(actionState);
		

		//State Specific Process
		if (currentState.ToString() != lastActionState)
		{
            currentState.Ready(this);
        }


		//Physics Calls
		PlatformerData.GravityMod = 1f;


        DoFlagActions((float)delta);
        Physics.PhysicsProcess((float)delta);

        currentState.Process(this, (float)delta);

		this.Velocity = PlatformerData.Velocity;
		MoveAndSlide();

		//Camera
		Gimbal.Position = Gimbal.Position.Lerp(this.GlobalPosition, (float)delta * PlatformerData.CameraLerpFactor);

		lastActionState = new string (currentState.ToString());

		PlatformerAnimation.UpdateAnimation(this);

		Debug();
	}

    public override void _PhysicsProcess(double delta)
    {
		Physics.PhysicsTickProcess((float)delta);
    }

    void DoFlagActions(float delta)
	{
		if((currentState.Flags() & PlayerState.ACT_FLAG_ALLOW_MODEL_ROTATION) != 0)
		{
			//Turn player
			RotateToPlayerVelocity(delta, 12);
        }

		if((currentState.Flags() & PlayerState.ACT_FLAG_CONTROL_JUMP_HEIGHT) != 0 && !Input.IsActionPressed(PlatformerInput.JumpAxis))
		{
			PlatformerData.GravityMod += 1.2f;
		}


        DeactiveCollisions();
        if ((currentState.Flags() & PlayerState.ACT_FLAG_SHORT_HITBOX) != 0)
		{
			this.Col_Small.Disabled = false;
		} else if((currentState.Flags() & PlayerState.ACT_FLAG_INTANGIBLE) != 0)
		{
            //Leave collisions off
        } else
		{
            this.Col_Normal.Disabled = false;
        }
	}

	public void DeactiveCollisions()
	{
		this.Col_Normal.Disabled = true;
        this.Col_Small.Disabled = true;
    }

	public void RotateToPlayerVelocity(float delta, float speed)
	{
        Vector2 lookDirection = new Vector2(PlatformerData.Velocity.Z, PlatformerData.Velocity.X);
        this.Model.Rotation = new Vector3(this.Model.Rotation.X, Mathf.LerpAngle(this.Model.Rotation.Y, lookDirection.Angle(), (float)delta * speed), this.Model.Rotation.Z);
    }

	public void InstantRotateToPlayerVelocity()
	{
        Vector2 lookDirection = new Vector2(PlatformerData.Velocity.Z, PlatformerData.Velocity.X);
		this.Model.Rotation = new Vector3(Model.Rotation.X, lookDirection.Angle(), Model.Rotation.Z);
    }

	public void ApplyVerticalForce(float force)
	{
		PlatformerData.Velocity = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z);
		this.actionState = PlatformerState.PlayerActionStateEnumerator.FALL;
        currentState = PlatformerState.GetStateClass(actionState);
        PlatformerData.Velocity += new Vector3(0, force, 0);
	}

    void Debug()
    {
        db = GetNode<al_debuginfo>("/root/AlDebuginfo");

        db.debugInfo.Add(actionState.ToString());
		Vector3 hSpeed = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z);
		//db.debugInfo.Add(hSpeed.Length().ToString() + "u/s");
    }
}
