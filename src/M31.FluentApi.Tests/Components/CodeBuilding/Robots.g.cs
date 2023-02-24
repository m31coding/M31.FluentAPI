using System.Collections.Generic;

namespace M31.FluentApi.Tests.Components;

public class Robot : IMove
{
    private readonly List<string> tasks;

    public string Name { get; }
    public int YearOfManufacture { get; }
    public double PosX { get; private set; }
    public double PosY { get; private set; }
    public IReadOnlyCollection<string> Tasks => tasks;

    public Robot(string name, int yearOfManufacture, double posX = 0, double posY = 0)
    {
        Name = name;
        YearOfManufacture = yearOfManufacture;
        tasks = new List<string>();
        PosX = posX;
        PosY = posY;
    }

    public void Move(double deltaX, double deltaY)
    {
        PosX += deltaX;
        PosY += deltaY;
    }

    public void AssignTask(string task)
    {
        tasks.Add(task);
    }
}

public interface IMove
{
    void Move(double deltaX, double deltaY);
}