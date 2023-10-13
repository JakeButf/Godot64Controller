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

    PlatformerState.PlayerStateEnumerator playerState;
	PlatformerState.PlayerActionStateEnumerator actionState;
	PlatformerPhysics Physics;

	PlayerState currentState;
	PlayerState lastFrameState;

	
	public override void _Ready()
	{
		actionState = PlatformerState.PlayerActionStateEnumerator.IDLE;
		playerState = PlatformerState.PlayerStateEnumerator.FREE;
		Physics = new PlatformerPhysics(this);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		PlatformerData.Grounded = this.IsOnFloor();
		currentState = PlatformerState.GetStateClass(actionState);

		//State Specific Process
		if (currentState != lastFrameState && lastFrameState != null)
			currentState.Ready();
		currentState.Process(this, (float)delta);
		actionState = currentState.CheckStateSwitch();

		//Physics Calls
		Physics.PhysicsProcess((float)delta);
		this.Velocity = PlatformerData.Velocity;
		MoveAndSlide();

		//Camera
		Gimbal.Position = Gimbal.Position.Lerp(this.GlobalPosition, (float)delta * PlatformerData.CameraLerpFactor);

		lastFrameState = currentState;
	}
}