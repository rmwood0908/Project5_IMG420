using Godot;
using System.Collections.Generic;

public partial class PhysicsChain : Node2D
{
	[Export] public int ChainSegments = 5;
	[Export] public float SegmentDistance = 30f;
	[Export] public PackedScene SegmentScene;
	
	private List<RigidBody2D> _segments = new List<RigidBody2D>();
	private List<Joint2D> _joints = new List<Joint2D>();
	
	public override void _Ready()
	{
		CreateChain();
	}
	
	private void CreateChain()
	{
		// Create all the segments
		for (int i = 0; i < ChainSegments; i++)
		{
			// Instantiate the segment scene
			RigidBody2D segment = SegmentScene.Instantiate<RigidBody2D>();
			AddChild(segment);
			
			// Position each segment vertically below the previous one
			segment.Position = new Vector2(0, i * SegmentDistance);
			
			// Configure the segment
			if (i == 0)
			{
				// First segment is static (anchored at the top)
				segment.GravityScale = 0f;
				segment.LockRotation = true;
			}
			else
			{
				// Other segments are dynamic
				segment.GravityScale = 1f;
				segment.LockRotation = false;
			}
			
			_segments.Add(segment);
		}
		
		// Create a StaticBody2D to anchor the first segment
		StaticBody2D anchorPoint = new StaticBody2D();
		AddChild(anchorPoint);
		anchorPoint.Position = _segments[0].Position;
		
		// Connect first segment to the anchor with a pin joint
		PinJoint2D anchorJoint = new PinJoint2D();
		AddChild(anchorJoint);
		anchorJoint.NodeA = anchorPoint.GetPath();
		anchorJoint.NodeB = _segments[0].GetPath();
		anchorJoint.Softness = 0.1f;
		_joints.Add(anchorJoint);
		
		// Now connect the segments to each other
		for (int i = 0; i < ChainSegments - 1; i++)
		{
			// Create a PinJoint2D to connect segment i to segment i+1
			PinJoint2D joint = new PinJoint2D();
			AddChild(joint);
			
			// Set which nodes the joint connects
			joint.NodeA = _segments[i].GetPath();
			joint.NodeB = _segments[i + 1].GetPath();
			
			// Configure joint properties
			joint.Softness = 0.5f;
			joint.Bias = 0.7f;
			
			_joints.Add(joint);
		}
	}
	
	public void ApplyForceToSegment(int segmentIndex, Vector2 force)
	{
		if (segmentIndex >= 0 && segmentIndex < _segments.Count)
		{
			_segments[segmentIndex].ApplyForce(force);
		}
	}
}
