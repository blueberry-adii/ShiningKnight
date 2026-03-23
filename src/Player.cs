using Godot;
using System;
using System.Threading.Tasks;

public partial class Player : CharacterBody2D
{
    public const float Speed = 100.0f;
    public const float JumpVelocity = -300.0f;
    public int Health = 5;
    bool IsDead = false;
    bool TakingDamage = false;
    AnimatedSprite2D PlayerSprite;
    Timer timer;
    AudioStreamPlayer JumpSFX;
    AudioStreamPlayer HurtSFX;

    public override void _Ready()
    {
        PlayerSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        timer = GetNode<Timer>("Timer");
        JumpSFX = GetNode<AudioStreamPlayer>("Jump");
        HurtSFX = GetNode<AudioStreamPlayer>("Hurt");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity += GetGravity() * (float)delta;

        if (!IsDead && !TakingDamage)
        {
            // Handle Jump.
            if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            {
                velocity.Y = JumpVelocity;
                JumpSFX.Play();
            }

            // Get the input direction and handle the movement/deceleration.
            // As good practice, you should replace UI actions with custom gameplay actions.
            Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
            if (direction != Vector2.Zero)
            {
                velocity.X = direction.X * Speed;
                PlayerSprite.FlipH = direction.X < 0;
            }
            else
            {
                velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            }

            if (!IsOnFloor())
                PlayerSprite.Play("jump");
            else if (direction != Vector2.Zero)
                PlayerSprite.Play("run");
            else
                PlayerSprite.Play("idle");
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public async Task TakeDamage(int damage)
    {
        Vector2 velocity = Velocity;
        if (IsDead) return;

        TakingDamage = true;
        HurtSFX.Play();
        Health -= damage;

        if (Health <= 0)
        {
            TakingDamage = false;
            Die();
        }
        else
        {
            PlayerSprite.Play("hit");
            velocity.Y = -150;
            Velocity = velocity;
            await ToSignal(PlayerSprite, "animation_finished");
            TakingDamage = false;
        }

        GD.Print("Health: " + Health);
    }

    public void Die()
    {
        if (IsDead) return;
        HurtSFX.Play();
        IsDead = true;
        Health = 0;
        Velocity = Vector2.Zero;
        Engine.TimeScale = 0.5;
        timer.Start();
        PlayerSprite.Play("death");
    }

    public void _OnTimerTimeout()
    {
        Engine.TimeScale = 1.0;
        GetTree().ReloadCurrentScene();
    }
}
