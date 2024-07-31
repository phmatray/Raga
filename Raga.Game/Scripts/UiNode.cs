using Godot;
using R3;

namespace Raga.Game.Scripts;

public class UiState
{
    public ReactiveProperty<int> Counter { get; }
    public ReadOnlyReactiveProperty<string> LabelText { get; }

    public UiState()
    {
        Counter = new ReactiveProperty<int>(0);
        
        LabelText = Counter
            .Select(x => $"Counter: {x}")
            .ToReadOnlyReactiveProperty();
    }
}

public partial class MainNode2D : Godot.Node2D
{
    private readonly CompositeDisposable _subscriptions = [];
    private readonly UiState _uiState = new();
    
    private Label _mainLabel;
    private Button _button;

    public override void _Ready()
    {
        InitializeNodes();
        InitializeSubscriptions();
    }

    private void InitializeNodes()
    {
        _mainLabel = GetNode<Label>("MainLabel");
        _button = GetNode<Button>("Button");
    }

    private void InitializeSubscriptions()
    {
        _uiState.LabelText
            .SubscribeToLabel(_mainLabel)
            .AddTo(_subscriptions);

        _button
            .OnPressedAsObservable()
            .Do(_ => _uiState.Counter.Value++)
            .Subscribe()
            .AddTo(_subscriptions);
    }

    public override void _ExitTree()
    {
        _subscriptions.Dispose();
    }
}