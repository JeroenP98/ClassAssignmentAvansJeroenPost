using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    
    [Test]
    public async Task UitTest()
    {
        /*
         *  ARRANGE
         */
        
        await Page.GotoAsync("https://demowebshop.tricentis.com/");

        // Check if user is already logged in. if so, clear cart and log out.
        // If the logout button is visible, the user is logged in
        var logoutButton = Page.GetByRole(AriaRole.Link, new() { Name = "Log out" });
        if (await logoutButton.IsVisibleAsync())
        {
            await ClearCart();
            await Logout();
        }
         
        /*
         * ACT
         */
        
        // log in as the user
        await Login("jf.post1@student.avans.nl", "123456");
        
        // check if the cart is empty
        await Page.GotoAsync("https://demowebshop.tricentis.com");
        var cartAmount = Page.Locator(".cart-qty");
        if (await cartAmount.TextContentAsync() != "(0)")
        {
            await ClearCart();
        }
        
        // Generate a random int between 2 and 10 (inclusive) to set the number of items to add to the cart
        var random = new Random();
        var smartphonesRandom = random.Next(2, 11);
        var beltsRandom = random.Next(2, 11);
        
        // navigate to smartphone product page
        await Page.GotoAsync("https://demowebshop.tricentis.com/smartphone");
        // Retrieve the price value
        var smartPhonePrice = await GetProductPrice("https://demowebshop.tricentis.com/smartphone");
        var totalSmartphonePrice = smartPhonePrice * smartphonesRandom;

        // add the product to the cart
        for (var i = 0; i < smartphonesRandom; i++)
        {
            await Page.Locator("#add-to-cart-button-43").ClickAsync();
            await Task.Delay(1000);
        }
        
        // navigate to belt product page
        await Page.GotoAsync("https://demowebshop.tricentis.com/casual-belt");
        // Retrieve the price value
        var beltPrice = await GetProductPrice("https://demowebshop.tricentis.com/casual-belt");
        var totalBeltPrice = beltPrice * beltsRandom;

        // add the product to the cart
        for (var i = 0; i < beltsRandom; i++)
        {
            await Page.Locator("#add-to-cart-button-40").ClickAsync();
            await Task.Delay(1000);
        }
        
        
        /*
         * ASSERT
         */

        var totalExpectedPrice = totalBeltPrice + totalSmartphonePrice;
        await Page.GotoAsync("https://demowebshop.tricentis.com/cart");
        var totalPriceString = await Page.GetByRole(AriaRole.Row, new() { Name = "Sub-Total:" }).Locator("span").Nth(2).TextContentAsync();
        var totalPrice = double.Parse(totalPriceString.Replace(".", ","));
        
        Assert.That(totalExpectedPrice, Is.EqualTo(totalPrice));
        
        // Finally, log out
        await Logout();
    }

    private async Task ClearCart()
    {
        // Navigate to the cart
        await Page.GotoAsync("https://demowebshop.tricentis.com/cart");
            
        // Check if cart is empty, if not, remove each item individually and update cart
        var emptyCartTextLocator = Page.Locator("text=Your Shopping Cart is empty!");
        if (!await emptyCartTextLocator.IsVisibleAsync())
        {
            var removeButtons = Page.Locator("input[name='removefromcart']");
            for (var i = 0; i < await removeButtons.CountAsync(); i++)
            {
                await removeButtons.Nth(i).CheckAsync(); // Check the remove checkbox
            }

            // Click the "Update shopping cart" button to remove items
            await Page.ClickAsync("input[name='updatecart']");

            // Confirm the cart is empty after updating
            await Expect(emptyCartTextLocator).ToBeVisibleAsync();
            Console.WriteLine("Cart has been cleared successfully.");
        }
    }
    
    private async Task<double> GetProductPrice(string productUrl)
    {
        await Page.GotoAsync(productUrl);
        var priceText = await Page.Locator(".product-price").TextContentAsync();
        return double.Parse(priceText, System.Globalization.CultureInfo.InvariantCulture);
    }
    
    private async Task Logout()
    {
        var logoutButton = Page.GetByRole(AriaRole.Link, new() { Name = "Log out" });
        if (await logoutButton.IsVisibleAsync())
        {
            await logoutButton.ClickAsync();
        }
    }
    
    private async Task Login(string email, string password)
    {
        await Page.GotoAsync("https://demowebshop.tricentis.com/login");
        await Page.GetByLabel("Email:").FillAsync(email);
        await Page.GetByLabel("Password:").FillAsync(password);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        await Page.GotoAsync("https://demowebshop.tricentis.com");
    }

}
