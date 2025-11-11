using Godot;

public partial class LaserDetector : Node2D
{
	[Export] public float LaserLength = 500f;
	[Export] public Color LaserColorNormal = Colors.Green;
	[Export] public Color LaserColorAlert = Colors.Red;
	[Export] public NodePath PlayerPath;
	
	private RayCast2D _rayCast;
	private Line2D _laserBeam;
	private Node2D _player;
	private bool _isAlarmActive = false;
	private Timer _alarmTimer;
	private float _alarmFlashTimer = 0f;
	
	public override void _Ready()
	{
		SetupRaycast();
		SetupVisuals();
		GetPlayer();
		SetupAlarmTimer();
	}
	
	private void SetupRaycast()
	{
		// Create a new RayCast2D node
		_rayCast = new RayCast2D();
		AddChild(_rayCast);
		
		// Point the raycast to the right
		_rayCast.TargetPosition = new Vector2(LaserLength, 0);
		
		// Set collision mask to detect the player layer
		// Make sure your player is on a collision layer that this can detect
		_rayCast.CollisionMask = 1;  // Adjust based on your layer setup
	}
	
	private void SetupVisuals()
	{
		// Create a Line2D for laser visualization
		_laserBeam = new Line2D();
		AddChild(_laserBeam);
		
		// Configure the laser beam appearance
		_laserBeam.Width = 3f;
		_laserBeam.DefaultColor = LaserColorNormal;
		
		// Add the starting point (origin)
		_laserBeam.AddPoint(Vector2.Zero);
		// Add the ending point
		_laserBeam.AddPoint(new Vector2(LaserLength, 0));
	}
	
	private void GetPlayer()
	{
		if (PlayerPath != null)
		{
			_player = GetNode<Node2D>(PlayerPath);
		}
	}
	
	private void SetupAlarmTimer()
	{
		_alarmTimer = new Timer();
		AddChild(_alarmTimer);
		_alarmTimer.OneShot = false;
		_alarmTimer.WaitTime = 0.1f;  // Flash every 0.1 seconds
		_alarmTimer.Timeout += OnAlarmTimerTimeout;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		// Force the raycast to update its collision check
		_rayCast.ForceRaycastUpdate();
		
		// Update the laser beam visualization
		UpdateLaserBeam();
		
		// Check if the raycast is hitting something
		if (_rayCast.IsColliding())
		{
			// Get the collider and cast it to Node
			Node collider = (Node)_rayCast.GetCollider();
			
			// Check if it hit the player
			if (collider == _player || collider.IsAncestorOf(_player))
			{
				TriggerAlarm();
			}
			else
			{
				ResetAlarm();
			}
		}
		else
		{
			ResetAlarm();
		}
	}
	
	private void UpdateLaserBeam()
	{
		// Clear existing points
		_laserBeam.ClearPoints();
		
		// Start point at origin
		_laserBeam.AddPoint(Vector2.Zero);
		
		// Check if raycast hit something
		if (_rayCast.IsColliding())
		{
			// End point at collision
			Vector2 collisionPoint = _rayCast.GetCollisionPoint();
			_laserBeam.AddPoint(collisionPoint);
		}
		else
		{
			// End point at full laser length
			_laserBeam.AddPoint(new Vector2(LaserLength, 0));
		}
	}
	
	private void TriggerAlarm()
	{
		if (!_isAlarmActive)
		{
			_isAlarmActive = true;
			_alarmTimer.Start();
			GD.Print("ALARM! Player detected!");
		}
	}
	
	private void ResetAlarm()
	{
		if (_isAlarmActive)
		{
			_isAlarmActive = false;
			_alarmTimer.Stop();
			_laserBeam.DefaultColor = LaserColorNormal;
		}
	}
	
	private void OnAlarmTimerTimeout()
	{
		// Flash the laser between normal and alert color
		if (_laserBeam.DefaultColor == LaserColorNormal)
		{
			_laserBeam.DefaultColor = LaserColorAlert;
		}
		else
		{
			_laserBeam.DefaultColor = LaserColorNormal;
		}
	}
}
