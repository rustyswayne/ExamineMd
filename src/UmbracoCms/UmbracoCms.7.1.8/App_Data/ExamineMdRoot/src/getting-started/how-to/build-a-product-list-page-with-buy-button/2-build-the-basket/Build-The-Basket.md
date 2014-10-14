# How to make a Product List page with "Add to Basket" buttons in Merchello, Part 2

## Introduction

In this example, we will create a product list page with "Add to Basket" buttons for each product. When the "Add to Basket" button is pressed, the basket page will display. The Umbraco version is 7.1.6 and the Merchello version is 1.4.

### Part 1 focused on the Product List and "Add to Basket" button

This is a two-part example. The first part focused on the product list, the "Add to Basket" button, and the files that supported those web pages.

### Part 2 focuses on the Basket

The second part of the example will focus on the basket page.

## The Customer

Umbraco provides membership services. As a merchant, you may choose to build your merchant customers from these membership services, provide for anonymous customers, or a blend of both. Starting in 1.4, Customers will be created automatically when a member of a specified type is created through the Umbraco Membership. While a Merchello customer can begin as an anonymous customer (table name: merchAnonymousCustomer), there is a process to convert to a known customer (table name: merchCustomer).

For the purposes of this example, all customers will be anonymous.

## The Basket

At the end of part 1, the product list and "Add to Basket" buttons were functional and redirected to the basket when selected. However the basket was empty both visually and in data storage.

!(browser-empty-basket.png)

The basket needs to be able to display current basket items as well as provide for:

- Updating basket item to a different quantity 
- Delete basket item
- Continue to checkout
- Continue shopping

Several existing files need to be modified as well as a few new files added. Since we already have a BasketController and a Basket view, these files need to be modified. The individual basket item's functionality and storage will be done with new models and a new partial view.

### List of New Files:

| File | Purpose |
|----|---|
| /App\_Code/Models/BasketViewLineItem.cs | Individual basket item |
| /App\_Code/Models/BasketViewModel.cs | The Basket information to display: price, items, customer |
| /App\_Code/Models/BasketViewModelEntensions.cs | Convert Merchello's IBasket to BasketViewModel |
| /Views/Partials/Basket.cshtml | Basket items, total value |
| /Views/Partials/Customer.cshtml | Customer information with basket summary information – presented at the end of Part 2 |

### Create the Models

Part 1 of this example focused on the AddItemModel used by the "Add to Basket" button. In Part 2, we need to add 3 models to manage the basket.

#### BasketViewModel

The first model, the BasketViewModel, contains the information necessary to display the basket.

using Merchello.Core.Models;

	namespace Models
	{
	  // simple basket view model
	  publicclassBasketViewModel
	  {
	    publicdecimal TotalPrice { get; set; }
	    publicBasketViewLineItem[] Items { get; set; }
	  }
	}

#### BasketViewLineItem

The next model contains the information for each product in the basket. Notice that both the Umbraco content id and the Merchello guid are part of the object

	using System;
	using Merchello.Core.Models;
	
	namespace Models
	{
	
	  public class BasketViewLineItem
	  {
	
	    // Merchello Product Guid
	    publicGuid Key { get; set; }
	
	    // Umbraco Content Id - notice in BasketViewModelExtensions that it is
	    // pulled from the extended properties
	    publicint ContentId { get; set; }
	
	    ///
	    /// Umbraco Content page name or Merchello Product Name
	    ///
	    publicstring Name { get; set; }
	
	    ///
	    /// This is a the extended properties that travel forward with the basket
	    ///
	    publicstring ExtendedData { get; set; }
	
	    ///
	    /// The sku
	    ///
	    publicstring Sku { get; set; }
	
	    ///
	    /// The price
	    ///
	    publicdecimal UnitPrice { get; set; }
	
	    ///
	    /// The total price of the line item
	    ///
	    publicdecimal TotalPrice { get; set; }
	
	    ///
	    /// The quantity to purchase
	    ///
	    publicint Quantity { get; set; }
	  }
	}

