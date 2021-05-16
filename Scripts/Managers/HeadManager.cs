using Godot;
using System;

public class HeadManager : Spatial
{
    private Camera _Camera;
    [Export]private Vector3 _HeadRotation;
    private KinematicBody _Player;
    [Export]private float _CurrSens = 5;
    public override void _Ready()
    {
        _Camera = (Camera)GetChild(0);
        _Player = (KinematicBody)Owner;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
      
  }

    public override void _Input(InputEvent _Event)
    {
        if(_Event is InputEventMouseMotion _MouseMotionEvent)
        {
			_HeadRotation.x -= (Mathf.Deg2Rad(_MouseMotionEvent.Relative.y * 1 * _CurrSens));
			_HeadRotation.x = Mathf.Clamp(_HeadRotation.x, -1.6f,1.6f);
            Rotation = _HeadRotation;
            _Player.RotateY(-Mathf.Deg2Rad(_MouseMotionEvent.Relative.x * 1 * _CurrSens));
        }
    }

}
