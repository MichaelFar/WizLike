using Godot;
using System;

public partial class Base3dCard : Area3D, IClickable
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
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

}
