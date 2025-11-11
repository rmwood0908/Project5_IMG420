using Godot;
public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 200f;
	
	public override void _Ready()
	{
		// Start the player at a visible location
		Position = new Vector2(200, 100);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;
		
		// Get input
		if (Input.IsActionPressed("ui_right"))
			velocity.X += 1;
		if (Input.IsActionPressed("ui_left"))
			velocity.X -= 1;
		if (Input.IsActionPressed("ui_down"))
			velocity.Y += 1;
		if (Input.IsActionPressed("ui_up"))
			velocity.Y -= 1;
		
		// Normalize and apply speed
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
