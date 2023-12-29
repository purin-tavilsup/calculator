namespace CalculatorApp;

internal class Program
{
	private static void Main(string[] args)
	{
		var calculator = new Calculator.Calculator();

		var additiion = calculator.Add(30.95, 10);
		var subtrcation = calculator.Subtract(300, 150);
		var multiplication = calculator.Multiply(70, 2);

		Console.WriteLine($"Add: {additiion}");
		Console.WriteLine($"Subtract: {subtrcation}");
		Console.WriteLine($"Multiply: {multiplication}");
	}
}

