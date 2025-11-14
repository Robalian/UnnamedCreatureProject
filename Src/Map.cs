using Godot;
using System;

public partial class Map : Node
{
	private TileMapLayer Ground;
	
	public override void _Ready()
	{
		this.Ground = GetNode<TileMapLayer>("Ground");
		_Generate();
	}
	
	private void _Generate() {
		Random r = new Random();
		for (int y=-200; y<=200; ++y)
		{
			for (int x=-200;x<=200; ++x)
			{
				this.Ground.SetCell(new Vector2I(x,y), 0, new Vector2I(r.Next(0, 2) ,0));
			}
		}
	}

	public override void _Process(double delta)
	{
	}
}
