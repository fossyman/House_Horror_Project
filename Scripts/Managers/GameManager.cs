using Godot;
using System;
using Helpers;
public class GameManager : Spatial
{
    public ItemDB _ItemDB = new ItemDB();
    public override void _Ready()
    {
        AddChild(_ItemDB);
        _ItemDB.BuildDatabase();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
