using Godot;
using System;
using BloodTribute;
using System.Collections.Generic;

public partial class Character : CharacterBody2D
{
    public AnimatedSprite2D Sprite { get; private set; }
    public CollisionShape2D CollisionShape { get; private set; }
    protected IPuppeteer Puppeteer { get; set; }
    public List<Item> Inventory { get; private set; }
    public Area2D ReachArea { get; private set; }
    [Export]
    public float Speed { get; set; }

    [Export]
    public float Gravity { get; set; }

    public event Action InventoryChanged;

    public override void _Ready()
    {
        Sprite = GetNode<AnimatedSprite2D>("Sprite");
        ReachArea = GetNode<Area2D>("ReachArea");
        CollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
        Inventory = [];
        base._Ready();
    }

    public void InsertItem(Item item)
    {
        Inventory.Add(item);
        InventoryChanged?.Invoke();
    }

    public Item RemoveItem(Item item)
    {
        Inventory.Remove(item);
        InventoryChanged?.Invoke();
        return item;
    }

    public override void _PhysicsProcess(double delta)
    {
        Puppeteer.PhysicsProcess(delta);
        MoveAndSlide();
    }

    public override void _Process(double delta)
    {
        Puppeteer.Process(delta);
    }
}
