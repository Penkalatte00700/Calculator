//https://github.com/Penkalatte00700/Calculator

namespace Calculator;

public static class Calculations
{
    public static string[] Tokenize(string expr)
    {
        string[] temp = new string[expr.Length];
        int count = 0;
        string w = "";
        string number = "";

        for (int i = 0; i < expr.Length; i++)
        {
            char a = expr[i];

            if (char.IsWhiteSpace(a))
            {
                continue;
            }

            if (a == '-')
            {
                bool u = number == "" && (count == 0 || temp[count - 1] == "(" || IsOperator(temp[count - 1]));
                if (u)
                {
                    number += a;
                    continue;
                }
            }

            if (char.IsDigit(a))
            {
                number += a;
            }
            else if (char.IsLetter(a))
            {
                w += a;
            }
            else
            {
                if (number != "")
                {
                    temp[count] = number;
                    count++;
                    number = "";
                }

                if (w != "")
                {
                    if (!IsFunction(w))
                    {
                        throw new Exception("Wrong function");
                    }

                    temp[count++] = w;
                    w = "";
                }

                if (a == ',' || a == '+' || a == '-' || a == '*' || a == '/' || a == ')' || a == '(' || a == '^')
                {
                    temp[count] = a.ToString();
                    count++;
                }
                else
                {
                    throw new Exception("Invalid char in expr");
                }
            }
        }

        if (number != "")
        {
            temp[count] = number;
            count++;
        }

        if (w != "")
        {
            if (!IsFunction(w))
            {
                throw new Exception("Wrong function");
            }
            temp[count++] = w;
        }

        string[] result = new string[count];
        
        for (int i = 0; i < count; i++)
        {
            result[i] = temp[i];
        }

        return result;
    }
    private static bool IsNum(string s)
    {
        return double.TryParse(s, out _);
    }

    private static bool IsOperator(string s)
    {
        return s == "+" || s == "*" || s == "/" || s == "-" || s == "^";
    }

    private static bool IsFunction(string s)
    {
        return s == "sin" || s == "cos" || s == "max";
    }

    private static int Priority(string oper)
    {
        if (oper == "+" || oper == "-")
        {
            return 1;
        }

        if (oper == "*" || oper == "/")
        {
            return 2;
        }
        if (oper == "^")
        {
            return 3;
        }

        return 0;
    }

    public static string[] ToRpn(string[] tokens)
    {
        string[] output = new string[tokens.Length];
        int outCount = 0;
        MyStack<string> ops = new MyStack<string>();

        for (int i = 0; i < tokens.Length; i++)
        {
            string token = tokens[i];

            if (IsNum(token))
            {
                output[outCount] = token;
                outCount++;
            }
            else if (IsFunction(token))
            {
                ops.Push(token);
            }
            else if (token == ",")
            {
                while (!ops.IsEmpty() && ops.Peek() != "(")
                {
                    output[outCount] = ops.Pop();
                    outCount++;
                }

                if (ops.IsEmpty())
                {
                    throw new Exception("Problem with comma");
                }
            }
            else if (IsOperator(token))
            {
                while (!ops.IsEmpty() && IsOperator(ops.Peek()) && Priority(ops.Peek()) > Priority(token))
                {
                    output[outCount] = ops.Pop();
                    outCount++;
                }

                ops.Push(token);
            }
            else if (token == "(")
            {
                ops.Push(token);
            }
            else if (token == ")")
            {
                while (!ops.IsEmpty() && ops.Peek() != "(")
                {
                    output[outCount] = ops.Pop();
                    outCount++;
                }

                if (ops.IsEmpty())
                {
                    throw new Exception("Problem with parentheses");
                }

                ops.Pop();


            }
            
        }
        while (!ops.IsEmpty())
        {
            string top = ops.Pop();

            if (top == "(" || top == ")")
            {
                throw new Exception("Problem with parantheses");
            }

            output[outCount] = top;
            outCount++;
        }



        string[] result = new string[outCount];
        for (int i = 0; i < outCount; i++)
        {
            result[i] = output[i];
        }

        return result;
        
    }

    public static double EvalRpn(string[] rpn)
    {
        MyStack<double> stack = new MyStack<double>();
        for (int i = 0; i < rpn.Length; i++)
        {
            string token = rpn[i];
            if (IsNum(token))
            {
                stack.Push(double.Parse(token));
            }
            else if (IsOperator(token))
            {
                if (stack.GetCount() < 2)
                {
                    throw new Exception("Invalid expr");
                }
                double b = stack.Pop();
                double a = stack.Pop();

                switch (token)
                {
                    case "+":
                        stack.Push(a+b);
                        break;
                    case "-":
                        stack.Push(a-b);
                        break;
                    case "*":
                        stack.Push(a*b);
                        break;
                    case "/":
                        if (b == 0)
                        {
                            throw new DivideByZeroException("We can not divide on 0");
                        }
                        stack.Push(a/b);
                        break;
                    case "^":
                        stack.Push(Math.Pow(a, b));
                        break;
                }
            }
            else if (IsFunction(token))
            {
                if (token == "sin")
                {
                    double znach = stack.Pop();
                    stack.Push(Math.Sin(znach));
                }
                if (token == "cos")
                {
                    double znach = stack.Pop();
                    stack.Push(Math.Cos(znach));
                }
                if (token == "max")
                {
                    double znach = stack.Pop();
                    double znach1 = stack.Pop();
                    stack.Push(Math.Max(znach, znach1));
                }
                
            }
        }

        if (stack.GetCount() != 1)
        {
            throw new Exception("Invalid expr");
        }
        return stack.Pop();
    }
    
}