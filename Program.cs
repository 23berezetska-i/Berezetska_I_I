using System;

namespace GUIChainOfResponsibility
{
    // Типи подій
    public enum GuiEventType
    {
        KeyPress,
        MouseClick
    }

    // Клас події
    public class GuiEvent
    {
        public GuiEventType EventType { get; }
        public string Data { get; }

        public GuiEvent(GuiEventType type, string data)
        {
            EventType = type;
            Data = data;
        }
    }

    // Абстрактний обробник
    public abstract class GuiHandler
    {
        protected GuiHandler nextHandler;

        public void SetNext(GuiHandler handler)
        {
            nextHandler = handler;
        }

        public void Handle(GuiEvent guiEvent)
        {
            if (!Process(guiEvent) && nextHandler != null)
            {
                nextHandler.Handle(guiEvent);
            }
        }

        protected abstract bool Process(GuiEvent guiEvent);
    }

    // Конкретні обробники
    public class Button : GuiHandler
    {
        protected override bool Process(GuiEvent guiEvent)
        {
            if (guiEvent.EventType == GuiEventType.MouseClick)
            {
                Console.WriteLine($"Button handled MouseClick: {guiEvent.Data}");
                return true;
            }
            return false;
        }
    }

    public class TextBox : GuiHandler
    {
        protected override bool Process(GuiEvent guiEvent)
        {
            if (guiEvent.EventType == GuiEventType.KeyPress)
            {
                Console.WriteLine($"TextBox handled KeyPress: {guiEvent.Data}");
                return true;
            }
            return false;
        }
    }

    public class Window : GuiHandler
    {
        protected override bool Process(GuiEvent guiEvent)
        {
            Console.WriteLine($"Window received event: {guiEvent.EventType}, Data: {guiEvent.Data}");
            return false; // не обробляє, лише приймає і передає далі
        }
    }

    class Program
    {
        static void Main()
        {
            // Створюємо об’єкти GUI
            Window window = new Window();
            Button button = new Button();
            TextBox textBox = new TextBox();

            // Формуємо ланцюг: Window → Button → TextBox
            window.SetNext(button);
            button.SetNext(textBox);

            // Генеруємо події
            GuiEvent clickEvent = new GuiEvent(GuiEventType.MouseClick, "Left button");
            GuiEvent keyEvent = new GuiEvent(GuiEventType.KeyPress, "Key A");

            // Відправляємо події у ланцюг
            Console.WriteLine("=== MouseClick ===");
            window.Handle(clickEvent);

            Console.WriteLine("\n=== KeyPress ===");
            window.Handle(keyEvent);
        }
    }
}
