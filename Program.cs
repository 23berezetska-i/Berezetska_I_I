using System;
using System.Text;

class PhoneRecord
{
    public string Name { get; set; }
    public string Number { get; set; }

    public PhoneRecord(string name, string number)
    {
        Name = name;
        Number = number;
    }

    public override string ToString()
    {
        return $"{Name}: {Number}";
    }
}

class Node
{
    public PhoneRecord Data { get; set; }
    public Node Next { get; set; }
    public Node Prev { get; set; }

    public Node(PhoneRecord data)
    {
        Data = data;
    }
}

class DoublyLinkedList
{
    public Node Head { get; private set; }
    public Node Tail { get; private set; }

    public void Add(PhoneRecord record)
    {
        Node newNode = new Node(record);
        if (Head == null)
        {
            Head = Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            newNode.Prev = Tail;
            Tail = newNode;
        }
    }
}

// Ітератор для двозв’язного списку
class ListIterator
{
    private Node current;

    public ListIterator(Node head)
    {
        current = head;
    }

    public bool HasNext()
    {
        return current != null;
    }

    public PhoneRecord Next()
    {
        PhoneRecord data = current.Data;
        current = current.Next;
        return data;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        DoublyLinkedList phoneBook = new DoublyLinkedList();
        phoneBook.Add(new PhoneRecord("Іван", "123-456"));
        phoneBook.Add(new PhoneRecord("Марія", "987-654"));
        phoneBook.Add(new PhoneRecord("Олег", "555-111"));
        phoneBook.Add(new PhoneRecord("Наталя", "222-333"));

        ListIterator iterator = new ListIterator(phoneBook.Head);
        Console.WriteLine("Обхід телефонної книги:");
        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
    }
}
