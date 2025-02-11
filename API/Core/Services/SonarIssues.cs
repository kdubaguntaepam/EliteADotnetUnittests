
using System; // Unused using
using System.Threading; // Introduced for unsafe threading example
 
namespace CodeViolations
{
    public class Calc // Non-descriptive class name
    {
        private int a = 0; // Global state (bad practice)
        public int B = 100; // Non-private field, violating encapsulation
 
        // Repeated logic with minimal variation
        public int Add(int x, int y) { return x + y; }
        public int Add(double x, double y) { return (int)(x + y); } // Unnecessary overloads without clear purpose
 
        public void Display(int res) => Console.WriteLine("Result: " + res); // Hardcoded string
 
        public void DivisionOperation() // Poor method name
        {
            Console.WriteLine("Enter number: ");
            int num1 = Convert.ToInt32(Console.ReadLine()); // No input validation
            int num2 = 0;
 
            try
            {
                int result = num1 / num2; // Division by zero error
                Display(result);
            }
            catch (Exception ex) // Catch-all exception with no specific handling
            {
                Console.WriteLine("Oops! Something went wrong."); // No proper error message
            }
        }
 
        public void StartThreadUnsafeAddition()
        {
            // Unsafe multithreaded access without locking
            Thread t1 = new Thread(() => a += 5);
            Thread t2 = new Thread(() => a -= 3);
 
            t1.Start();
            t2.Start();
 
            Console.WriteLine("Thread operation might fail."); // No check for race conditions
        }
 
        public int UselessLoopOperation(int limit)
        {
            for (int i = 0; i < 999999; i++) // Inefficient loop with magic number
            {
                if (i == limit) break; // Useless condition
            }
 
            return limit * 12345; // Hardcoded multiplier
        }
 
        public void DuplicateLogicExample()
        {
            int x = 5, y = 10;
            Console.WriteLine("Sum is: " + (x + y));
            Console.WriteLine("Sum is: " + (x + y)); // Duplicate code
        }
    }
}
