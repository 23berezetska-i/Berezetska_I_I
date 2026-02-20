using System;
using System.Collections.Generic;
using System.Text;

class Context
{
    public string Roman { get; set; }
    public int Value { get; set; }

    public Context(string roman)
    {
        Roman = roman;
    }
}

// Абстрактний вираз
abstract class Expression
{
    public abstract void Interpret(Context context);
}

// Конкретні вирази для кожного символу
class ThousandExpression : Expression
{
    public override void Interpret(Context context)
    {
        while (context.Roman.StartsWith("M"))
        {
            context.Value += 1000;
            context.Roman = context.Roman.Substring(1);
        }
    }
}

class HundredExpression : Expression
{
    public override void Interpret(Context context)
    {
        if (context.Roman.StartsWith("CM"))
        {
            context.Value += 900;
            context.Roman = context.Roman.Substring(2);
        }
        else if (context.Roman.StartsWith("CD"))
        {
            context.Value += 400;
            context.Roman = context.Roman.Substring(2);
        }
        else if (context.Roman.StartsWith("D"))
        {
            context.Value += 500;
            context.Roman = context.Roman.Substring(1);
        }
        while (context.Roman.StartsWith("C"))
        {
            context.Value += 100;
            context.Roman = context.Roman.Substring(1);
        }
    }
}

class TenExpression : Expression
{
    public override void Interpret(Context context)
    {
        if (context.Roman.StartsWith("XC"))
        {
            context.Value += 90;
            context.Roman = context.Roman.Substring(2);
        }
        else if (context.Roman.StartsWith("XL"))
        {
            context.Value += 40;
            context.Roman = context.Roman.Substring(2);
        }
        else if (context.Roman.StartsWith("L"))
        {
            context.Value += 50;
            context.Roman = context.Roman.Substring(1);
        }
        while (context.Roman.StartsWith("X"))
        {
            context.Value += 10;
            context.Roman = context.Roman.Substring(1);
        }
    }
}

class OneExpression : Expression
{
    public override void Interpret(Context context)
    {
        if (context.Roman.StartsWith("IX"))
        {
            context.Value += 9;
            context.Roman = context.Roman.Substring(2);
        }
        else if (context.Roman.StartsWith("IV"))
        {
            context.Value += 4;
            context.Roman = context.Roman.Substring(2);
        }
        else if (context.Roman.StartsWith("V"))
        {
            context.Value += 5;
            context.Roman = context.Roman.Substring(1);
        }
        while (context.Roman.StartsWith("I"))
        {
            context.Value += 1;
            context.Roman = context.Roman.Substring(1);
        }
    }
}

class Program
{
    static void Main()
    {
        string roman = "MCMXCIV"; // 1994
        Context context = new Context(roman);

        List<Expression> tree = new List<Expression>
        {
            new ThousandExpression(),
            new HundredExpression(),
            new TenExpression(),
            new OneExpression()
        };

        foreach (var exp in tree)
        {
            exp.Interpret(context);
        }

        Console.WriteLine($"{roman} = {context.Value}");
    }
}
