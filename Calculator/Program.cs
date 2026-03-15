//https://github.com/Penkalatte00700/Calculator

namespace Calculator;



class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter your expression:");
        string expression = Console.ReadLine();

        try
        {
            string[] tokens = Calculations.Tokenize(expression);
            string[] rpn = Calculations.ToRpn(tokens);
            double result = Calculations.EvalRpn(rpn);
            Console.WriteLine(result);
        }
        catch (Exception exc)
        {
            Console.WriteLine("Error " + exc.Message);
        }
    }
}