#### BasketViewModelExtension

This model turns a Merchello product object into a display on the actual basket page. There is a lot of room from interpretation and negotiation of this extension. As this is just an example, use or change it as you normally would in your own development environment.

##### Extended data 

This example displays the product extended data as a name/value list pumped into a string in on the Basket page. This is presented just so you can see how extended data are used in Merchello. Extended data are used in several areas of Merchello, all for the purpose of allowing more information in a less rigid format. In the BasketViewModelExtension.cs, you will see that the UmbracoContentId is pulled from the Merchello basket item's extended data.

An example for the Boots product is [name:value;]:

merchWeight:10.000000;merchDownload:False;merchShippable:True;merchLength:10.000000;merchManufacturer:BootsAreUs;merchTaxable:True;merchBarcode:;merchOutOfStockPurchase:False;merchOnSale:False;merchHeight:10.000000;merchSalePrice:5.000000;merchProductVariantKey:fc6291d1-36d2-4094-8315-b828eaeceda6;merchProductKey:17a1fc86-511f-495c-9728-0f0c215d53d2;merchTrackInventory:True;merchCostOfGoods:0.000000;merchPrice:10.000000;merchDownloadMediaId:-1;merchManufacturerModelNumber:BAU123;merchWidth:10.000000;

##### The Umbraco Content Name versus the Merchello Product Name

The ToBasketViewLineItems method has two choices for the name of the item to show in the basket. The first is the content page associated with the product in Umbraco. This content id was added to the line item extended data in the BasketController. The content id is used to get the name out of the content object. The second choice is the Merchello product name. This is already in the lineItem object as the Name.

For a smaller or unified site, these two names will probably be the same or similar enough to be irrelevant in terms of invoice, shipping, email notification, etc. However, if you have two different people managing these different areas or the names themselves indicate different information, you should choose which name to use.

Since the products were prepended with "Merchello" in the product catalog, and the Umbraco content pages are prepended with "Umbraco", you can see which you are getting when the code is executed.

	using System.Linq;
	using Merchello.Core.Models;
	using Merchello.Web.Workflow;
	using Umbraco.Core.Models;
	using Umbraco.Core.Services;
	using Umbraco.Web;

	namespace Models
	{
	
	  ///
	  /// Convert Merchello Product object to Displayable Product
	  ///
	  public static class BasketViewModelExtensions
	  {
	    public static BasketViewModel ToBasketViewModel(thisIBasket basket)
	    {
	      return new BasketViewModel()
	      {
	        TotalPrice = basket.TotalBasketPrice,
	        Items = basket.Items.Select(item => item.ToBasketViewLineItem()).OrderBy(x => x.Name).ToArray()
	      };
	    }
	
	    ///
	    /// Used to display extended data - for example purposes only
	    ///
	    private static string DictionaryToString(ExtendedDataCollection extendedData)
	    {
	      var extendedDataAsString = string.Empty;
	
	      foreach (var dataItem in extendedData)
	      {
	        extendedDataAsString += dataItem.Key + ":" + dataItem.Value + ";";
	      }
	
	      return extendedDataAsString;
	    }
	
	    ///
	    /// Utility extension to map a  to a BasketViewLine item.
	    /// Notice that the ContentId is pulled from the extended data. The name can either
	    /// be the Merchello product name via lineItem.Name or the Umbraco content page
	    /// name with umbracoHelper.Content(contentId).Name
	    ///
	    ///
	    ///The  to be mapped
	    ///
	    private static BasketViewLineItem ToBasketViewLineItem(thisILineItem lineItem)
	    {
	
	      var umbracoHelper = newUmbracoHelper(UmbracoContext.Current);
	
	      var contentId = lineItem.ExtendedData.ContainsKey("umbracoContentId") ? int.Parse(lineItem.ExtendedData["umbracoContentId"]) : 0;
	
	      var umbracoName = umbracoHelper.Content(contentId).Name;
	      //var merchelloProductName = lineItem.Name;

	      returnnewBasketViewLineItem()
	      {
	
	        Key = lineItem.Key,
	        ContentId = contentId,
	        ExtendedData = DictionaryToString(lineItem.ExtendedData),
	        //Name = merchelloProductName,
	        Name = umbracoName,
	        Sku = lineItem.Sku,
	        UnitPrice = lineItem.Price,
	        TotalPrice = lineItem.TotalPrice,
	        Quantity = lineItem.Quantity
	
	      };
	    }
	  }
	}

