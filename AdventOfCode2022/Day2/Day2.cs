using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day2
{
    using System.Xml.Serialization;

    public class Day1Tests
    {
        [InlineData(RockPaperScissors.Paper, RockPaperScissors.Paper, Outcome.Draw, 5)]
        [InlineData(RockPaperScissors.Rock, RockPaperScissors.Scissors, Outcome.Win, 7)]
        [InlineData(RockPaperScissors.Rock, RockPaperScissors.Paper, Outcome.Loss, 1)]
        [InlineData(RockPaperScissors.Paper, RockPaperScissors.Rock, Outcome.Win, 8)]
        [InlineData(RockPaperScissors.Paper, RockPaperScissors.Scissors, Outcome.Loss, 2)]
        [InlineData(RockPaperScissors.Scissors, RockPaperScissors.Paper, Outcome.Win, 9)]
        [InlineData(RockPaperScissors.Scissors, RockPaperScissors.Rock, Outcome.Loss, 3)]
        [Theory]
        public void VerifyScore(RockPaperScissors yours, RockPaperScissors opponents, Outcome expectedOutcome, int expectedScore)
        {
            var outcome = Day2.GetOutcome(yours, opponents);
            var score = Day2.GetScore(yours, opponents);
            Assert.Equal(expectedOutcome, outcome);
            Assert.Equal(expectedScore, score);
        }

        [InlineData("A Y", 8)]
        [InlineData("B X", 1)]
        [InlineData("C Z", 6)]
        [Theory]
        public void VerifyScoreFromInput(string input, int expected)
        {
            var score = Day2.ParseDay1Input(input).Score();
            Assert.Equal(expected, score);
        }

        [Fact]
        public void GetTotalScoreDay1()
        {
            var totalScore = Day2.GetPart1TotalScoreForFile(); //12794
        }

        [Fact]
        public void GetTotalScoreDay2()
        {
            var totalScore = Day2.GetPart2TotalScoreForFile(); //14979
        }
    }
    public class Day2
    {
        private static Dictionary<string, RockPaperScissors> _choiceMapping =
            new(StringComparer.OrdinalIgnoreCase);
        private static Dictionary<string, Outcome> _outcomeMapping = new(StringComparer.OrdinalIgnoreCase);

        static Day2()
        {
            _choiceMapping.Add("A", RockPaperScissors.Rock);
            _choiceMapping.Add("B", RockPaperScissors.Paper);
            _choiceMapping.Add("C", RockPaperScissors.Scissors);
            _choiceMapping.Add("X", RockPaperScissors.Rock);
            _choiceMapping.Add("Y", RockPaperScissors.Paper);
            _choiceMapping.Add("Z", RockPaperScissors.Scissors);
            _outcomeMapping.Add("X", Outcome.Loss);
            _outcomeMapping.Add("Y", Outcome.Draw);
            _outcomeMapping.Add("z", Outcome.Win);
        }

        public static int GetPart1TotalScoreForFile()
        {
            var fileContent = File.ReadAllLines("Day2\\Day_2_input.txt");
            var matches = fileContent.Select(ParseDay1Input);
            var totalScore = matches.Select(m => m.Score()).Sum();
            return totalScore;
        }

        public static int GetPart2TotalScoreForFile()
        {
            var fileContent = File.ReadAllLines("Day2\\Day_2_input.txt");
            var matches = fileContent.Select(ParseDay2Input);
            var totalScore = matches.Select(m => m.Score()).Sum();
            return totalScore;
        }

        public static Match ParseDay1Input(string input)
        {
            var choices = input.Split(' ');
            return new Match
            {
                Theirs = _choiceMapping[choices[0]],
                Yours = _choiceMapping[choices[1]]
            };
        }

        public static Match ParseDay2Input(string input)
        {
            var values = input.Split(' ');
            RockPaperScissors theirs = _choiceMapping[values[0]];
            var outcome = _outcomeMapping[values[1]];
            var yours = GetChoiceToAchieveOutcome(theirs, outcome);
            return new Match
            {
                Theirs = theirs,
                Yours = yours
            };
        }

        public static int GetScore(RockPaperScissors yours, RockPaperScissors opponent)
        {
            var outcomeScore = (int)GetOutcome(yours, opponent);
            return outcomeScore + (int)yours;
        }

        public static Outcome GetOutcome(RockPaperScissors yours, RockPaperScissors opponent)
        {
            if (yours == opponent) return Outcome.Draw;

            if (yours == RockPaperScissors.Rock)
            {
                return opponent == RockPaperScissors.Paper ? Outcome.Loss : Outcome.Win;
            }

            if (yours == RockPaperScissors.Paper)
            {
                return opponent == RockPaperScissors.Scissors ? Outcome.Loss : Outcome.Win;
            }

            return opponent == RockPaperScissors.Rock ? Outcome.Loss : Outcome.Win;
        }

        public static RockPaperScissors GetChoiceToAchieveOutcome(RockPaperScissors theirs, Outcome outcome)
        {
            if (outcome == Outcome.Draw) return theirs;
            if (theirs == RockPaperScissors.Rock)
            {
                return outcome == Outcome.Win ? RockPaperScissors.Paper : RockPaperScissors.Scissors;
            }
            if (theirs == RockPaperScissors.Paper)
            {
                return outcome == Outcome.Win ? RockPaperScissors.Scissors : RockPaperScissors.Rock;
            }
            return outcome == Outcome.Win ? RockPaperScissors.Rock : RockPaperScissors.Paper;
        }
    }

    public enum RockPaperScissors
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public enum Outcome
    {
        Loss = 0,
        Draw = 3,
        Win = 6
    }

    public class Match
    {
        public RockPaperScissors Yours { get; set; }
        public RockPaperScissors Theirs { get; set; }
        public int Score() => Day2.GetScore(Yours, Theirs);

    }

}
