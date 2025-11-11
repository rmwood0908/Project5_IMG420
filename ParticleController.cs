using Godot;

public partial class ParticleController : GpuParticles2D
{
	private ShaderMaterial _shaderMaterial;
	
	public override void _Ready()
	{
		// Create a new particle process material
		var particleMaterial = new ParticleProcessMaterial();
		
		// Set emission properties on the process material
		particleMaterial.EmissionShape = ParticleProcessMaterial.EmissionShapeEnum.Sphere;
		particleMaterial.EmissionSphereRadius = 20f;
		
		// Apply the process material
		ProcessMaterial = particleMaterial;
		
		// Create and apply the shader material (for the canvas shader)
		_shaderMaterial = new ShaderMaterial();
		_shaderMaterial.Shader = GD.Load<Shader>("res://custom_particle.gdshader");
		
		// Apply shader material to rendering
		Material = _shaderMaterial;
		
		// Configure particle system properties on GpuParticles2D
		Amount = 50;
		Lifetime = 3.0f;
		SpeedScale = 1.0f;
		Emitting = true;
	}
	
	public override void _Process(double delta)
	{
		// The shader's TIME uniform updates automatically
	}
}