Now that the models are built, we should create the partial view that will use them.

### Create the Partial View Basket.cshtml

The partial view of basket.cshtml is really the whole basket. As a partial view, it can used anywhere. The view will display the basket items in an HTML table with the ability to update the quantity or remove the line item. Links to the product list and checkout (not included in this example) will also display.

In this example, we display the extended data of each line item. You can see that all the properties are Merchello product properties, except for the one property of umbracoContentId that was added to the extended data in the BasketController.AddToBasket.

!(browser-basket-with-an-order-and-extended.png)

	@inherits Merchello.Web.Mvc.MerchelloViewPage
	@using Controllers
	@using Models
	
	Basket
	
	@(Model.Items.Any() ? RenderBasket() : RenderEmpty())

	@helper RenderEmpty()
	{
	  There's nothing in your shopping cart, start shopping!
	}
	
	@helper RenderBasket()
	{
	
	  // Renders an "Updatable" basket
	  using(Html.BeginUmbracoForm("UpdateBasket"))
	  {
        <table class="table">
            <thead>
                <tr>
            	    <th class="name">Product</th>
            	    <th class="price">Price</th>
            	    <th class="quantity">Quantity</th>
            	    <th class="total">Total</th>
            	    <th class="delete">Delete</th>
            	    <th class="">Extended Data</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td colspan="5"><hr /></td>
                </tr>

	@{
	      // iterates through every line item in the basket. A for loop is used here
	
	      // so that MVC Html.HiddenFor and Html.ActionLink will properly create the references to the Quantity fields that
	
	      // are posted back to the controller to be updated.
	      for (var i = 0; i < Model.Items.Count(); i++)
	      {
                <tr>
                    <td class="image">
                        <span>@Model.Items[i].Name</span>                                                     
                        @Html.HiddenFor(model => model.Items[i].Key)                 
                    </td>
	                <td >@Model.Items[i].UnitPrice.ToString("C2")</td>
                    <td >@Html.TextBoxFor(model => model.Items[i].Quantity, new { @class = "col-xs-2"})</td>
                    <td >@((Model.Items[i].TotalPrice).ToString("C2"))</td>
                    <td >@Html.ActionLink("X", "RemoveItemFromBasket", "Basket", new {lineItemKey = Model.Items[i].Key}, null)</td>
                    <td class="">@Model.Items[i].Attributes</td>
                </tr>

	      }
	}
                <tr>
                    <td colspan="5"><hr /></td>
                </tr>
                <tr>
                    <td colspan="3">Sub Total</td>
                    <td colspan="2">@Model.TotalPrice.ToString("C2")</td>
                </tr>

            </tbody>
        </table>       
	
        <input type="submit" name="update" value="Update" /><br /><br />
	  }
	
    <a href="/productlist/">Continue Shopping</a><br />
    <a href="/basket/">Basket</a><br />
    <a href="/checkout/">Checkout</a><br />      

	}

### Modify the Basket Template to use the Partial View Basket

	@inherits Umbraco.Web.Mvc.UmbracoTemplatePage
	@{
	  Layout = null;
	}
	@Html.Action("DisplayBasket", "Basket", new { area = "MerchelloProductListExample"})

### Modify the BasketController 

