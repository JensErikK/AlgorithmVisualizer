
namespace AlgorithmVisualizer.Client.Algorithms.Sorting;

public class DefaultSort: ISorter
{
    public async Task Sort(CancellationToken ct)
    {
        await Task.Delay(0, ct);
    }
}

