using Godot;
using System;

public partial class Base3dCard : Area3D, IClickable
{
	// Called when the node enters the scene tree for the first time.
    private float startingZPosition = 0.0f;
    
    [Export] MeshInstance3D cardMesh;
	public override void _Ready()
	{
		startingZPosition = Position.Z;
        
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

    public void OnClick(Vector3 mouse_pos)
    {
       
		GlobalPosition = mouse_pos;
		GD.Print(mouse_pos);
    }

    public void OnRelease()
    {
        return;
    }

    public bool CanClick()
    {
        return true;
    }
    public void DragCard(Node camera, InputEvent this_event, 
    Vector3 event_position, Vector3 normal, int shape_index)
	{
        
        if(event_position != Vector3.Zero && Input.IsActionPressed("LeftClick"))
        {
            Camera3D camera3D = camera as Camera3D;
            
            Vector3 ray_origin = camera3D.ProjectLocalRayNormal(GetViewport().GetMousePosition()) * 3f;
            GD.Print(ray_origin);
            
            
            Vector3 correction_vector = new Vector3(ray_origin.X, ray_origin.Y, startingZPosition);// * 20;
            // - ((GlobalPosition.DistanceTo(camera3D.GlobalPosition) * GlobalPosition.DirectionTo(camera3D.GlobalPosition)));//.Normalized();
            Position = correction_vector;
            //Position = ToLocal(new Vector3(event_position.X, event_position.Y, startingZPosition));
            //Position = new Vector3(Position.X + mouse_motion_event.Relative.X,Position.Y +  mouse_motion_event.Relative.Y, Position.Z);
        }
        
		// if(this_event.IsActionPressed("LeftClick"))
        // {
        //     if(event_position != Vector3.Zero)
        //     {
        //         GlobalPosition = event_position;
        //         GD.Print(event_position);
        //     }
        // }
        
	}

    


}
