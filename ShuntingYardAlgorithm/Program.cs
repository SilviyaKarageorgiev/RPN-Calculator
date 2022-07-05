using System;
using System.Collections.Generic;

namespace ShuntingYardAlgorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string expression = Console.ReadLine();

            Stack<char> stack = new Stack<char>();

            Queue<string> queue = new Queue<string>();

            for (int iteration = 0; iteration < expression.Length; iteration++)
            {
                char currentSymbol = expression[iteration];

                if (char.IsDigit(currentSymbol))
                {
                    string number = currentSymbol.ToString();
                    if (iteration < expression.Length - 1)
                    {
                        for (int i = iteration + 1; i < expression.Length; i++)
                        {
                            if (char.IsDigit(expression[i]))
                            {
                                number += expression[i];
                                iteration++;
                            }
                            else
                            {
                                
                                break;
                            }
                        }
                    }
                    queue.Enqueue(number);
                    
                }

                else if (currentSymbol == '+')
                {
                    if (stack.Count != 0)
                    {
                        while (stack.Peek() == '*' || stack.Peek() == '/' || stack.Peek() == '^')
                        {
                            queue.Enqueue(stack.Pop().ToString());
                        }
                        
                    }
                    stack.Push(currentSymbol);

                }

                else if (currentSymbol == '-')
                {
                    if (stack.Count > 0)
                    {
                        if (stack.Peek() == '*' || stack.Peek() == '/' || stack.Peek() == '^')
                        {
                            queue.Enqueue(stack.Pop().ToString());
                            
                        }
                        
                    }
                    stack.Push(currentSymbol);
                    
                }

                else if (currentSymbol == '*' || currentSymbol == '/')
                {
                    if (stack.Count > 0)
                    {
                        if (stack.Peek() == '^')
                        {
                            queue.Enqueue(stack.Pop().ToString());
                            
                        }
                    }

                    stack.Push(currentSymbol);
                }
                                
                else if (currentSymbol == '^')
                {
                    stack.Push(currentSymbol);

                }

                else if (currentSymbol == '(')
                {
                    stack.Push(currentSymbol);
                }

                else if (currentSymbol == ')')
                {
                    if (stack.Count == 0 || !stack.Contains('('))
                    {
                        Console.WriteLine("Invalid expression!");
                    }
                    else
                    {
                        while (stack.Peek() != '(')
                        {
                            queue.Enqueue(stack.Pop().ToString());
                        }
                        stack.Pop();
                    }
                }

                else if (currentSymbol == ' ')
                {
                    continue;
                }
            }
            while (stack.Count > 0)
            {
                queue.Enqueue(stack.Pop().ToString());
            }

            Console.WriteLine(string.Join(" ", queue));

            Stack<double> result = new Stack<double>();

            double parsedNum;

            while (queue.Count > 0)
            {
                bool isInteger = double.TryParse(queue.Peek(), out parsedNum);
                if (isInteger)
                {
                    result.Push(parsedNum);
                    queue.Dequeue();
                }
                else
                {
                    if (queue.Peek() == "+")
                    {
                        double second = result.Pop();
                        double first = result.Pop();

                        double currResult = first + second;
                        result.Push(currResult);
                        queue.Dequeue();

                    }

                    else if (queue.Peek() == "-")
                    {
                        double second = result.Pop();
                        double first = result.Pop();

                        double currResult = first - second;
                        result.Push(currResult);
                        queue.Dequeue();

                    }

                    else if (queue.Peek() == "*")
                    {
                        double second = result.Pop();
                        double first = result.Pop();

                        double currResult = first * second;
                        result.Push(currResult);
                        queue.Dequeue();

                    }

                    else if (queue.Peek() == "/")
                    {
                        double second = result.Pop();
                        double first = result.Pop();

                        double currResult = first / second;
                        result.Push(currResult);
                        queue.Dequeue();
                    }

                    else if (queue.Peek() == "^")
                    {
                        double second = result.Pop();
                        double first = result.Pop();

                        double currResult = Math.Pow(first, second);
                        result.Push(currResult);
                        queue.Dequeue();
                    }

                    else
                    {
                        Console.WriteLine("Invalid expression!");
                    }
                }
            }

            Console.WriteLine(result.Pop());

        }
    }
}
