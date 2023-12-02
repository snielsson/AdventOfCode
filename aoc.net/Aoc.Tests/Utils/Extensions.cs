// Copyright © 2023 TradingLens. All Rights Reserved.

using System.Text.Json;

namespace Aoc.Tests.Utils; 

public static class Extensions {
  private static JsonSerializerOptions IndentedJsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
  public static string ToJson(this object? x) => JsonSerializer.Serialize(x, IndentedJsonSerializerOptions);
}