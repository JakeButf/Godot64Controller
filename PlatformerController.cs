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
	[Export] public GpuParticles3D StepParticles;
	[Export] public GpuParticles3D WallSlideParticles;
	[Export] public GpuParticles3D SparkleParticles;

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
		if(!PlatformerData.Grounded)
			PlatformerData.GroundTimer.Reset();

		if (PlatformerData.GroundTimer.time > PlatformerData.JumpModWindow)
			PlatformerData.JumpIterator = 0;

        actionState = currentState.CheckStateSwitch();
        currentState = PlatformerState.GetStateClass(actionState);
		

		//State Specific Process
		if (currentState.ToString() != lastActionState)
		{
            currentState.Ready();
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
            Vector2 lookDirection = new Vector2(PlatformerData.Velocity.Z, PlatformerData.Velocity.X);
            this.Model.Rotation = new Vector3(this.Model.Rotation.X, Mathf.LerpAngle(this.Model.Rotation.Y, lookDirection.Angle(), (float)delta * 12), this.Model.Rotation.Z);
        }

		if((currentState.Flags() & PlayerState.ACT_FLAG_CONTROL_JUMP_HEIGHT) != 0 && !Input.IsActionPressed(PlatformerInput.JumpAxis))
		{
			PlatformerData.GravityMod += 1.2f;
		}
	}

    void Debug()
    {
        db = GetNode<al_debuginfo>("/root/AlDebuginfo");

        db.debugInfo.Add(actionState.ToString());
    }
}
