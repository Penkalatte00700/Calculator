//https://github.com/Penkalatte00700/Calculator

namespace Calculator;

public class MyStack<T>
{
    private T[] data;
    private int count;

    public MyStack()
    {
        data = new T[4];
        count = 0;
    }

    public int Count
    {
        get { return count;  }
    }

    public void Push(T item)
    {
        if (count == data.Length)
        {
            T[] newData = new T[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                newData[i] = data[i];
            }

            data = newData;
        }

        data[count] = item;
        count++;
    }

    public T Pop()
    {
        if (count == 0)
        {
            throw new Exception("Stack is empty");
        }

        count--;
        return data[count];
    }

    public T Peek()
    {
        if (count == 0)
        {
            throw new Exception("Stack is empty");
        }

        return data[count - 1];
    }

    public bool IsEmpty()
    {
        return count == 0;
    }
}