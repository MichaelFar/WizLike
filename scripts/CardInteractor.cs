using Godot;
using System;

public partial class CardInteractor : Camera3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]float RayLength = 4.0f;
	
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
		// if(Input.IsActionPressed("LeftClick"))
		// {
		// 	PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
		// 	Camera3D cam = this;
		// 	Vector2 mousePos = GetViewport().GetMousePosition();

		// 	Vector3 origin = cam.ProjectRayOrigin(mousePos);
		// 	Vector3 end = origin + cam.ProjectRayNormal(mousePos) * RayLength;
		// 	PhysicsRayQueryParameters3D query = PhysicsRayQueryParameters3D.Create(origin, end);
		// 	query.CollideWithAreas = true;

		// 	Godot.Collections.Dictionary card_check_result = spaceState.IntersectRay(query);
		// 	Godot.Collections.Dictionary plane_check_result = spaceState.IntersectRay(query);
		// 	//GD.Print(result["collider"]);
		// 	if(card_check_result.Keys.Contains("collider"))
		// 	{
		// 		IClickable clickable = card_check_result["collider"].As<Node3D>() as IClickable;
				
		// 		if(plane_check_result.Keys.Contains("collider"))
		// 		{
		// 			//CardDragPlane cardDragPlane = p;
		// 			if(plane_check_result["collider"].Obj is CardDragPlane)
		// 			{
		// 				if(clickable != null)
		// 				{
		// 					if(clickable.CanClick())
		// 					{
		// 						GD.Print("Card clicked");
		// 						clickable.OnClick(plane_check_result["position"].As<Vector3>());
		// 					}
		// 				}
						
		// 			}
		// 		}
		// 	}
			
			
		//}
			
    }

}
