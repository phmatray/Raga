using Godot;
using System;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Raga.Game;
using Raga.Game.Libs;

public partial class Player : CharacterBody2D
{
    [Export] public int Speed = 300;
    private GachaClient _gameClient;
    private string _playerId;
    
    public override void _Ready()
    {
        _playerId = "philippe";
        _gameClient = new GachaClient("http://localhost:5042", _playerId);
        Task.Run(async () =>
        {
            var inventory = await _gameClient.GetInventoryAsync();
            foreach (var item in inventory.Items)
            {
                GD.Print(item);
            }
        });
    }
    
    public override void _PhysicsProcess(double delta)
    {
        var velocity = Vector2.Zero;
    
        if (Input.IsActionPressed("ui_up"))
            velocity.Y -= Speed;
        if (Input.IsActionPressed("ui_down"))
            velocity.Y += Speed;
        if (Input.IsActionPressed("ui_left"))
            velocity.X -= Speed;
        if (Input.IsActionPressed("ui_right"))
            velocity.X += Speed;
    
        Velocity = velocity.Normalized() * Speed;
        MoveAndSlide();
    }
}
