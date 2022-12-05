namespace AdventOfCode;

using System;
using System.Collections.Generic;

public class Day01 : BaseDay
{
    private readonly int[] _input;

    public Day01()
    {
        string[] lines = File.ReadAllLines(InputFilePath);
        List<int> input = new List<int>();
        int elfIndex = 0;
        input.Add(0);
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] == string.Empty)
            {
                elfIndex++;
                input.Add(0);
            }
            else
            {
                input[elfIndex] += int.Parse(lines[i]);
            }
        }
        _input = input.ToArray();
        Console.Write(_input.ToString());
    }

    public override ValueTask<string> Solve_1() => new(_input.Max().ToString());

    public override ValueTask<string> Solve_2()
    {
        // Shortest but NOT most efficient solution
        List<int> sortedInput = _input.ToList();
        sortedInput.OrderDescending();
        return new(sortedInput.Take(3).Sum().ToString());
    }
}
