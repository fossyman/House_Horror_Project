using Godot;
using System;
using System.Collections.Generic;

public class InventoryManager : Spatial
{
	private GameManager _GameManager; // Just Gives a reference to the gamemanager
	private Control _InventoryDisplay;
	[Export]private List<Item> CurrentItems = new List<Item>();
	[Export]private List<Spatial> CurrentItemSpatials = new List<Spatial>();
	private float RotationDuration = 4f;
	private Vector3 Radius = new Vector3(2,0,2);
	private float OrbitAngleOffset;
	private Spatial ItemSpatialArea;
	[Export]private bool _CanRotate=true;
	private Vector3 ItemRotation = new Vector3();
	private Tween InventoryTweener;
	public override void _Ready()
	{
		_GameManager = (GameManager)GetTree().Root.GetChild(0); // Gets gamemanager from the root of the map scene
		InventoryTweener = (Tween)GetChild(0).GetChild(2);
		ItemSpatialArea = (Spatial)GetChild(1);
	}

    public override void _Process(float delta)
    {
		if(CurrentItems.Count>0)
		{
    	UpdateItemRotations();
		}
    }

    

	private void AddItemToInventory(int _ID)
	{
    Item _ItemToAdd = _GameManager._ItemDB.GetItem(_ID);
	Spatial ItemScene = (Spatial)_ItemToAdd.ItemScene.Instance();
	ItemSpatialArea.AddChild(ItemScene);
	CurrentItems.Add(_ItemToAdd);
	CurrentItemSpatials.Add(ItemScene);
	UpdateInventoryCircle();
    ResetInventoryRotation();
	}

		private void SetUpInventoryCircle()
		{
        OrbitAngleOffset += 2 * Mathf.Pi / RotationDuration;
		OrbitAngleOffset = Mathf.Wrap(OrbitAngleOffset,-Mathf.Pi,Mathf.Pi);
		UpdateInventoryCircle();
	}

	private void ResetInventoryRotation()
	{
	InventoryTweener.RemoveAll();
	ItemSpatialArea.RotationDegrees = new Vector3(0,-90,0);
	_CanRotate=true;
	}

	private void RotateInventory()
	{
		if(_CanRotate){
		_CanRotate=false;
		InventoryTweener.InterpolateProperty(ItemSpatialArea,"rotation_degrees",ItemSpatialArea.RotationDegrees,ItemSpatialArea.RotationDegrees + new Vector3(0,Mathf.Rad2Deg(Mathf.Pi/CurrentItems.Count*2),0),0.8f,Tween.TransitionType.Sine,Tween.EaseType.InOut);
		InventoryTweener.Start();
		}
	}

    public override void _Input(InputEvent _Event)
    {
       if(Input.IsActionJustReleased("move_back"))
	   {
		   			AddItemToInventory(0);
	   }
       if(Input.IsActionJustReleased("move_left"))
	   {
		RotateInventory();
	   }
    }

    public void UpdateInventoryCircle()
    {
	  if(CurrentItems.Count != 0)
	  {
	  var spacing = 2 * Mathf.Pi / CurrentItems.Count;

	  	for(int x = 0; x < ItemSpatialArea.GetChildCount();x++)
		{
			var newpos = new Vector3();
			Spatial itemfunny = (Spatial)ItemSpatialArea.GetChild(x);
			newpos.x = Mathf.Cos(spacing * x) * Radius.x;
			newpos.z = Mathf.Sin(spacing * x) * Radius.z;
			itemfunny.Translation = newpos;
		}
	  }
    }
	private void UpdateItemRotations()
	{
		ItemRotation.y = Mathf.Wrap(ItemRotation.y,0,360);
		ItemRotation.y+=1;
		for(int x = 0; x < CurrentItemSpatials.Count;x++)
		{
			CurrentItemSpatials[x].RotationDegrees=ItemRotation;
		}
	}
	public void TweenerFinished()
  	{
		GD.Print("Finished");
		_CanRotate=true;
		CheckIfRotationLooped();
  	}

	private void CheckIfRotationLooped()
	{
		if(ItemSpatialArea.RotationDegrees.y == 270)
		{
			ItemSpatialArea.RotationDegrees = new Vector3(0,-90,0);
		}
	}
}
