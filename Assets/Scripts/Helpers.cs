using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum Coordinate
{
  TopLeft, TopCenter, TopRight, MiddleLeft, MiddleCenter, MiddleRight, BottomLeft, BottomCenter, BottomRight,
}

public static class Helpers
{
  public static Vector3 GetHealthPointCoordinates(int index)
  {
    var spaceBetween = 4;
    var healthPointWidth = 34;

    return new Vector3(index * (healthPointWidth + spaceBetween), 0);
  }

  public static Vector3 GetButtonCoordinates(Coordinate coordinate)
  {
    Vector3[][] lines = new Vector3[3][];

    for (int i = 0; i < 3; i++)
      lines[i] = GenerateCoordinate(i);

    return coordinate switch
    {
      Coordinate.TopLeft => lines[0][0],
      Coordinate.TopCenter => lines[0][1],
      Coordinate.TopRight => lines[0][2],
      Coordinate.MiddleLeft => lines[1][0],
      Coordinate.MiddleCenter => lines[1][1],
      Coordinate.MiddleRight => lines[1][2],
      Coordinate.BottomLeft => lines[2][0],
      Coordinate.BottomCenter => lines[2][1],
      Coordinate.BottomRight => lines[2][2],
      _ => throw new System.Exception("Something went wrong"),
    };
  }

  public static Tuple<string, int> GenerateValue(int max)
  {
    var random = new System.Random();

    var num1 = random.Next(1, max + 1);
    var num2 = random.Next(1, max + 1);

    var result = num1 * num2;
    var multiplication = $"{num1}x{num2}";

    return new Tuple<string, int>(multiplication, result);
  }

  private static Vector3[] GenerateCoordinate(int line)
  {
    var basePosition = new Vector3(0f, 18f);
    var spaceBetween = 20f;
    var buttonWidth = 154f;
    var buttonHeight = 132f;

    var xModifier = spaceBetween + buttonWidth;
    var yModifier = spaceBetween + buttonHeight;

    var horizontalModifier = new Vector3(xModifier, 0f);
    var verticalModifier = new Vector3(0f, -yModifier);

    return new Vector3[] {
      basePosition + (horizontalModifier * -1f) + (verticalModifier * line),
      basePosition + (horizontalModifier * 0f) + (verticalModifier * line),
      basePosition + (horizontalModifier * 1f) + (verticalModifier * line)
    };
  }
}


public static class IEnumerableExtensions
{
  public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
     => self.Select((item, index) => (item, index));
}