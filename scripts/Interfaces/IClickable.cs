using Godot;
using System;

public interface IClickable
{
    public void OnClick(Vector3 mouse_pos);
    public void OnRelease();

    public bool CanClick();
}
