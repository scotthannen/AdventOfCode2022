namespace AdventOfCode2022.Day1
{
    public class Day1
    {
        [Fact]
        public void Part1()
        {
            var result = GetLists();
            var caloriesByList = result.Select(list => list.Sum());
            var mostCalories = caloriesByList.Max();
        }

        [Fact]
        public void Part2()
        {
            var result = GetLists();
            var caloriesByList = result.Select(list => list.Sum());
            var caloriesByListSorted = caloriesByList.OrderByDescending(c => c);
            var top3 = caloriesByListSorted.Take(3);
            var top3Total = top3.Sum();
        }

        private List<List<int>> GetLists()
        {
            var result = new List<List<int>>();
            result.Add(new List<int>());
            var lines = File.ReadAllLines("day_1_input.txt");
            foreach (var line in lines)
            {
                if (int.TryParse(line, out int calories))
                {
                    result.Last().Add(calories);
                }
                else
                {
                    result.Add(new List<int>());
                }
            }

            return result;
        }
    }
}