The Basket controller needs to have several new methods: UpdateBasket, RemoveItemFromBasket, DisplayBasket. An additional method, DisplayCustomerBasket, has also been added and will be discussed at the end of this example.

	namespace Controllers
	{
	  using System;
	  using System.Collections.Generic;
	  using System.Linq;
	  using System.Web;
	  using System.Web.Mvc;
	  using Models;
	  using Merchello.Core;
	  using Merchello.Core.Models;
	  using Merchello.Web.Models.ContentEditing;
	  using Umbraco.Core.Logging;
	  using Umbraco.Web.Mvc;
	  using System.Globalization;
	
	  ///
	  /// Summary description for BasketController
	  ///
	  [PluginController("MerchelloProductListExample")]
	  publicclassBasketController : MerchelloSurfaceContoller
	  {

    // TODO These would normally be passed in or looked up so that there is not a
    // hard coded reference
    privateconstint BasketContentId = 1089;

    public BasketController()
      : this(MerchelloContext.Current)
    {
    }

    public BasketController(IMerchelloContext merchelloContext)
      : base(merchelloContext)
    {
    }

    //
    //Renders the BuyButton form which is a partial view
    //
    publicActionResult Display_BuyButton(AddItemModel product)
    {
      return PartialView("BuyButton", product);
    }

    [HttpPost]
    publicActionResult AddToBasket(AddItemModel model)
    {

      // Add to Logical Basket
      // add Umbraco content id to Merchello Product extended data
      var extendedData = newExtendedDataCollection();

      extendedData.SetValue("umbracoContentId", model.ContentId.ToString(CultureInfo.InvariantCulture));

      // get Merchello product
      var product = Services.ProductService.GetByKey(model.ProductKey);

      // add a single item of the Product to the logical Basket
      Basket.AddItem(product, product.Name, 1, extendedData);

      // Save to Database tables: merchItemCache, merchItemCacheItem
      Basket.Save();

      return RedirectToUmbracoPage(BasketContentId);
    }

    [ChildActionOnly]
    publicActionResult DisplayBasket()
    {
      return PartialView("Basket", GetBasketViewModel());
    }

    ///
    /// Responsible for updating the quantities of items in the basket.
    ///
    /// Redirects to the current Umbraco page (the basket page)
    [HttpPost]
    publicActionResult UpdateBasket(BasketViewModel model)
    {
      if (ModelState.IsValid)
      {
        // The only thing that can be updated in this basket is the quantity
        foreach (var item in model.Items)
        {
          if (Basket.Items.First(x => x.Key == item.Key).Quantity != item.Quantity)
          {
            Basket.UpdateQuantity(item.Key, item.Quantity);
          }
        }

        // Tidbit - Everytime "Save()" is called on the Basket, a new VersionKey (Guid) is generated.
        // This is used to validate the SalePreparationBase (BasketCheckoutPrepartion in this case),
        // asserting that any previously saved information (rate quotes, shipments ...) correspond to the Basket version.

        // If the versions do not match, the the checkout workflow is essentially reset - meaning
        // you have to start the checkout process all over
        Basket.Save();
      }
      return RedirectToUmbracoPage(BasketContentId);
    }

    ///
    /// Removes an item from the basket
    ///
    [HttpGet]
    publicActionResult RemoveItemFromBasket(Guid lineItemKey)
    {
      if (Basket.Items.FirstOrDefault(x => x.Key == lineItemKey) == null)
      {
        var exception = newInvalidOperationException("Attempt to delete an item from a basket that does not match the CurrentUser");

        LogHelper.Error("RemoveItemFromBasket failed.", exception);

        throw exception;
      }

      // remove product 
      Basket.RemoveItem(lineItemKey);

      Basket.Save();

      return RedirectToUmbracoPage(BasketContentId);
    }

    privateBasketViewModel GetBasketViewModel()
    {

      // ToBasketViewModel is an extension that
      // translates the IBasket to a local view model which
      // can be submitted via a form.
      var basketViewModel = Basket.ToBasketViewModel();

      // grab customer id - for example only
      // regardless if anon or known customer
      // stored in merchAnonymousCustomer table
      // or merchCustomer table
      if (CurrentCustomer.IsAnonymous)
      {
        basketViewModel.Customer = "Anonymous Customer";
      }
      else
      {
        basketViewModel.Customer = "Friend";
      }
      return basketViewModel;
    }

    [HttpGet]
    publicActionResult DisplayCustomerBasket()
    {
      return PartialView("Customer", GetBasketViewModel());
    }
	  }
	}

