
using System.Security.Cryptography;

namespace AlgorithmVisualizer.Client.Algorithms.Sorting;

public class MergeSort: ISorter
{
    private Pages.Sorting _sortingPage;
    private int _iterationDelay;

    private int Count => _sortingPage.SortingArray.Count;
    private List<int> Arr => _sortingPage.SortingArray;

    public MergeSort(Pages.Sorting page, int iterationDelay)
    {
        _sortingPage = page;
        _iterationDelay = iterationDelay;
    }
    public async Task Sort(CancellationToken ct)
    {
        await DoMergeSort(0, Arr.Count-1, ct);
    }

    private async Task DoMergeSort(int low, int high, CancellationToken ct)
    {
        if (low < high)
        {
            var middle = (high + low) / 2;

            await DoMergeSort(low, middle, ct);
            await DoMergeSort(middle + 1, high, ct);

            if (ct.IsCancellationRequested) ct.ThrowIfCancellationRequested();

            await Merge(low, middle, high, ct);
        }
    }

    private async Task Merge(int start, int middle, int end, CancellationToken ct)
    {

        var left = start;
        var right = middle + 1;
        var tempArr = new int[end - start + 1];
        var index = 0;


        while (left <= middle && right <= end)
        {

            if (Arr[left] < Arr[right])
            {
                tempArr[index] = Arr[left];
                left++;
            }
            else
            {
                tempArr[index] = Arr[right];
                right++;
            }
            index++;
        }

        for (var i = left; i <= middle; i++)
        {
            tempArr[index] = Arr[i];
            index++;
        }

        for (var i = right; i <= end; i++)
        {
            tempArr[index] = Arr[i];
            index++;
        }

        for (var i = 0; i < tempArr.Length; i++)
        {
            Arr[start + i] = tempArr[i];

            _sortingPage.RefreshUi();
            await Task.Delay(_iterationDelay, ct);
        }
    }
}

