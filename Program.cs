using System;
using System.Collections.Generic;

namespace CalculatorCommandPattern
{
    // Інтерфейс команди
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    // Калькулятор
    public class Calculator
    {
        public int CurrentValue { get; private set; } = 0;

        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': CurrentValue += operand; break;
                case '-': CurrentValue -= operand; break;
                case '*': CurrentValue *= operand; break;
                case '/': CurrentValue /= operand; break;
            }
            Console.WriteLine($"Current value = {CurrentValue} (after {@operator} {operand})");
        }
    }

    // Конкретна команда
    public class CalculatorCommand : ICommand
    {
        private char @operator;
        private int operand;
        private Calculator calculator;

        public CalculatorCommand(Calculator calculator, char @operator, int operand)
        {
            this.calculator = calculator;
            this.@operator = @operator;
            this.operand = operand;
        }

        public void Execute()
        {
            calculator.Operation(@operator, operand);
        }

        public void Undo()
        {
            // Виконуємо протилежну операцію
            char undoOperator = @operator switch
            {
                '+' => '-',
                '-' => '+',
                '*' => '/',
                '/' => '*',
                _ => throw new InvalidOperationException()
            };
            calculator.Operation(undoOperator, operand);
        }
    }

    // Менеджер команд з Undo/Redo
    public class CommandManager
    {
        private Stack<ICommand> undoStack = new Stack<ICommand>();
        private Stack<ICommand> redoStack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            undoStack.Push(command);
            redoStack.Clear();
        }

        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                ICommand command = undoStack.Pop();
                command.Undo();
                redoStack.Push(command);
            }
        }

        public void Redo()
        {
            if (redoStack.Count > 0)
            {
                ICommand command = redoStack.Pop();
                command.Execute();
                undoStack.Push(command);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Calculator calculator = new Calculator();
            CommandManager manager = new CommandManager();

            manager.ExecuteCommand(new CalculatorCommand(calculator, '+', 10));
            manager.ExecuteCommand(new CalculatorCommand(calculator, '-', 5));
            manager.ExecuteCommand(new CalculatorCommand(calculator, '*', 3));

            manager.Undo(); // undo multiply
            manager.Undo(); // undo subtract
            manager.Redo(); // redo subtract
            manager.Redo(); // redo multiply
        }
    }
}
