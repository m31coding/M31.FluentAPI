namespace M31.FluentApi.Generator.CodeGeneration.CodeBoardActors.BuilderStepsGeneration;

internal class BaseInterface
{
    public BaseInterface(string name, int step)
    {
        Name = name;
        Step = step;
    }

    public string Name { get; }
    public int Step { get; }
}