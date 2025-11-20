using Godot;
using System;

[Tool]
public partial class Inventory : Control
{
    private GridContainer Grid;
    private int SlotsX = 12;
    private int SlotsY = 3;
    private int SelectedSlot = -1;
    private NinePatchRect HoverSprite;

	public override void _Ready()
    {
        if (!Engine.IsEditorHint() || this != GetTree().EditedSceneRoot)
            Visible = false;
        
        Grid = GetNode<GridContainer>("GridContainer");
        HoverSprite = GetNode<NinePatchRect>("HoverSprite");
        var slotClass = GD.Load<PackedScene>("res://Scenes/UI/ItemSlot.tscn");
        for (int i=0; i<SlotsX*SlotsY; ++i)
        {
            var instance = slotClass.Instantiate<Panel>();
            int slotIndex = i;
            instance.MouseEntered += () => SelectSlot(slotIndex);
            instance.MouseExited += () => {
                HoverSprite.Hide();
            };
            Grid.AddChild(instance);
        }
    }


    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("toggle_inventory"))
            Visible = !Visible;
        
        if (IsOpen()) {
            if (@event.IsActionPressed("ui_cancel"))
                Visible = false;
                
            if (@event.IsActionPressed("ui_left", true))
                SelectSlot((SelectedSlot / SlotsX * SlotsX) + (SelectedSlot - 1 + SlotsX) % SlotsX);
                
            if (@event.IsActionPressed("ui_right", true))
                SelectSlot((SelectedSlot / SlotsX * SlotsX) + (SelectedSlot + 1) % SlotsX);
                
            if (@event.IsActionPressed("ui_up", true))
                SelectSlot(((SelectedSlot / SlotsX - 1 + SlotsY) % SlotsY * SlotsX) + SelectedSlot % SlotsX);
                
            if (@event.IsActionPressed("ui_down", true))
                SelectSlot(((SelectedSlot / SlotsX + 1) % SlotsY * SlotsX) + SelectedSlot % SlotsX);
        }
    }

	public bool IsOpen()
    {
        return Visible;
    }

    private void SelectSlot(int index)
    {
        Panel slot = Grid.GetChild<Panel>(index);
        HoverSprite.Show();
        HoverSprite.GlobalPosition = slot.GlobalPosition + slot.Size / 2 - HoverSprite.Size / 2;
        SelectedSlot = index;
    }
}
