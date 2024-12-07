var input = File.ReadAllLines("input");
var reports = ProcessInput(input);
Console.WriteLine(CalculateSafeReports(reports));

List<Report> ProcessInput(string[] input)
{
    var reports = new List<Report>();
    var occurrences = 0;
    foreach (var line in input)
    {
        var individualLevels = line.Split(' ');
        reports.Add(new Report(individualLevels.Select(x => int.Parse(x)).ToList()));
        occurrences++;
    }
    Console.WriteLine("Occurrences: " + occurrences);

    return reports;
}

int CalculateSafeReports(List<Report> reports)
{
    int safeReports = 0;
    foreach (var report in reports)
    {
        if (CalculateSafeReportWithVariants(report.Levels))
        {
            safeReports++;
        }
    }
    return safeReports;
}

bool CalculateSafeReportWithVariants(List<int> levels)
{
    if (CalculateSafeReport(levels))
    {
        return true;
    }
    return TryWithVariants(levels);
}

bool CalculateSafeReport(List<int> levels)
{
    if (AllIncreasing(levels) || AllDecreasing(levels))
    {
        if (AdjacentDifferAtleastOneOrAtMostThree(levels))
        {
            return true;
        }
    }
    return false;
}

bool TryWithVariants(List<int> levels)
{
    List<int> copy = new List<int>(levels);
    for (int i = 0; i < levels.Count; i++)
    {
        copy.RemoveAt(i);
        if (CalculateSafeReport(copy))
        {
            return true;
        }
        copy = new List<int>(levels);
    }
    return false;
}

bool AllIncreasing(List<int> levels)
{
    for (int i = 0; i < levels.Count - 1; i++)
    {
        if (levels[i] >= levels[i + 1])
        {
            return false;
        }
    }
    return true;
}

bool AllDecreasing(List<int> levels)
{
    for (int i = 0; i < levels.Count - 1; i++)
    {
        if (levels[i] <= levels[i + 1])
        {
            return false;
        }
    }
    return true;
}

bool AdjacentDifferAtleastOneOrAtMostThree(List<int> levels)
{
    for (int i = 0; i < levels.Count - 1; i++)
    {
        if (Math.Abs(levels[i] - levels[i + 1]) > 3)
        {
            return false;
        }
        if (Math.Abs(levels[i] - levels[i + 1]) < 1)
        {
            return false;
        }
    }
    return true;
}

class Report(List<int> levels)
{
    public List<int> Levels { get; set; } = levels;
}