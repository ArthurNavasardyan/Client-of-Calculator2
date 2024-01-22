using Action = WebCalculator2.Entity.Action;

namespace WebCalculator2
{
    public class Calculation
    {
        public double CreateCalculation(double a, string action, double b)
        {            
            double result;           

            switch (action)
            {
                case "+":
                    result = a + b;                    
                    break;
                case "-":
                    result = a - b;                   
                    break;
                case "*":
                    result = a * b;                    
                    break;
                case "/":
                    if (b != 0)
                    {
                        result = a / b;                        
                        break;
                    }
                    else
                    {
                        throw new ArithmeticException("the number can't division by zero");
                    }
                default:
                    {
                        throw new Exception("There is no such action,the action must be․" +
                                       "mount(+) or subtraction(-) or multiplication(*) or division(/)");
                    }

            }

            return result;
        }
    }
}