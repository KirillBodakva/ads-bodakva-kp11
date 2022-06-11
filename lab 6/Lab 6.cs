using System;
using static System.Console;

namespace Lab6
{
    class Program
    {

        public class Node<T>
        {
            public T Value;
            public Node<T> nextNode;
            public Node(T value)
            {
                Value = value;
            }
        }

        public class MyStack<T>
        {
            public Node<T> top = null;
            public int size = 0;
            private MyStack() { }
            public static MyStack<T> InitStack()
            {
                MyStack<T> myStack = new MyStack<T>();
                return myStack;
            }

            public void PrintData()
            {
                if (size > 0)
                {
                    WriteLine("Поточний вмiст стека: ");
                    Node<T> curNode = top;
                    while (!(curNode is null))
                    {
                        WriteLine(curNode.Value);
                        curNode = curNode.nextNode;
                    }
                    WriteLine();
                }
                else
                {
                    WriteLine("Поточний стек пустий.");
                }
            }

            public void Push(T e)
            {
                Node<T> N = new Node<T>(e);
                if (top == null)
                {
                    top = N;
                    top.nextNode = null;
                }
                else
                {
                    N.nextNode = top;
                    top = N;
                    PrintData();
                }
                size++;
            }
            public T Pop()
            {
                if (!Empty())
                {
                    T show = top.Value;
                    top = top.nextNode;
                    size--;
                    PrintData();
                    return show;
                }
                else
                {
                    return default(T);
                }
            }

            public static void DestroyStack(ref MyStack<T> stack)
            {
                while (stack.top.nextNode != null)
                {
                    stack.top = stack.top.nextNode;
                }
                if (!(stack.top is null))
                {
                    stack.top = null;
                }
                stack.size = 0;
                stack = null;
            }

            public T PopHidden()
            {
                if (!Empty())
                {
                    T valueToShow = top.Value;
                    top = top.nextNode;
                    size--;
                    return valueToShow;
                }
                else
                {
                    PrintData();
                    return default(T);
                }
            }

            public T Peek()
            {
                if (!Empty())
                {
                    return top.Value;
                }
                else
                {
                    return default(T);
                }
            }

            public bool Empty()
            {
                if (size != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        static void Main(string[] args)
        {
            bool r = true;
            while (r)
            {
                WriteLine();
                WriteLine("Подивитися приклад: 1 \n" +
                                "Ввести кастомнi данi: 2 \n" +
                                "Вийти: 3");
                WriteLine();
                string c = ReadLine();
                string html = "<html> \n" +
                              "<head> \n" +
                              "<title> Hello </title> \n" +
                              "</head> \n" +
                              "<body> \n" +
                              "<p> This appears in the <i> browser </i>. </p> \n" +
                              "</body> \n" +
                              "</html> \n";
                WriteLine();

                if (c == "1")
                {
                    WriteLine("Готовий приклад: \n" + html);

                    if (Correct(html)) { WriteLine("Алгоритм завершив свою роботу."); }
                    else { WriteLine("Помилка."); }
                }

                else if (c == "2")
                {
                    WriteLine("Ведiть кастомнi данi:");
                    string code = ReadLine();

                    if (Correct(code)) { WriteLine("Алгоритм завершив свою роботу."); }
                    else { WriteLine("Помилка."); }
                }

                else if (c == "3") { r = false; }
            }
        }

        static bool Correct(string code)
        {
            MyStack<string> S = MyStack<string>.InitStack();
            bool open = false;
            string t = "";

            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == '<')
                {
                    open = true;
                }
                else if (code[i] == '>')
                {
                    t += code[i];
                    if (t.Contains('/'))
                    {
                        string i1 = S.Peek();
                        string i2 = t.Replace("/", "");
                        if (i1 == i2)
                        {
                            S.Pop();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    S.Push(t);
                    t = "";
                    open = false;
                }
                if (open)
                {
                    t += code[i];
                }
            }
            if (S.Empty())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}