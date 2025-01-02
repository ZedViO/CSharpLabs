using System;
using System.Collections;
using System.Runtime.CompilerServices;

class MyMatrix
{
    private int m, n;
    private double[][] matrix;
    public MyMatrix(int _m, int _n, int rand_begin, int rand_end)
    {
        Random random = new Random();

        m = _m;                                                                 //Столбцы
        n = _n;                                                                 //Строки
        double[][] matr = new double[n][];                                          //Создаем каркас матрицы

        for (int i = 0; i < n; i++)
        {
            double[] line = new double[m];                                            //Создаем строку
            for (int j = 0; j < m; j++)                                         //Заполняем строку
            {
                line[j] = random.Next(rand_begin, rand_end);
            }
            matr[i] = line;                                                   //Добавляем строку в матрицу
        }
        matrix = matr;
    }
    public void Show()
    {
        foreach (double[] line in matrix)
        {
            foreach (double elem in line)
            {
                Console.Write($"{elem} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    public void Fill(int rand_begin, int rand_end)
    {
        Random random = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                matrix[i][j] = random.Next(rand_begin, rand_end);
            }
        }
    }
    
    public void ChangeSize(int newM, int newN, int rand_begin, int rand_end)
    {
        Random random = new Random();
        double[][] matrix = new double[newN][];
        for (int i = 0; i < newN; i++)
        {
            double[] line = new double[newM];
            for (int j = 0; j < newM; j++)
            {
                try 
                {
                    line[j] = this.matrix[i][j];
                }
                catch(System.IndexOutOfRangeException)
                {
                    line[j] = random.Next(rand_begin, rand_end);
                }
            }
            matrix[i] = line;
        }
        this.matrix = matrix;
    }
    public void ShowPartially(int mBegin, int nBegin, int mEnd, int nEnd) 
    {
        for (int i = nBegin; i <= nEnd; ++i)
        {
            for (int j = mBegin; j <= mEnd; ++j)
            {
                Console.Write($"{matrix[i][j]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    public double this[int indexRow, int indexCol]
    {
        get => matrix[indexCol][indexRow];
        set => matrix[indexCol][indexRow] = value;
    }
}

class MyList<T> : IEnumerable<T>
{
    private T[] list;
    private int count;

    public MyList()
    {
        list = new T[0];
        count = 0;
    }

    public MyList(params T[] list)
    {
        this.list = list;
        count = list.Length;
    }

    public MyList(IEnumerable<T> collection)
    {
        MyList<T> arr = new MyList<T>();
        foreach(T item in collection)
        {
            arr.Add(item);
        }
        list = arr.list; 
        count = arr.count;
    }

    public void Add(T elem)
    {
        if (count == list.Length)
        {
            // Увеличиваем размер массива, удваивая его текущий размер
            int newLength = count == 0 ? 4 : count * 2;
            T[] newList = new T[newLength];
            Array.Copy(list, newList, count);
            list = newList;
        }

        list[count] = elem;
        count++;
    }

    public void Pop()
    {
        Array.Resize(ref list, --count);
    }

    public void Remove(int index)
    {
        if (index >= count || index < 0)
        {
            Console.WriteLine("Выход за пределы списка");
        }
        else
        {
            T[] newList = new T[count - 1];

            for (int i = 0; i < index; i++)
            {
                newList[i] = list[i];
            }
            for (int i = index + 1; i < count; i++)
            {
                newList[i-1] = list[i];
            }
            list = newList;
            --count;
        }
    }

    public void Clear()
    {
        count = 0;
        T[] newList = new T[0];
        list = newList;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Выход за пределы массива");
            }
            return list[index];
        }
    }

    public int Count
    {
        get { return count; }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in list)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private List<KeyValuePair<TKey, TValue>> dict; 

    public MyDictionary(List<KeyValuePair<TKey, TValue>> list)
    {
        dict = list;
    }
    public MyDictionary()
    {
        dict = new List<KeyValuePair<TKey, TValue>>();
    }

    public void Add(TKey key, TValue value)
    {
        // Проверка наличия ключа в коллекции
        if (ContainsKey(key))
        {
            throw new ArgumentException("An item with the same key already exists.");
        }

        dict.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public void Clear()
    {
        dict = new List<KeyValuePair<TKey, TValue>>();
    }
    public void Remove(TKey key)
    {
        int indexToRemove = -1;

        for (int i = 0; i < dict.Count; i++)
        {
            if (EqualityComparer<TKey>.Default.Equals(dict[i].Key, key))
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove != -1)
        {
            dict.RemoveAt(indexToRemove);
        }
        else
        {
            throw new KeyNotFoundException($"The key '{key}' was not found in the dictionary.");
        }
    }
    public TValue this[TKey key]
    {
        get
        {
            foreach (var pair in dict)
            {
                if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
                {
                    return pair.Value;
                }
            }
            throw new KeyNotFoundException($"The key '{key}' was not found in the dictionary.");
        }
    }

    public int Count
    {
        get { return dict.Count; }
    }

    public bool ContainsKey(TKey key)
    {
        foreach (var pair in dict)
        {
            if (EqualityComparer<TKey>.Default.Equals(pair.Key, key))
            {
                return true;
            }
        }
        return false;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return dict.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
class Executable
{
    static void Main(string[] args)
    {
        //Задание 1
        MyMatrix m1 = new MyMatrix(4, 4, 0, 9);
        m1.Show();
        m1.Fill(0, 9);
        m1.Show();
        m1.ChangeSize(3, 3, 0, 9);
        m1.Show();
        m1.ShowPartially(0, 0, 2, 1);
        m1[0, 0] = 0;
        m1[1, 1] = 0;
        m1[2, 2] = 0;
        m1.Show();

        //Задание 2
        //Проверка работоспособности блока инициализации НАЧАЛО
        MyList<int> list = new MyList<int>() {1,2,3,4};
        Console.WriteLine($"БЛОК ИНИЦИАЛИЗАЦИИ {list[0]} {list[1]} {list[2]} {list[3]}");
        //Проверка работоспособности блока инициализации КОНЕЦ

        MyList<int> myList = new MyList<int>();
        myList.Add(1);
        myList.Add(2);
        myList.Add(3);
        myList.Add(4);
        myList.Add(5);

        for (int i = 0; i < myList.Count; i++)
        {
            Console.Write($"{myList[i]} ");
        }
        Console.WriteLine();

        myList.Pop();
        myList.Pop();

        for (int i = 0; i < myList.Count; i++)
        {
            Console.Write($"{myList[i]} ");
        }
        Console.WriteLine();

        myList.Remove(2);
        for (int i = 0; i < myList.Count; i++)
        {
            Console.Write($"{myList[i]} ");
        }
        Console.WriteLine();
    

        MyList<int> myList1 = new MyList<int>(1,2,3,4,5,6,7,8,9,10);
        myList1.Remove(3);
        myList1.Remove(0);
        myList1.Remove(myList1.Count - 1);

        for (int i = 0; i < myList1.Count; i++)
        {
            Console.Write($"{myList1[i]} ");
        }
        Console.WriteLine();

        myList1.Clear();
        for (int i = 0; i < myList1.Count; i++)
        {
            Console.Write($"{myList1[i]} ");
        }
        Console.WriteLine();

        //Задание 3
        MyDictionary<string, int> dictionary = new MyDictionary<string, int>();

        dictionary.Add("one", 1);
        dictionary.Add("two", 2);
        dictionary.Add("three", 3);

        dictionary.Remove("two");

        // Получение значения по ключу
        Console.WriteLine("Value for key 'one': " + dictionary["one"]);

        // Перебор элементов
        foreach (var kvp in dictionary)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}
