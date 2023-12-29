using AutoFixture;
using Calculator;
using FluentAssertions;
using Moq;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace CalculatorTests;

public class CalculatorTests
{
    [Theory]
    [InlineData(15, 20, 35)]
	[InlineData(35.5, 20, 55.5)]
	[InlineData(200.950, 400.05, 601)]
	public void Calculator_Add_ShouldReturnCorrectResult(double value1, double value2, double expectedValue)
	{
		// Arrange
		var fixture = new Fixture();
		var userIdentityService = fixture.Create<Mock<IUserIdentityService>>();

		var sut = new Calculator.Calculator(userIdentityService.Object);
		
		// Act
		var result = sut.Add(value1, value2);

        // Assert
		result.Should().Be(expectedValue);
	}

	[Theory, AutoMock]
	public void Calculator_LoginUser_Success_ShouldReturnTrue([Frozen]Mock<IUserIdentityService> userIdentityService,
															  string userName,
															  Guid userId)
	{
		// Arrange
		var sut = new Calculator.Calculator(userIdentityService.Object);

		userIdentityService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
						   .Returns(true);

		// Act
		var result = sut.LoginUser(userName, userId.ToString());

		// Assert
		result.Should().BeTrue();
	}

	[Fact]
	public void Calculator_LoginUser_Fail_ShouldReturnFalse()
	{
		// Arrange
		var fixture = new Fixture();
		var userIdentityService = fixture.Create<Mock<IUserIdentityService>>();
		var sut = new Calculator.Calculator(userIdentityService.Object);
		var userName = fixture.Create<string>();
		var userId = fixture.Create<Guid>().ToString();

		userIdentityService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
						   .Returns(false);

		// Act
		var result = sut.LoginUser(userName, userId);

		// Assert
		result.Should().BeFalse();
	}

	[Fact]
	public void Calculator_LoginUser_UnexpectedError_ShouldThrowArgumentNullException()
	{
		// Arrange
		var fixture = new Fixture();
		var userIdentityService = fixture.Create<Mock<IUserIdentityService>>();
		var sut = new Calculator.Calculator(userIdentityService.Object);
		var userName = fixture.Create<string>();
		var userId = fixture.Create<Guid>().ToString();

		userIdentityService.Setup(x => x.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
						   .Throws<ArgumentNullException>();

		// Act
		Action action = () => sut.LoginUser(userName, userId);

		// Assert
		action.Should().Throw<ArgumentNullException>();
	}

	public class AutoMockAttribute : AutoDataAttribute
	{
		public AutoMockAttribute()
            : base(() => new Fixture().Customize(new AutoMoqCustomization()))
		{ }
	}

}