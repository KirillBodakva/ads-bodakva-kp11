using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lab_7
{
    public class Hashtable
    {
        Entry[] table;
        public double loadness;
        public int size;
        public Hashtable(int n)
        {
            initHashtable(n);
        }
        public void initHashtable(int n)
        {
            size = n;
            table = new Entry[size];
            loadness = 0;
        }
        Random rnd = new Random();
        public List<int> keys = new List<int>();
        public Value insertEntry(Key k, Value val, string title, List<string> author, int yearOfPublishing)
        {
            (k, val) = insertConverting(k, val, title, author, yearOfPublishing);

            if (table[k.getHash(size)] == null)
                table[k.getHash(size)] = new Entry(k, val);
            else
                table[k.getHash(size)].insertNode(k, val);

            return val;
        }
        public void insertAddEntry(Value val, List<string> author)
        {
            foreach (var el in author)
            {
                bool b = true;
                Key ke = new Key(el);
                if (table[ke.getHash(size)] == null)
                    table[ke.getHash(size)] = new Entry(ke, new Value("", new Value.LinkedList(val.title), 0));
                else
                {
                    Entry.Node current = table[ke.getHash(size)].head;
                    for (int i = 0; i < table[ke.getHash(size)].size; i++)
                    {
                        if (current.key.bookID == ke.bookID)
                        {
                            Value.LinkedList.Author cur = current.value.authors.head;
                            for (int j = 0; j < current.value.authors.size; j++)
                            {
                                if (cur.data == val.title)
                                {
                                    b = false;
                                    break;
                                }
                                cur = cur.prev;
                            }
                            if (b)
                            {
                                current.value.authors.insertNode(val.title);
                                b = false;
                            }
                            break;
                        }
                        current = current.prev;
                    }
                    if (b)
                        table[ke.getHash(size)].insertNode(ke, new Value("", new Value.LinkedList(val.title), 0));
                }
            }
        }
        public (Key, Value) insertConverting(Key k, Value val, string title, List<string> author, int yearOfPublishing)
        {
            if (k == null && val == null)
            {
                int bookID = rnd.Next(100000, 1000000);
                if (keys.Contains(bookID))
                {
                    insertConverting(k, val, title, author, yearOfPublishing);
                    return (k, val);
                }
                else
                    keys.Add(bookID);

                int count = 1;
                Value.LinkedList authors = new Value.LinkedList(null);
                foreach (var item in author)
                {
                    if (count == 1)
                        authors = new Value.LinkedList(item);
                    else
                        authors.insertNode(item);
                    count++;
                }

                k = new Key(bookID.ToString());
                val = new Value(title, authors, yearOfPublishing);
            }
            else
            {
                try
                {
                    keys.Add(int.Parse(k.bookID));
                }
                catch { }
            }
            return (k, val);
        }
        public void removeEntry(int code)
        {
            Key ke = new Key(code.ToString());
            if (table[ke.getHash(size)] == null)
            {
                Console.WriteLine("Книги з даним ID не існує");
                return;
            }

            Entry.Node current = table[ke.getHash(size)].head;
            if (current.key.bookID == ke.bookID)
            {
                table[ke.getHash(size)].head = current.prev;
                table[ke.getHash(size)] = null;
                return;
            }
            for (int i = 1; i < table[ke.getHash(size)].size; i++)
            {
                if (current.prev.key.bookID == ke.bookID)
                {
                    current.prev = current.prev.prev;
                    keys.Remove(int.Parse(current.key.bookID));
                    table[ke.getHash(size)].size--;
                    Console.WriteLine("Відповідна книга була видалена");
                    return;
                }
                current = current.prev;
            }
            Console.WriteLine("Книги з даною назвою не існує");
        }
        public void findEntry(int code)
        {
            Key ke = new Key(code.ToString());
            if (table[ke.getHash(size)] == null)
            {
                Console.WriteLine("Книги з даним ID не існує");
                return;
            }

            Entry.Node current = table[ke.getHash(size)].head;
            for (int i = 0; i < table[ke.getHash(size)].size; i++)
            {
                if (current.key.bookID == ke.bookID)
                {
                    Console.Write($" Знайдена книга:\n      Інформація | {current.key.bookID,15} | {current.value.title,15} | {current.value.yearOfPublishing,15} ");
                    for (int j = 3; j < current.value.authors.size; j++)
                        Console.Write($"|                 ");

                    Console.Write($"\n          Автори ");
                    Value.LinkedList.Author cur = current.value.authors.head;

                    for (int j = 0; j < current.value.authors.size; j++)
                    {
                        if (j == 1 && current.value.title.Length > 15)
                        {
                            for (int k = cur.data.Length; k < current.value.title.Length; k++)
                                cur.data = " " + cur.data;
                            Console.Write($"| {cur.data} ");
                        }
                        else
                            Console.Write($"| {cur.data,15} ");
                        cur = cur.prev;
                    }
                    for (int j = current.value.authors.size; j < 3; j++)
                        Console.Write($"|                 ");
                    Console.WriteLine();
                    return;
                }
                current = current.prev;
            }
            Console.WriteLine("Книги з даною назвою не існує");
        }
        public void findAllBooks(string authorName)
        {
            Key ke = new Key(authorName.ToString());
            if (table[ke.getHash(size)] == null)
            {
                Console.WriteLine("Автора з даним ID не існує");
                return;
            }

            Entry.Node current = table[ke.getHash(size)].head;
            for (int i = 0; i < table[ke.getHash(size)].size; i++)
            {
                if (current.key.bookID == ke.bookID)
                {
                    Console.Write($" Знайдений Автор:\n            Ім'я | {current.key.bookID,15} ");
                    for (int j = 1; j < current.value.authors.size; j++)
                        Console.Write($"|                 ");

                    Console.Write($"\n           Книги ");
                    Value.LinkedList.Author cur = current.value.authors.head;

                    for (int j = 0; j < current.value.authors.size; j++)
                    {
                        if (cur.data.Length > 15)
                            Console.Write($"| {cur.data.Substring(0, 13)}.. ");
                        else
                            Console.Write($"| {cur.data,15} ");
                        cur = cur.prev;
                    }
                    Console.WriteLine();
                    return;
                }
                current = current.prev;
            }
            Console.WriteLine("Автора з даним прізвищем не існує");
        }
        public bool unique(string title)
        {
            for (int i = 0; i < size; i++)
            {
                if (table[i] != null)
                {
                    Entry.Node current = table[i].head;
                    for (int j = 0; j < table[i].size; j++)
                        if (title == current.value.title)
                            return true;
                }

            }
            return false;
        }
        public Hashtable rehashing()
        {
            Hashtable temporary = new Hashtable(size * 2);
            for (int i = 0; i < size; i++)
            {
                Entry.Node current = table[i].head;
                try
                {
                    int.Parse(current.key.bookID);
                    keys.Clear();
                }
                catch { }
                for (int j = 0; j < table[i].size; j++)
                {
                    temporary.insertEntry(current.key, current.value, null, null, 0);
                    current = current.prev;
                }
            }

            Console.WriteLine("Відбулося перегешування таблиці");
            size *= 2;
            return temporary;
        }
        public (Hashtable, bool) loadnessCheck(Hashtable test)
        {
            double count = 0;
            for (int i = 0; i < table.Length; i++)
                if (table[i] != null)
                    count++;

            loadness = count / size;
            if (loadness >= 1)
            {
                test = rehashing();
                loadness /= 2;
                return (test, true);
            }

            return (test, false);
        }
        public void peek()
        {
            Console.WriteLine();
            int maxCol = 0;
            for (int i = 0; i < table.Length; i++)
                if (table[i] != null)
                    maxCol = Math.Max(table[i].size, maxCol);


            Console.Write("                 ");
            for (int i = 0; i < maxCol; i++)
                Console.Write($"|         Книга {i + 1} ");
            Console.WriteLine();

            for (int i = 0; i < table.Length; i++)
            {
                Console.Write($"--------{i}--------");
                for (int j = 1; j < maxCol + 1; j++)
                    Console.Write($"|-----------------");

                Console.WriteLine();
                if (table[i] == null)
                {
                    Console.Write($"            null ");
                    for (int j = 1; j < maxCol + 1; j++)
                        Console.Write($"|                 ");
                    Console.WriteLine();
                }
                else
                {
                    int maxRow = 0;
                    Entry.Node current = table[i].head;
                    for (int j = 0; j < table[i].size; j++)
                    {
                        maxRow = Math.Max(current.value.authors.size, maxRow);
                        current = current.prev;
                    }

                    string[,] outputEl = new string[maxRow + 3, maxCol + 1];
                    outputEl[0, 0] = "        ID книги ";
                    outputEl[1, 0] = "           Назва ";
                    outputEl[2, 0] = "     Рік видання ";
                    for (int j = 0; j < maxRow; j++)
                        outputEl[j + 3, 0] = $"         Автор {j + 1} ";

                    current = table[i].head;
                    for (int j = 0; j < table[i].size; j++)
                    {
                        outputEl[0, j + 1] = $"| {current.key.bookID,15} ";
                        if (current.value.title.Length > 15)
                            outputEl[1, j + 1] = $"| {current.value.title.Substring(0, 13)}.. ";
                        else
                            outputEl[1, j + 1] = $"| {current.value.title,15} ";
                        outputEl[2, j + 1] = $"| {current.value.yearOfPublishing,15} ";

                        Value.LinkedList.Author cur = current.value.authors.head;
                        for (int k = 0; k < current.value.authors.size; k++)
                        {
                            outputEl[k + 3, j + 1] = $"| {cur.data,15} ";
                            cur = cur.prev;
                        }
                        current = current.prev;
                    }

                    for (int k = 0; k < maxRow + 3; k++)
                    {
                        for (int j = 0; j < maxCol + 1; j++)
                        {
                            if (outputEl[k, j] != null)
                                Console.Write(outputEl[k, j]);
                            else
                                Console.Write("|                 ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        public void peekAuth()
        {
            Console.WriteLine();
            int maxCol = 0;
            for (int i = 0; i < table.Length; i++)
                if (table[i] != null)
                    maxCol = Math.Max(table[i].size, maxCol);


            Console.Write("                 ");
            for (int i = 0; i < maxCol; i++)
                Console.Write($"|         Автор {i + 1} ");
            Console.WriteLine();

            for (int i = 0; i < table.Length; i++)
            {
                Console.Write($"--------{i}--------");
                for (int j = 1; j < maxCol + 1; j++)
                    Console.Write($"|-----------------");

                Console.WriteLine();
                if (table[i] == null)
                {
                    Console.Write($"            null ");
                    for (int j = 1; j < maxCol + 1; j++)
                        Console.Write($"|                 ");
                    Console.WriteLine();
                }
                else
                {
                    int maxRow = 0;
                    Entry.Node current = table[i].head;
                    for (int j = 0; j < table[i].size; j++)
                    {
                        maxRow = Math.Max(current.value.authors.size, maxRow);
                        current = current.prev;
                    }

                    string[,] outputEl = new string[maxRow + 1, maxCol + 1];
                    outputEl[0, 0] = "        Прізвище ";
                    for (int j = 0; j < maxRow; j++)
                        outputEl[j + 1, 0] = $"         Книга {j + 1} ";

                    current = table[i].head;
                    for (int j = 0; j < table[i].size; j++)
                    {
                        outputEl[0, j + 1] = $"| {current.key.bookID,15} ";

                        Value.LinkedList.Author cur = current.value.authors.head;
                        for (int k = 0; k < current.value.authors.size; k++)
                        {
                            if (cur.data.Length > 15)
                                outputEl[k + 1, j + 1] = $"| {cur.data.Substring(0, 13)}.. ";
                            else
                                outputEl[k + 1, j + 1] = $"| {cur.data,15} ";
                            cur = cur.prev;
                        }
                        current = current.prev;
                    }

                    for (int k = 0; k < maxRow + 1; k++)
                    {
                        for (int j = 0; j < maxCol + 1; j++)
                        {
                            if (outputEl[k, j] != null)
                                Console.Write(outputEl[k, j]);
                            else
                                Console.Write("|                 ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
    public class Key
    {
        string key;
        public string bookID;
        public Key(string k)
        {
            bookID = k;
            key = bookID;
        }
        private BigInteger hashCode()
        {
            char[] alphabet = new char[33] { 'а', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и', 'і', 'ї', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я' };
            BigInteger code = 0;

            for (int i = 0; i < key.Length; i++)
            {
                long j;
                for (j = 0; j < alphabet.Length; j++)
                    if (key[i].ToString().ToLower() == alphabet[j].ToString())
                        code += (BigInteger)((j + 1) * Math.Pow(33, key.Length - i - 1));
            }
            return code;
        }
        public int getHash(int mod)
        {
            int index;
            for (int i = 0; i < key.Length; i++)
            {
                if (!Char.IsNumber(key[i]))
                {
                    index = (int)(hashCode() % mod);
                    return index;
                }
            }
            index = (int)(double.Parse(key) % mod);
            return index;
        }
    }
    public class Value
    {
        (string, LinkedList, int) value;
        public LinkedList authors;
        public string title;
        public int yearOfPublishing;
        public Value(string t, LinkedList a, int y)
        {
            title = t;
            authors = a;
            yearOfPublishing = y;
            value = (title, authors, yearOfPublishing);
        }
        public class LinkedList
        {
            public Author head;
            public int size;
            public class Author
            {
                public string data;
                public Author prev;
                public Author(string d, Author p)
                {
                    data = d;
                    prev = p;
                }
            }
            public LinkedList(string author)
            {
                head = null;
                size = 0;
                insertNode(author);
            }
            public void insertNode(string author)
            {
                head = new Author(author, head);
                size++;
            }
        }
    }
    public class Entry
    {
        public Entry(Key key, Value value)
        {
            head = null;
            size = 0;
            insertNode(key, value);
        }
        public Node head;
        public int size;
        public class Node
        {
            (Key, Value) entry;
            public Key key;
            public Value value;
            public Node prev;
            public Node(Key k, Value v, Node p)
            {
                key = k;
                value = v;
                entry = (k, v);
                prev = p;
            }
        }
        public void insertNode(Key key, Value value)
        {
            head = new Node(key, value, head);
            size++;
        }
    }
    public class Program
    {
        static Hashtable books = new Hashtable(10);
        static Hashtable authors = new Hashtable(10);
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            books.peek();
            while (true)
            {
                Console.WriteLine("\nОберіть одину з дій:");
                Console.WriteLine("1 - додати книгу");
                Console.WriteLine("2 - знайти книгу за ключем");
                Console.WriteLine("3 - видалити книгу");
                Console.WriteLine("4 - знайти автора за ключем");
                Console.WriteLine("5 - переглянути список книг");
                Console.WriteLine("6 - переглянути список авторів");
                Console.WriteLine("7 - контрольний приклад");
                Console.WriteLine("8 - вийти з додатку\n");

                string m = Console.ReadLine();
                if (m.Length > 1 || !Char.IsNumber(m[0]))
                {
                    Console.WriteLine("\nВи обрали неіснуючу дії");
                    continue;
                }
                switch (int.Parse(m))
                {
                    case 1: addNew(); break;
                    case 2: findAuth(); break;
                    case 3: delete(); break;
                    case 4: findBooks(); break;
                    case 5: books.peek(); break;
                    case 6: authors.peekAuth(); break;
                    case 7: control(); break;
                    case 8: Environment.Exit(0); break;
                    default: Console.WriteLine("\nВи обрали неіснуючу дії"); break;
                }
            }
        }
        static public void addNew()
        {
            List<string> author = new List<string>();
            string title;
            Console.WriteLine("Введіть назву книги:");
            while (true)
            {
                title = Console.ReadLine();
                if (title == "")
                {
                    Console.WriteLine("Поле введення має бути заповненим:");
                    continue;
                }
                break;
            }
            Console.WriteLine("Введіть рік видання книги:");
            int year;
            while (true)
            {
                year = keyCheck("", 4);
                if (year >= 0 && year <= 2022)
                    break;
                Console.WriteLine("Введіть інсуючий рік:");
            }

            while (true)
            {
                Console.WriteLine("Введіть прізвище автора:");
                string surname = Check("", true);
                author.Add(surname);

                Console.WriteLine("1 - додати ще одного автора");
                Console.WriteLine("інше - закінчити введення");

                string m = Console.ReadLine();
                try
                {
                    if (int.Parse(m) != 1)
                        break;
                }
                catch
                {
                    break;
                }
            }

            bool b = true;
            Value v = books.insertEntry(null, null, title, author, year);
            while (b)
            {
                (books, b) = books.loadnessCheck(books);
            }

            b = true;
            authors.insertAddEntry(v, author);
            while (b)
            {
                (authors, b) = authors.loadnessCheck(authors);
            }
            books.peek();
        }
        static public string uniqueInt(string str)
        {
            str = Check(str, false);
            if (books.unique(str))
            {
                Console.WriteLine("Книга з даною назвою вже існує");
                return uniqueInt(str);
            }
            return str;
        }
        static public void findAuth()
        {
            Console.WriteLine("\nВведіть ID книги:");
            books.findEntry(keyCheck("", 6));
        }
        static public void delete()
        {
            Console.WriteLine("\nВведіть ID книги:");
            books.removeEntry(keyCheck("", 6));
            books.peek();
        }
        static public void findBooks()
        {
            Console.WriteLine("\nВведіть фамілію автора:");
            authors.findAllBooks(Check("", true));
        }
        static public int keyCheck(string str, int count)
        {
            str = Console.ReadLine();
            try
            {
                if (str.Length != count)
                {
                    Console.WriteLine($"Поле введеня має містити {count} цифр");
                    return keyCheck(str, count);
                }
                return int.Parse(str);
            }
            catch
            {
                Console.WriteLine($"Поле введеня має містити {count} цифр");
                return keyCheck(str, count);
            }
        }
        static public string Check(string str, bool b)
        {
            str = Console.ReadLine();
            if (str.Length > 15 || str.Length == 0)
            {
                Console.WriteLine("Довжина вводу повинна бути мінімум один та не більше 15 символів");
                str = Check(str, b);
                return str;
            }

            if (b)
            {
                List<char> alphabet = new List<char> { 'а', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и', 'і', 'ї', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я' };
                for (int i = 0; i < str.Length; i++)
                    if (!alphabet.Contains(Char.ToLower(str[i])))
                    {
                        Console.WriteLine("Поля мають містити тільки українські літери");
                        return str;
                    }
            }

            return str;
        }
        static public void control()
        {
            books = new Hashtable(5);
            books.keys.Clear();
            books.peek();
            Console.WriteLine("\n        |");
            Console.WriteLine("        v");
            authors = new Hashtable(5);
            (int, string, List<string>, int)[] example = new (int, string, List<string>, int)[7]
            {
            (172381, "Гори", new List<string>(), 2005 ),
            (724134, "Сіверський Донець", new List<string>(), 2015 ),
            (324892, "Самостійна Україна", new List<string>(), 1900 ),
            (241349, "Слобожанщина", new List<string>(), 1991 ),
            (261831, "Люди", new List<string>(), 2009 ),
            (268343, "Збірник жартів", new List<string>(), 1998 ),
            (493445, "Основи програмування", new List<string>(), 2013 ),
            };

            example[0].Item3.Add("Артимович");
            example[1].Item3.Add("Купчинський");
            example[1].Item3.Add("Балика");
            example[2].Item3.Add("Міхновський");
            example[3].Item3.Add("Артюшенко");
            example[3].Item3.Add("Вертій");
            example[3].Item3.Add("Балабан");
            example[4].Item3.Add("Балабан");
            example[5].Item3.Add("Купчинський");
            example[6].Item3.Add("Балика");
            example[6].Item3.Add("Артимович");

            Key k;
            Value val;

            for (int i = 0; i < 7; i++)
            {
                int count = 1;
                Value.LinkedList author = new Value.LinkedList(null);
                foreach (var item in example[i].Item3)
                {
                    if (count == 1)
                        author = new Value.LinkedList(item);
                    else
                        author.insertNode(item);
                    count++;
                }

                k = new Key(example[i].Item1.ToString());
                val = new Value(example[i].Item2.ToString(), author, example[i].Item4);

                bool b = true;
                val = books.insertEntry(k, val, null, example[i].Item3, 0);
                while (b)
                {
                    (books, b) = books.loadnessCheck(books);
                }

                b = true;
                authors.insertAddEntry(val, example[i].Item3);
                while (b)
                {
                    (authors, b) = authors.loadnessCheck(authors);
                }
            }
            books.peek();
        }
    }
}