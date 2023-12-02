// Copyright © 2023 TradingLens. All Rights Reserved.
using Xunit;
using Xunit.Abstractions;

namespace Aoc.Tests._2023._01; 

public class Code {
  public ITestOutputHelper Output { get; }
  public Code(ITestOutputHelper output) {
    Output = output;
  }
  private int? GetDigit(int i, string str, bool allowWordDigits) {
    if (char.IsDigit(str[i])) return str[i] - '0';
    if (allowWordDigits) {
      if (i < 2) return null;
      switch (str.Substring(i-2,3)) {
          case "one": return 1;  
          case "two": return 2;  
          case "six": return 6;  
      }
      if (i >= 3) { 
        switch (str.Substring(i-3,4)) {
          case "four": return 4;  
          case "five": return 5;  
          case "nine": return 9;  
        }
      }
      if (i >= 4) { 
        switch (str.Substring(i-4,5)) {
          case "three": return 3;  
          case "seven": return 7;  
          case "eight": return 8;  
        }
      }
    }
    return null;
  }

  public int ParseLine(string str, bool allowWordDigits)
  {
    var val = 0;
    for (int i = 0; i < str.Length; i++) {
      var digit = GetDigit(i, str, allowWordDigits);
      if (digit != null) {
        val += 10 * digit.Value;
        break;
      }
    }
    for (int i = str.Length - 1; i >= 0; i--)
    {
      var digit = GetDigit(i, str, allowWordDigits);
      if (digit != null) {
        val += digit.Value;
        break;
      }
    }
    return val;
  }  
  [Fact]
  public void Run() {
    Output.WriteLine(File.ReadLines("2023/01/example2.txt").Sum(x => ParseLine(x,true)).ToString());
    Output.WriteLine(File.ReadLines("2023/01/input.txt").Sum(x => ParseLine(x,false)).ToString());
    Output.WriteLine(File.ReadLines("2023/01/input.txt").Sum(x => ParseLine(x,true)).ToString());
  }
}