using Godot;
using System;

public partial class DraggableArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	private bool mouseInArea = false;
	public bool beingDragged = false;
	private PathFollow2D myPath;
	[Export]
	private CollisionShape2D activeDragShape;
	[Export]
	private CollisionShape2D selectShape;
	[Signal]
	public delegate void ReleasedCardEventHandler();
	[Signal]
	public delegate void RightClickedEventHandler(CardContainer my_container);
	public override void _Ready()
	{
		myPath = Owner as CardContainer;
		SelectDragState(false);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(mouseInArea)
		{
			if(Input.IsActionPressed("LeftClick"))
			{
				if(GlobalGameData.Instance.isDragging == false || beingDragged)
				{
					if(!beingDragged)
					{
						SelectDragState(true);
					}
					Reparent(GetTree().Root);
					GlobalGameData.Instance.isDragging = true;
					beingDragged = true;
					Position = GetViewport().GetMousePosition();
				}
			}
			else if(Input.IsActionJustReleased("LeftClick"))
			{
				
				GlobalGameData.Instance.isDragging = false;
				beingDragged = false;
				SelectDragState(false);
				Reparent(myPath);
				Position = Vector2.Zero;
				EmitSignal(SignalName.ReleasedCard);
			}
			if(Input.IsActionJustReleased("RightClick") && !beingDragged)
			{
				EmitSignal(SignalName.RightClicked, myPath);
			}
		}
		
	}
	private void SelectDragState(bool is_dragging)
	{
		activeDragShape.Disabled = !is_dragging;
	}
    public void HandleDrag(Node viewport, InputEvent input_event, int shape_index)
	{
		InputEventMouseMotion mouse_motion_event = input_event as InputEventMouseMotion;
		
	}
	public void OnMouseEnter()
	{
		mouseInArea = true;
	}
	public void OnMouseExit()
	{
		mouseInArea = false;
		
	}
    public override void _Notification(int what)
    {
        base._Notification(what);
		if(what == NotificationWMMouseExit)
		{
			EmitSignal(SignalName.ReleasedCard);
			GlobalGameData.Instance.isDragging = false;
			beingDragged = false;
			Reparent(myPath);
			Position = Vector2.Zero;
			SelectDragState(false);
		}
    }


}
