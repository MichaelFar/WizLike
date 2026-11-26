using Godot;
using System;


public partial class DraggableRigidBody : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	
	
	[Signal]
	public delegate void ReleasedCardEventHandler();
	[Export]public Area2D areaToFollow;
	[Export]public Marker2D topPoint;
	[Export]public BaseCardControlScene myCardScene;
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		
		Vector2 drag_force = GlobalPosition.DirectionTo(areaToFollow.GlobalPosition) * (850.0f) * GlobalPosition.DistanceTo(areaToFollow.GlobalPosition);
		ConstantForce = drag_force;
		if(RotationDegrees != 0.0f)
		{
			AngularVelocity = Mathf.Pi * (GetAngleTo(topPoint.GlobalPosition) - Mathf.DegToRad(RotationDegrees - 90.0f) - Rotation);
			if(Mathf.Abs(GetAngleTo(topPoint.GlobalPosition) - Mathf.DegToRad(RotationDegrees - 90.0f) - Rotation) <= 0.001)
			{
				RotationDegrees = 0.0f;
			}
		}
		
			
    }
	

    

}
