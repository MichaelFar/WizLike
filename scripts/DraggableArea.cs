using Godot;
using System;

public partial class DraggableArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	private bool mouseInArea = false;
	public bool beingDragged = false;
	private PathFollow2D myPath;
	[Signal]
	public delegate void ReleasedCardEventHandler();
	[Signal]
	public delegate void RightClickedEventHandler(CardContainer my_container);
	public override void _Ready()
	{
		myPath = Owner as CardContainer;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    public void HandleDrag(Node viewport, InputEvent input_event, int shape_index)
	{
		InputEventMouseMotion mouse_motion_event = input_event as InputEventMouseMotion;
		
		if(Input.IsActionPressed("LeftClick"))
		{
			if(mouse_motion_event != null 
			&& mouseInArea 
			&& GlobalGameData.Instance.isDragging == false 
			|| mouse_motion_event != null 
			&& mouseInArea 
			&& beingDragged)
			{
				Reparent(GetTree().Root);
				GlobalGameData.Instance.isDragging = true;
				beingDragged = true;
				Position = GetViewport().GetMousePosition();
			}
		}
		else if(Input.IsActionJustReleased("LeftClick") && mouseInArea)
		{
			EmitSignal(SignalName.ReleasedCard);
			GlobalGameData.Instance.isDragging = false;
			beingDragged = false;
			Reparent(myPath);
			Position = Vector2.Zero;
		}
		if(Input.IsActionJustReleased("RightClick") && mouseInArea && !beingDragged)
		{
			EmitSignal(SignalName.RightClicked, myPath);
		}
	}
	public void OnMouseEnter()
	{
		mouseInArea = true;
	}
	public void OnMouseExit()
	{
		mouseInArea = false;
	}

}
