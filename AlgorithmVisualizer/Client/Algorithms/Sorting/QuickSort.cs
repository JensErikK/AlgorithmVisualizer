
namespace AlgorithmVisualizer.Client.Algorithms.Sorting;

public class QuickSort: ISorter
{
    private Pages.Sorting _sortingPage;
    private int _iterationDelay;

    private int Count => _sortingPage.SortingArray.Count;
    private List<int> Arr => _sortingPage.SortingArray;

    public QuickSort(Pages.Sorting page, int iterationDelay)
    {
        _sortingPage = page;
        _iterationDelay = iterationDelay;
    }
    public async Task Sort(CancellationToken ct)
    {
        await DoQuickSort(0, Arr.Count - 1, ct);
    }

    private async Task DoQuickSort(int low, int high,CancellationToken ct)
    {
        if (low < high)
        {
            var partitionIdx = await Partition(low, high, ct);

            await DoQuickSort(low, partitionIdx - 1, ct);
            await DoQuickSort(partitionIdx + 1, high, ct);
        }
    }

    private async Task<int> Partition(int low, int high, CancellationToken ct)
    {
        var pivot = Arr[high];
        var i = low - 1;

        for (var j = low; j < high; j++)
        {
            if (Arr[j] >= pivot) continue;

            i++;
            (Arr[i], Arr[j]) = (Arr[j], Arr[i]);
            _sortingPage.RefreshUi();
            await Task.Delay(_iterationDelay);

            if(ct.IsCancellationRequested)
                ct.ThrowIfCancellationRequested();
        }

        (Arr[i+1], Arr[high]) = (Arr[high], Arr[i+1]);

        _sortingPage.RefreshUi();
        await Task.Delay(_iterationDelay);
        if (ct.IsCancellationRequested)
            ct.ThrowIfCancellationRequested();
        return i + 1;
    }

}

