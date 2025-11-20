using Godot;
using System;

enum ELookDirection {
	Right,
	Left
}
public partial class Player : CharacterBody2D
{
	public const float Speed = 200.0f;
	
	private AnimatedSprite2D Sprite;
	private Inventory InventoryUI;
	
	private ELookDirection LookDirection = ELookDirection.Right;


	public override void _Ready()
	{
		this.Sprite = GetNode<AnimatedSprite2D>("Sprite");
		this.InventoryUI = GetTree().Root.GetNode("Game").GetNode("HUD").GetNode<Inventory>("Inventory");
	}

	private void _UpdateAnimation(Vector2 direction) {
		if (direction.X < 0)
			this.LookDirection = ELookDirection.Left;
		else if (direction.X > 0)
			this.LookDirection = ELookDirection.Right;
		
		string suffix = this.LookDirection == ELookDirection.Left ? "_L" : "_R";
		if (direction.Length() > 0) {
			this.Sprite.Play($"walk{suffix}");
		}
		else {
			this.Sprite.Play($"idle{suffix}");
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!this.InventoryUI.IsOpen()) {
			Vector2 velocity = Velocity;
			Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
			if (direction != Vector2.Zero)
			{
				velocity = direction.Normalized() * Speed;
			}
			else
			{
				velocity = Vector2.Zero;
			}
			Velocity = velocity;
			MoveAndSlide();
			_UpdateAnimation(direction);
		}
		else
        {
			_UpdateAnimation(Vector2.Zero);
        }
	}
}
