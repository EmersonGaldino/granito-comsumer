using System.Diagnostics.CodeAnalysis;
using granito.consumer.domain.Configuration.Service;
using granito.consumer.domain.Entity;
using granito.consumer.domain.Interface.Http;
using granito.consumer.domain.Interface.Juros;
using granito.consumer.domain.Service.Juros;
using Moq;
using Xunit;

namespace granito.consumer.test.Juros;

public class CalculateTests
{
    private readonly Mock<IJurosService> _mockService = new();
    private readonly Mock<IWebRequestService> _mockWebRequestService= new();
    private readonly Mock<ServiceConfig> _mockServiceConfig= new();
    private JurosService GetService() => new JurosService(_mockWebRequestService.Object, _mockServiceConfig.Object);

    [Fact(DisplayName = "Should return a correct calculated value")]
    public async Task ShoudCalculate()
    {
        //Arrange
        var taxa = 1;
        var item = new JurosEntity
        {
            Months = 5,
            ValueInitial = 100,
        };
        _mockService.Setup(x =>
            x.Post(It.IsAny<JurosEntity>())
        )!.ReturnsAsync(new JurosEntity());
        var service = GetService();

        //ACT
        var data = service.Calculate(item, taxa);

        //Assert
        Assert.NotNull(data);
        Assert.Equal("105,10", data.ValuePortion.ToString());
        
    }
    [Fact(DisplayName = "should return a wrong calculated value")]
    public async Task ShoudErrorCalculate()
    {
        //Arrange
        var taxa = 1;
        var item = new JurosEntity
        {
            Months = 5,
            ValueInitial = 200,
        };
        _mockService.Setup(x =>
            x.Post(It.IsAny<JurosEntity>())
        )!.ReturnsAsync(new JurosEntity());
        var service = GetService();

        //ACT
        var data = service.Calculate(item, taxa);

        //Assert
        Assert.NotNull(data);
        Assert.NotEqual("210,10", data.ValuePortion.ToString());
        
    }
}