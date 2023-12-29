namespace Calculator
{
    public class Calculator : ICalculator
    {
		private readonly IUserIdentityService _userIdentityService;

		public Calculator(IUserIdentityService userIdentityService)
		{
			_userIdentityService = userIdentityService;
		}

		public double Add(double value1, double value2)
		{
			return value1 + value2;
		}

		public double Subtract(double value1, double value2)
		{
			return value1 - value2;
		}

		public double Multiply(double value1, double value2)
		{
			return value1 * value2;
		}

		public double Divide(double value1, double value2)
		{
            return value1 / value2;
        }

		public bool LoginUser(string username, string userId)
		{
			return _userIdentityService.ValidateUser(userId, username);
		}
	}
}
