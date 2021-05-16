using Godot;
using System;

public class PlayerManager : KinematicBody
{
    [Export]private float _MovementSpeed;
    private Vector3 _Velocity;
    
    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
     _Movement(); 
  }


    private void _Movement()
    {
        _Velocity.x = 0;
        _Velocity.z = 0;
        if(Input.IsActionPressed("move_forward"))
        {
        _Velocity += -GlobalTransform.basis.z * _MovementSpeed * Input.GetActionStrength("move_forward");
        }
        if(Input.IsActionPressed("move_back"))
        {
        _Velocity += GlobalTransform.basis.z * _MovementSpeed * Input.GetActionStrength("move_back");
        }

        if(Input.IsActionPressed("move_left"))
        {
        _Velocity += -GlobalTransform.basis.x * _MovementSpeed * Input.GetActionStrength("move_left");
        }

        if(Input.IsActionPressed("move_right"))
        {
        _Velocity += GlobalTransform.basis.x * _MovementSpeed * Input.GetActionStrength("move_right");
        }
        MoveAndSlide(_Velocity);
    }
}
