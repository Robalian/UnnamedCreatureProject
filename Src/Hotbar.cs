using Godot;
using System;

[Tool]
public partial class Hotbar : Control
{
    private GridContainer Grid;
    private int SlotsX = 12;
    private int SelectedSlot = 0;
    private NinePatchRect HoverSprite;

	public override void _Ready()
    {   
        Grid = GetNode<GridContainer>("GridContainer");
        HoverSprite = GetNode<NinePatchRect>("HoverSprite");
        var slotClass = GD.Load<PackedScene>("res://Scenes/UI/ItemSlot.tscn");
        for (int i=0; i<SlotsX; ++i)
        {
            var instance = slotClass.Instantiate<Panel>();
            Grid.AddChild(instance);
        }
		Callable.From(() => {
			SelectSlot(0);
		}).CallDeferred();
    }


    public override void _Input(InputEvent @event)
    {
        if (Visible)
		{
			if (@event.IsActionPressed("hotbar_left") && Input.IsActionJustPressed("hotbar_left"))
				SelectSlot((SelectedSlot - 1 + SlotsX) % SlotsX);
				
			if (@event.IsActionPressed("hotbar_right") && Input.IsActionJustPressed("hotbar_right"))
				SelectSlot((SelectedSlot + 1) % SlotsX);
				
			if (@event.IsActionPressed("hotbar_1"))
				SelectSlot(0);
			if (@event.IsActionPressed("hotbar_2"))
				SelectSlot(1);
			if (@event.IsActionPressed("hotbar_3"))
				SelectSlot(2);
			if (@event.IsActionPressed("hotbar_4"))
				SelectSlot(3);
			if (@event.IsActionPressed("hotbar_5"))
				SelectSlot(4);
			if (@event.IsActionPressed("hotbar_6"))
				SelectSlot(5);
			if (@event.IsActionPressed("hotbar_7"))
				SelectSlot(6);
			if (@event.IsActionPressed("hotbar_8"))
				SelectSlot(7);
			if (@event.IsActionPressed("hotbar_9"))
				SelectSlot(8);
			if (@event.IsActionPressed("hotbar_10"))
				SelectSlot(9);
			if (@event.IsActionPressed("hotbar_11"))
				SelectSlot(10);
			if (@event.IsActionPressed("hotbar_12"))
				SelectSlot(11);
		}
    }

    private void SelectSlot(int index)
    {
        Panel slot = Grid.GetChild<Panel>(index);
        HoverSprite.GlobalPosition = slot.GlobalPosition + slot.Size / 2 - HoverSprite.Size / 2;
        SelectedSlot = index;
    }
}
