using UnityEngine;

public interface IMoveCommand : ICommand
{
    void Execute(Vector2 direction);
}
