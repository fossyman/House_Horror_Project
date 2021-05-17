using Godot;
using System;
using System.Collections.Generic;

public class ItemDB : Node
{

public List<Item> items = new List<Item>();

    public Item GetItem(int id)
    {
        return items.Find(items => items.ID == id);
    }
    public void BuildDatabase()
    {
    items.Add( new Item(0,1,false,"Test Item","res://Test Item.tscn",true,5) );
    }



}
