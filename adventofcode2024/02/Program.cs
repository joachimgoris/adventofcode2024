var input = File.ReadAllLines("input");
var reports = ProcessInput(input);
Console.WriteLine(CalculateSafeReports(reports));

List<Report> ProcessInput(string[] input)
{
    var reports = new List<Report>();
    foreach (var line in input)
    {
        var individualLevels = line.Split(' ');
        reports.Add(new Report(individualLevels.Select(x => int.Parse(x)).ToList()));
    }

    return reports;
}

int CalculateSafeReports(List<Report> reports)
{
    int safeReports = 0;
    foreach (var report in reports)
    {
        var (isAllIncreasing, isDeleted) = AllIncreasing(report.Levels);
        if (AllIncreasing(report.Levels).Item1 || AllDecreasing(report.Levels).Item1)
        {
            if (AdjacentDifferAtleastOneOrAtMostThree(report.Levels))
            {
                safeReports++;
            }
        }
    }
    return safeReports;
}

(bool, bool) AllIncreasing(List<int> levels)
{
    for (int i = 0; i < levels.Count - 1; i++)
    {
        if (levels[i] >= levels[i + 1])
        {
            if (i + 2 < levels.Count && levels[i] >= levels[i + 2])
            {
                return (false, false);
            }
            levels.RemoveAt(i + 1);
            return (true, true);
        }
    }
    return (true, false);
}

(bool, bool) AllDecreasing(List<int> levels)
{
    for (int i = 0; i < levels.Count - 1; i++)
    {
        if (levels[i] <= levels[i + 1])
        {
            if (i + 2 < levels.Count && levels[i] <= levels[i + 2])
            {
                return (false, false);
            }
            levels.RemoveAt(i + 1);
            return (true, true);
        }
    }
    return (true, false);
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

class Report
{
    public Report(List<int> levels)
    {
        Levels = levels;
    }
    public List<int> Levels { get; set; }
}