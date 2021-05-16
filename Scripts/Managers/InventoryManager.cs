using Godot;
using System;
using System.Collections.Generic;

public class InventoryManager : Spatial
{
	private GameManager _GameManager; // Just Gives a reference to the gamemanager
	private Control _InventoryDisplay;
	[Export]private List<Item> CurrentItems = new List<Item>();
	private float RotationDuration = 4f;
	private Vector3 Radius = new Vector3(2,0,2);
	private float OrbitAngleOffset;
	private Spatial ItemSpatialArea;
	private bool _CanRotate=true;

	private Tween InventoryTweener;
	public override void _Ready()
	{
		_GameManager = (GameManager)GetTree().Root.GetChild(0); // Gets gamemanager from the root of the map scene
		InventoryTweener = (Tween)GetChild(0).GetChild(2);
		ItemSpatialArea = (Spatial)GetChild(1);
	}

	private void AddItemToInventory(int _ID)
	{
    Item _ItemToAdd = _GameManager._ItemDB.GetItem(_ID);
	CurrentItems.Add(_ItemToAdd);
	Spatial ItemScene = (Spatial)_ItemToAdd.ItemScene.Instance();
	ItemSpatialArea.AddChild(ItemScene);
	ItemSpatialArea.RotationDegrees = new Vector3(0,-90,0);
	UpdateInventoryCircle();
	}

		private void SetUpInventoryCircle()
		{
        OrbitAngleOffset += 2 * Mathf.Pi / RotationDuration;
		OrbitAngleOffset = Mathf.Wrap(OrbitAngleOffset,-Mathf.Pi,Mathf.Pi);
		UpdateInventoryCircle();
	}

	private void RotateInventory()
	{
		if(_CanRotate){
		InventoryTweener.InterpolateProperty(ItemSpatialArea,"rotation_degrees",ItemSpatialArea.RotationDegrees,ItemSpatialArea.RotationDegrees + new Vector3(0,Mathf.Rad2Deg(Mathf.Pi/CurrentItems.Count*2),0),0.5f,Tween.TransitionType.Linear,Tween.EaseType.InOut);
		InventoryTweener.Start();
		_CanRotate=false;
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
	public void TweenerFinished()
  	{
		_CanRotate=true;
  	}
}