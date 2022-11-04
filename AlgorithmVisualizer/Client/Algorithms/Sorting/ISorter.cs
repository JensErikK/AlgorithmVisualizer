namespace AlgorithmVisualizer.Client.Algorithms.Sorting
{
    public interface ISorter
    {
        Task Sort(CancellationToken ct);
    }
}
