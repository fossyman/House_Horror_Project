using Godot;
using System;
using System.Collections.Generic;

public class Item : Reference
{
public int ID; // The Item's ID
public enum ItemTypeEnum{Item,Ammo,Weapon};
public int ItemType; 
public bool CanEquip;
public string ItemName; // The name of the item
public string ItemScenePath; // The path to the item's scene
public Node ItemInfo;
public bool Stackable; // Lets you know if the item can be stacked
public int MaxStack; // The max amount of items that can be in one stack
public PackedScene ItemScene;

    public Item(int _ID,int _ItemType,bool _CanEquip,string _Name,string _ItemScenePath,bool _Stackable,int _MaxStack)
    {
        this.ID = _ID;
        this.ItemType = _ItemType;
        this.CanEquip = _CanEquip;
        this.ItemName = _Name;
        this.ItemScenePath = _ItemScenePath;
        this.ItemScene = (PackedScene)ResourceLoader.Load(_ItemScenePath);
        this.Stackable = _Stackable;
        this.MaxStack = _MaxStack;

        if(ItemScenePath != null) // Checks in the items scene path in not empty
        {
            Spatial _Item = (Spatial)ItemScene.Instance(); // Creates an instance of the item from the item scene
            ItemInfo = _Item.GetChild(0);
            _Item.QueueFree();
        }
    }

        public Item(){} // Legit Useless. Just used to stop a godot error
}
