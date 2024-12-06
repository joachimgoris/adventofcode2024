var lines = File.ReadAllLines("input");
var leftList = new List<double>();
var rightList = new List<double>();
var distances = new List<double>();

foreach (var line in lines)
{
    leftList.Add(int.Parse(line[..5]));
    rightList.Add(int.Parse(line[8..]));
}

CalculateSimilarity();

for (int i = 0; i < 1000; i++)
{
    var leftMinimum = leftList.Min();
    var rightMinimum = rightList.Min();
    if (rightMinimum > leftMinimum)
    {
        distances.Add(rightMinimum - leftMinimum);
    }
    else
    {
        distances.Add(leftMinimum - rightMinimum);
    }
    rightList.Remove(rightMinimum);
    leftList.Remove(leftMinimum);
}

var totalDistance = distances.Sum();

Console.WriteLine(totalDistance);

double CalculateSimilarity()
{
    
}