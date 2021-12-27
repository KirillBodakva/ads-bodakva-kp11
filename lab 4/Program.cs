using System;

namespace ConsoleApp2
{
    internal class Program
    {
        public class Node
        {
            public int data;
            public Node prev;
            public Node next;
            public Node(int data, Node prev, Node next)
            {
                this.data = data;
                this.prev = prev;
                this.next = next;
            }
        }

        public class DoubleLinkedList
        {
            public Node head;
            public Node tail;

            public void AddFirst(int data)
            {
                if (head == null)
                {
                    head = new Node(data, null, null);
                    tail = head;
                }
                else if (head.next == null)
                {
                    tail = head;
                    head = new Node(data, null, tail);
                    tail.prev = head;
                }
                else
                {
                    Node buffer = head;
                    head = new Node(data, null, buffer);
                    buffer.prev = head;
                }
            }

            public void AddLast(int data)
            {
                if (tail == null)
                {
                    head = new Node(data, null, null);
                    tail = head;
                }
                else if (head.next == null)
                {
                    tail = head;
                    head = new Node(data, null, tail);
                    tail.prev = head;
                }
                else
                {
                    Node buffer = tail;
                    tail = new Node(data, buffer, null);
                    buffer.next = tail;
                }
            }

            public void AddAtPosition(int data, int pos)
            {
                pos++;
                if (head == null || tail == null)
                {
                    head = new Node(data, null, null);
                    tail = head;
                }
                else
                {
                    if (pos <= 1)
                    {
                        AddFirst(data);
                    }
                    else
                    {
                        Node current = head;
                        for (int i = 0; i < pos; i++)
                        {
                            if(current.next == null)
                            {
                                AddFirst(data); return;
                            }
                            else { current = current.next; }
                        }

                        Node buffer = current.prev; Node added = new Node(data, buffer, current);
                        buffer.next = added; current.prev = added;
                        if (added.prev == null)
                        {
                            head = added;
                        }
                        else if (added.next == null)
                        {
                            tail = added;
                        }
                    }
                }
            }

            public void DeleteFirst()
            {
                if (head == null) { Console.WriteLine("Ошибка"); }
                else if (head.next == null)
                {
                    head = null; tail = null;
                }
                else
                {
                    head = head.next; head.prev = null;
                }
                
            }

            public void DeleteLast()
            {
                if (head == null) { Console.WriteLine("Опять ошибка"); }
                else if (tail.prev == null)
                {
                    head = null; tail = null;
                }
                else
                {
                    tail = tail.prev; tail.next = null;
                }
            }

            public void DeleteAtPosition(int pos)
            {
                if (head == null || tail == null)
                {
                    Console.WriteLine("Пустой список");
                }
                else
                {
                    Node current = head;
                    if(pos<= 0)
                    {
                        DeleteFirst();
                    }
                    else
                    {
                        for (int i = 0; i < pos; i++)
                        {
                            if (current.next == null)
                            {
                                DeleteFirst(); return;
                            }
                            else { current = current.next; }
                        }

                        Node buffer2 = current.prev; Node tempNext = current.next;
                        buffer2.next = tempNext; tempNext.prev = buffer2;
                    }
                }
            }

            //task
            public void Var1(int data)
            {
                if(data < 0)
                {
                    AddLast(data);
                }
                else
                {
                    Node buffer = tail.prev;
                    buffer.next = new Node(data, buffer, tail);
                    tail.prev = buffer.next;
                }
            }

            public void Print()
            {
                Node current = head;
                if (head == null)
                {
                    Console.WriteLine("Empty list");
                }
                else
                {
                    while (current != null)
                    {
                        Console.Write(current.data + "  ");
                        current = current.next;
                    }
                    Console.WriteLine();
                }
                
            }
        }

        static void Main(string[] args)
        {
            DoubleLinkedList Bruh = new DoubleLinkedList();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("add 1 ---> добавить первый элемент");
            Console.WriteLine("add inf ---> добавить первый элемент");
            Console.WriteLine("add bruh ---> добавить элемент");
            Console.WriteLine("del 1  ---> удалить первый элемент");
            Console.WriteLine("del inf  ---> удалить последний элемент");
            Console.WriteLine("del bruh ---> удалить элемент");
            Console.WriteLine("var1 ---> вариант 1");
            Console.WriteLine("shutdown ---> да");
            Console.WriteLine("clear ---> очистка");
            Console.WriteLine("-----------------------------------------");


            while (true)
            {
                try
                {
                    string s = Console.ReadLine();
                    switch (s)
                    {
                        case "add 1":
                            Console.Write("Введите элемент: ");
                            Bruh.AddFirst(int.Parse(Console.ReadLine()));
                            Bruh.Print();
                            break;
                        case "add inf":
                            Console.Write("Введите элемент: ");
                            Bruh.AddLast(int.Parse(Console.ReadLine()));
                            Bruh.Print();
                            break;
                        case "add bruh":
                            Console.Write("Введите элемент: ");
                            int elem = int.Parse(Console.ReadLine());
                            Console.Write("Введите позицию: ");
                            int posit = int.Parse(Console.ReadLine());
                            Bruh.AddAtPosition(elem, posit);
                            Bruh.Print();
                            break;
                        case "del 1":
                            Bruh.DeleteFirst();
                            Bruh.Print();
                            break;
                        case "del last":
                            Bruh.DeleteLast();
                            Bruh.Print();
                            break;
                        case "del bruh":
                            Console.Write("Введите позицию: ");
                            int position = int.Parse(Console.ReadLine());
                            Bruh.DeleteAtPosition(position);
                            Bruh.Print();
                            break;
                        case "var1":
                            Console.Write("Введите элемент: ");
                            Bruh.Var1(int.Parse(Console.ReadLine()));
                            Bruh.Print();
                            break;
                        case "shutdown":
                            System.Environment.Exit(0);
                            break;
                        case "clear":
                            Console.Clear(); break;
                        default:
                            Console.WriteLine("Пиши нормально"); break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("error");
                }
            }
        }
    }
}
