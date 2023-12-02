// Copyright © 2023 TradingLens. All Rights Reserved.
using Xunit;
using Xunit.Abstractions;

namespace Aoc.Tests._2023._02;

public class Code {
  private readonly ITestOutputHelper _output;
  public Code(ITestOutputHelper output) {
    _output = output;

  }
  private void W(object s) => _output.WriteLine(s.ToString());

  public record Game(int Id, Dictionary<string, int> MaxObservations) {
    public override string ToString() => $"{Id}: Red:{MaxObservations["red"]} Green:{MaxObservations["green"]} Blue:{MaxObservations["blue"]}";
  };

  //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
  public IEnumerable<Game> ParseLine(string line) {
    int i = 0;
    Dictionary<string, int> maxValues = new Dictionary<string, int>();
    while (i < line.Length && !char.IsDigit(line[i])) i++;
    if (i == 0 || i >= line.Length) yield break;
    
    while (char.IsWhiteSpace(line[i])) i++;
    
    var factor = 1;
    var id = 0;
    while (char.IsDigit(line[i])) {
      var digit = line[i] - '0';
      id = id * factor + digit;
      factor *= 10;
      i++;
    }
    while (i < line.Length) {
      while (!char.IsDigit(line[i])) i++;
      factor = 1;
      var value = 0;
      while (char.IsDigit(line[i])) {
        var digit = line[i] - '0';
        value = value * factor + digit;
        factor *= 10;
        i++;
      }
      while (char.IsWhiteSpace(line[i])) i++;
      var start = i;
      while (i < line.Length && char.IsLetter(line[i])) i++;
      var color = line[start..i];
      maxValues.TryGetValue(color, out int current);
      if (value > current) maxValues[color] = value;
      i++;
    }
    yield return new Game(id, maxValues);
  }

  private Game[] ParseFile(string filepath) 
  {
    W("===== " + filepath + " ====");
    var lines = File.ReadLines(filepath);
    var games = lines.SelectMany(x => ParseLine(x));
    return games.ToArray();
  }

  private int Evaluate(IEnumerable<Game> games) { 
    var result = 0;
    foreach (Game game in games) {
      if (game.MaxObservations.TryGetValue("red", out int red) && red > 12) continue;
      if (game.MaxObservations.TryGetValue("green", out int green) && green > 13) continue;
      if (game.MaxObservations.TryGetValue("blue", out int blue) && blue > 14) continue;
      result += game.Id;
    }
    return result;
  }

  [Fact]
  public void Run() {
    W(Evaluate(ParseFile("2023/02/example1.txt")));
    W(Evaluate(ParseFile("2023/02/input.txt")));
    W(SumOfPowers(ParseFile("2023/02/example1.txt")));
    W(SumOfPowers(ParseFile("2023/02/input.txt")));
  }
  private int SumOfPowers(Game[] games) => games.Sum(game => game.MaxObservations.Values.Aggregate(1, (int x, int y) => x * y));
}