using ClassLibrary1;

namespace ClassAssignmentAvansJeroenPost;

public class DetermineShippingCostsTests
{
    private DetermineShippingCosts _determineShippingCosts;

    [SetUp]
    public void Setup()
    {
        _determineShippingCosts = new DetermineShippingCosts();
    }

    [Test]
    public void DetermineShippingCosts_CalculateShippingCostsFalse_ReturnsZero()
    {
        // Arrange
        const bool calculateShippingCosts = false;
        const string typeOfShippingCosts = "Ground";
        const int totalPrice = 100;

        // Act
        var result = _determineShippingCosts.ShippingCosts(calculateShippingCosts, typeOfShippingCosts, totalPrice);

        // Assert
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void DetermineShippingCosts_TotalPriceGreaterThan1500_ReturnsZero()
    {
        // Arrange
        const bool calculateShippingCosts = true;
        const string typeOfShippingCosts = "Ground";
        const int totalPrice = 1501;
        
        // Act
        var result = _determineShippingCosts.ShippingCosts(calculateShippingCosts, typeOfShippingCosts, totalPrice);
        
        // Assert
        Assert.That(result, Is.EqualTo(0));
    }
    
    [Test]
    public void DetermineShippingCosts_TypeOfShippingCostsGround_Returns100()
    {
        // Arrange
        const bool calculateShippingCosts = true;
        const string typeOfShippingCosts = "Ground";
        const int totalPrice = 100;
        
        // Act
        var result = _determineShippingCosts.ShippingCosts(calculateShippingCosts, typeOfShippingCosts, totalPrice);
        
        // Assert
        Assert.That(result, Is.EqualTo(100));
    }
    
    [Test]
    public void DetermineShippingCosts_TypeOfShippingCostsInstore_Returns50()
    {
        // Arrange
        const bool calculateShippingCosts = true;
        const string typeOfShippingCosts = "InStore";
        const int totalPrice = 100;
        
        // Act
        var result = _determineShippingCosts.ShippingCosts(calculateShippingCosts, typeOfShippingCosts, totalPrice);
        
        // Assert
        Assert.That(result, Is.EqualTo(50));
    }
    
    [Test]
    public void DetermineShippingCosts_TypeOfShippingCostsNextDayAir_Returns250()
    {
        // Arrange
        const bool calculateShippingCosts = true;
        const string typeOfShippingCosts = "NextDayAir";
        const int totalPrice = 100;
        
        // Act
        var result = _determineShippingCosts.ShippingCosts(calculateShippingCosts, typeOfShippingCosts, totalPrice);
        
        // Assert
        Assert.That(result, Is.EqualTo(250));
    }
    
    [Test]
    public void DetermineShippingCosts_TypeOfShippingCostsSecondDayAir_Returns125()
    {
        // Arrange
        const bool calculateShippingCosts = true;
        const string typeOfShippingCosts = "SecondDayAir";
        const int totalPrice = 100;
        
        // Act
        var result = _determineShippingCosts.ShippingCosts(calculateShippingCosts, typeOfShippingCosts, totalPrice);
        
        // Assert
        Assert.That(result, Is.EqualTo(125));
    }
    
    [Test]
    public void DetermineShippingCosts_TypeOfShippingCostsUnknown_Returns0()
    {
        // Arrange
        const bool calculateShippingCosts = true;
        const string typeOfShippingCosts = "Unknown";
        const int totalPrice = 100;
        
        // Act
        var result = _determineShippingCosts.ShippingCosts(calculateShippingCosts, typeOfShippingCosts, totalPrice);
        
        // Assert
        Assert.That(result, Is.EqualTo(0));
    }
}