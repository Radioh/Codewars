namespace Codewars.Solutions.Core
{
    public interface ITask
    {
        string Name { get; }
        string Rank { get; }
        string Link { get; }

        string Run();
    }
}