### Test the Web site

At this point, the product list should be able to add products to the basket. The basket should display products along with providing Update Item and Remove Item functionality. You should now be able to allow customers to create baskets on your website.

### Create Customer and Basket Summary Partial View

Let's build a customer partial view to display the anonymous customer as well as display the basket item count and total value. This partial view can be display at the top of the content pages. The example below shows this partial view at the top of the product list.

!( browser-product-list-with-customer-basket-at-top.png)

#### Customer.cshtml

	@inherits Merchello.Web.Mvc.MerchelloViewPage
	@using Controllers
	@using Models
	
	<h2>@Model.Customer | @Model.Items.Length.ToString() items in basket= @Model.TotalPrice.ToString("C")</h2>
	
	@{
	  //these are hard coded with URLs for our store
	  conststring continueShoppingUrl = "/productlist/";
	
	  conststring checkoutStep1Url = "/checkout/";
	
	  conststring basket = "/basket/";
	}
	
	<a href="@continueShoppingUrl">Continue Shopping</a><br />
	<a href="@basket">Basket</a><br />
	<a href="@checkoutStep1Url">Checkout</a><br />
	<hr/>
	


Modify the product list template to use this customer partial view at the top.

#### PageList.cshtml

	@inherits Merchello.Web.Mvc.MerchelloTemplatePage
	@using Models;
	@using Merchello.Web
	@using Merchello.Web.Models.ContentEditing
	@{
	    Layout = null;
	}
	
	<!-- show customer name and basket item count -->
	@Html.Action("DisplayCustomerBasket", "Basket", new { area = "MerchelloProductListExample"})
	
	<h1>@CurrentPage.Name</h1>
	
	<table>
	    @*    for this page, get all visible children in Umbraco content tree *@
	    @foreach (IPublishedContent contentProduct in CurrentPage.Children.Where("Visible"))
	    {
	        //@GetProductInfo(contentProduct)
	        
	        // info from Umbraco
	        var contentidForChildPage = contentProduct.Id;
	        var productname = contentProduct.Name;
	        var productdescription = contentProduct.GetPropertyValue("productdescription").ToString();
	        var merchelloproductguid = contentProduct.GetPropertyValue("merchelloproduct").ToString();
	
	    // merchello object
	    var merchello = new MerchelloHelper();
	    
	    // info from Merchello
	    var merchelloproductobject = merchello.Query.Product.GetByKey(new Guid(merchelloproductguid));
	    var productprice = merchelloproductobject.Price.ToString("C");
	
	    // build model to hand to controller used in BuyButton.cshtml
	    var productitemmodel = new AddItemModel()
	    {
	        ProductKey = new Guid(merchelloproductguid),
	        ContentId = contentidForChildPage
	    };            
	                
	    <tr>
	            <!-- <td>@contentidForChildPage</td> -->
	            <!-- <td>@merchelloproductguid</td> -->
	            <td>@contentProduct.Name</td>
	            <td>@productdescription</td>
	            <td>@productprice</td>
	                    
	            <td>@Html.Action("Display_BuyButton", "Basket", new { area = "MerchelloProductListExample", product = productitemmodel })</td>
	    </tr>        
	    }
	</table>


## Example Website Available for Download

For your convenience, [you can download a zip file containing this entire example completed](Merchello-Build-The-Basket-Example.zip).

## Summary

In this example, we created a new Umbraco 7.x website using the txt starter kit, installed the Merchello 1.4 package, then created an Umbraco content ProductList. You can add a product to the basket and see the basket. Basket management includes changing quantities and removing products. You should now be able to build your own basket system with Merchello.
