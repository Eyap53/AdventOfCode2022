namespace AdventOfCode;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Day05 : BaseDay
{
    struct Instruction
    {
        public int moves;
        public int from;
        public int to;

        public Instruction(int movingCount, int from, int to)
        {
            this.moves = movingCount;
            this.from = from;
            this.to = to;
        }

        public override string ToString()
        {
            return string.Format("move {0} from {1} to {2}", moves, from + 1, to + 1);
        }
    }

    private readonly List<List<char>> _input;
    private readonly List<Instruction> _instructions;

    public Day05()
    {
        _input = new List<List<char>>();
        _instructions = new List<Instruction>();

        string[] lines = File.ReadAllLines(InputFilePath);
        int inputType = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            if (inputType == 0)
            {
                Match m = Regex.Match(lines[i], @" (\d+)  ?");
                if (m.Success)
                {
                    inputType++;
                    i++;
                }
                else
                {
                    for (int c = 0; c < (lines[i].Length - 1) / 4.0; c++)
                    {
                        if (_input.Count <= c)
                        {
                            _input.Add(new List<char>());
                        }
                        if (lines[i][c * 4 + 1] != ' ')
                        {
                            _input[c].Insert(0, lines[i][c * 4 + 1]);
                        }
                    }
                }
            }
            else if (inputType == 1)
            {
                Match m = Regex.Match(lines[i], @"move (\d+) from (\d+) to (\d+)");
                if (!m.Success)
                {
                    throw new Exception("Error when parsing input : " + lines[i]);
                }
                _instructions.Add(new Instruction(int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value) - 1, int.Parse(m.Groups[3].Value) - 1));
            }

        }
    }

    public override ValueTask<string> Solve_1()
    {
        List<List<char>> input = new List<List<char>>();
        for (int i = 0; i < _input.Count; i++)
        {
            input.Add(new List<char>(_input[i]));
        }

        ApplyRearrangement9000(input);

        // Print result
        string r = "";
        input.ForEach(q => r += q.Last());
        return new(r);
    }

    public override ValueTask<string> Solve_2()
    {
        List<List<char>> input = new List<List<char>>();
        for (int i = 0; i < _input.Count; i++)
        {
            input.Add(new List<char>(_input[i]));
        }

        ApplyRearrangement9001(input);

        // Print result
        string r = "";
        input.ForEach(q => r += q.Last());
        return new(r);
    }

    /// <summary>
    /// Stack may be used to gain perf and ease of write
    /// </summary>
    /// <param name="crates"></param>
    private void ApplyRearrangement9000(List<List<char>> crates)
    {
        for (int i = 0; i < _instructions.Count; i++)
        {
            Console.WriteLine(_instructions[i].ToString());
            for (int move = 0; move < _instructions[i].moves; move++)
            {
                crates[_instructions[i].to].Add(crates[_instructions[i].from].TakeLast(1).Single());
                crates[_instructions[i].from].RemoveAt(crates[_instructions[i].from].Count - 1);
            }
        }
    }

    private void ApplyRearrangement9001(List<List<char>> crates)
    {
        for (int i = 0; i < _instructions.Count; i++)
        {
            crates[_instructions[i].to].AddRange(crates[_instructions[i].from].TakeLast(_instructions[i].moves));
            crates[_instructions[i].from] = crates[_instructions[i].from].SkipLast(_instructions[i].moves).ToList();
        }
    }
}
