using UnityEngine;

public interface ICommand
{
    void Execute(Renderer target);
}