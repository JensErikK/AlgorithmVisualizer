
namespace AlgorithmVisualizer.Client.Algorithms.Sorting;

public class SelectionSort: ISorter
{
    private Pages.Sorting _sortingPage;
    private int _iterationDelay;

    private int Count => _sortingPage.SortingArray.Count;
    private List<int> Arr => _sortingPage.SortingArray;

    public SelectionSort(Pages.Sorting page, int iterationDelay)
    {
        _sortingPage = page;
        _iterationDelay = iterationDelay;
    }

    public async Task Sort(CancellationToken ct)
    {

        for (var i = 0; i < Count-1; i++)
        {
            var minIdx = i;

            for (var j = i + 1; j < Count; j++)
            {
                if (Arr[j] < Arr[minIdx])
                    minIdx = j;
            }

            (Arr[i], Arr[minIdx]) = (Arr[minIdx], Arr[i]);
            await Task.Delay(_iterationDelay, ct);
            _sortingPage.RefreshUi();

            if (ct.IsCancellationRequested)
                ct.ThrowIfCancellationRequested();
        }

    }
}

