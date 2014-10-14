# How to make a Product List page with Buy buttons in Merchello, Part 1

## Introduction

In this example, we will create a product list page with "Add to Basket" buttons for each product. When the "Add to Basket" button is pressed, the basket page will display.

### Part 1 focuses on the Product List and Buy Button

This is a two-part example. The first part will focus on the product list, the "Add to Basket" button, and the files that support these web pages.

### Part 2 focuses on the Basket

The second part of the example will focus on the basket page. It is important to note that the code to add a product to the logical basket is in the second article. For this article, an empty basket page will be shown.

The Umbraco version is 7.1.6 and the Merchello version is 1.4.

## Installation

### Install Umbraco

Download Umbraco 7.1.6 (UmbracoCms.7.1.6.zip) from [http://our.umbraco.org/contribute/releases/716](http://our.umbraco.org/contribute/releases/716). Expand the zip file. Open IIS Manager and add a new website called MerchelloProductListPart1. Create a new .Net v4.0 application pool for the website. Make sure the website started.

In your host file, add a hostname as MerchelloProductListPart1.

127.0.0.1MerchelloProductListPart1

Save the host file and open a browser to [http://MerchelloProductListPart1/](http://MerchelloProductListPart1/). When the web page comes up, the "Install Umbraco 7" box will show.

!(umbraco-begin-install.png)

Enter the name, email, and password. I'll use admin, [admin@admin.com](mailto:admin@admin.com), and 1234.

>Name:admin
>
>Email:  [admin@admin.com](mailto:admin@admin.com)
>
>Password: 1234

Press the **install** button. When the web-based installation of Umbraco is done, you will see the black login screen.

!(umbraco-logon.png)

Open a second tab in the browser to view the live content. Make sure the txt starter kit is loaded at http://MerchelloSingleProductExample/. Keep this second tab open. We will move between the Umbraco backend and the content frontend to verify results later in this example.

!(umbraco-text-starter-kit.png)

### Install Merchello

The next step is to get Merchello. Download Merchello from [http://our.umbraco.org/projects/collaboration/merchello](http://our.umbraco.org/projects/collaboration/merchello). For this sample, I downloaded Merchello 1.4.0. The file name is Merchello\_1.4.0.zip. Don't expand the zip file.

Logon to the Umbraco backend and go to the Developer section. In the Developer/Packages section, select **Install local package** .

!(umbraco-backend-install-package.png)

Click the **Accept License** check box and select **Install Package** for the Merchello file you just downloaded.

!(umbraco-package-installed-successfully.png)

When the installation is done, you will see the "Package is Installed" screen.

!(umbraco-merchello-icon.png)

Notice on the left-side black bar, there is now a Merchello icon toward the bottom.

!(merchello-icon.png)

Umbraco and Merchello are now successfully installed. We can move on to configure Umbraco and Merchello.

## Merchello Configuration

Merchello configuration is done in the Umbraco backend. You can control the product catalog, inventory, shipping, payment, taxation, and notifications.

### Configure Merchello Global Product Properties

Merchello configuration includes some general settings for the product catalog such as currency, date format, and taxability.

Before we begin configuring Merchello for this specific example, we should set a few global properties for Merchello products. This will save time by NOT having to set the properties for each product as it is added to the catalog.

Go to Merchello/Settings. Select any property that applies to MOST of your products:

- All items are taxable
- All items are shippable
- Track Inventory for all items and variants
- Shipping charges are taxable

You can uncheck the individual settings on each product as it is entered into the catalog, or at any time after it is entered.

!(merchello-settings.png)

You also have more configuration choices inside of the shipping, taxation, payment, notification, and gateway provider categories. This example won't change any of those settings.

### Add Merchello Data Types to Umbraco

The next step of the Merchello example is to connect the Merchello data type in the Developer/Data Types section so it can be used in an Umbraco/Settings/Document Type as a property. Merchello has 2 properties: product selector and product editor. This example will only use the product selector.

#### Add Product Selector Data Type

In the Umbraco/Settings/Data Types, create a new Data Type called Merchello Product Selector. Choose the Merchello Product Selector as the Property editor. Umbraco will create the alias of Merchello.ProductSelector. Save the Data Type.

!(umbraco-datatypes-merchello-product-selector.png)

## Product Catalog Configuration

### Add 3 Products to Merchello Product Catalog

The next step is to add products to the Merchello Product catalog.

In the Merchello/Catalog section, create a new product called Merchello Boots, with a price of 10, and a base sku of BootsSku. These are the required properties on a product. Save the product.

!(merchello-create-product-boots.png)

Save the product then select the catalog again and see it now has one product.

!(merchello-product-catalog-boots-only.png)

Add two more products:

| Product | Price| Sku |
| --- | --- | --- |
| Merchello Hat | $5 | HatsSku |
| Merchello Coat | $20 | CoatsSku |

**Note:** The purpose of adding "Merchello" to the product name is to understand when and how the Name field is the Merchello product name or the Umbraco Content page name. This will be more apparent in part 2 of this example.

It is important to note that you probably have a lot more information about your products that you want to keep in the system. Usually, Merchello holds the merchandising information needed for shipping and fulfillment only. All descriptive information (including images) is kept in Umbraco in the Content section. Alternatively, you can pull that information in from another source via APIs in the code pages.

#### Extended Data

There may be circumstances when a basket item needs additional information that isn't necessarily part of the content system (Umbraco) or part of the merchant system (Merchello). You can use the basket item extended data to add additional information that is kept with the item as it moves through the purchasing system.

An example of product extended data is to add the Umbraco content id as an extended data of the product.

#### Extended data versus Product Options

Extended data and product variations are not the same thing. Extended data is custom data you want to track – such as the Umbraco content id. Product variations such as size and color are used to determine the total product variation which becomes the product itself. For example, the following code determines the product from the product variations and adds that total product to the basket (altered code from BasketController). Notice that extended data is add with the product, regardless if the product has options or not.

	public ActionResult AddToBasket(AddItemModel model)
	{
	  // This is an example of using the ExtendedDataCollection to add some custom functionality.
	  // In this case, we are creating a direct reference to the content (Product Detail Page) so
	  // that we can provide a link, thumbnail and description in the cart per this design. In other
	  // designs, there may not be thumbnails or descriptions and the link could be to a completely
	  // different website.
	
	  var extendedData = new ExtendedDataCollection();
	
	  extendedData.SetValue("umbracoContentId", model.ContentId.ToString(CultureInfo.InvariantCulture));
	
	  var product = Services.ProductService.GetByKey(model.ProductKey);
	
	  // In the event the product has options we want to add the "variant" to the basket.
	  // -- If a product that has variants is defined, the FIRST variant will be added to the cart.
	  // -- This was done so that we did not have to throw an error since the Master variant is no
	  // -- longer valid for sale.
	  if (model.OptionChoices != null && model.OptionChoices.Any())
	  {       
	    var variant = Services.ProductVariantService.GetProductVariantWithAttributes(product, model.OptionChoices);
	
	    Basket.AddItem(variant, variant.Name, 1, extendedData);
	  }
	  else
	  {
	    Basket.AddItem(product, product.Name, 1, extendedData);
	  }
	
	  Basket.Save();
	
	  return RedirectToUmbracoPage(BasketContentId);
	}

At this point, Umbraco is working (both the content website and the Umbraco backend). You have 3 products in the catalog and Merchello is configured. Now it is time to build out the product list page.

## Process to Build a Product List with Buy Button webpage

The first step to build out the product list page is the Umbraco back end: document types, templates, and content pages. The next part of this example is to add the models, controllers, and partial views to flush out the product list and "Add to Basket" button. The finished example will display the list of products (boots, coat, hat) with a "Add to Basket" button for each item. The "Add to Basket" button, when pressed while redirect to the basket. 

!(browser-productlist-with-buttons.png)

### Create the Umbraco back end pages

#### Document Types

##### Create 3 Document Types: ProductList, Product, and Basket 

The ProductList, Product, Basket will each have a corresponding document type, template, and content pages. Begin by creating document types for each. Each should create their own template. On the Structure tab, check "Allow at root." At this point, they do not need any additional properties, or other settings.

ProductList Document Type

!( umbraco-doctype-productlist.png)

Basket Document Type

!( umbraco-doctype-basket.png)

Product Document Type

!( umbraco-doctype-product.png)

##### Modify the ProductList Document Type

Open the ProductList document type. On the Structure tab, check that the Product document type is an "Allowed child node types."

!( umbraco-modify-productlist-doctype.png

##### Modify the Product Document Type

Open the Product document type. Add a Merchant tab and two properties to that tab. The first property should be named MerchelloProduct with a type (data type) of Merchello Product Selector. The second property is the ProductDescription with a type of textstring. Both properties should be on the Merchant tab.

!( umbraco-modify-product-doctype.png)

#### Templates

##### Modify Templates to show Page name

On the ProductList, Product and Basket templates, modify the template to display the Umbraco page name.

```
	<h1>@CurrentPage.Name</h1>
```

The template doesn't need any other content right now. We will change the templates later.

#### Content Pages

##### Create the Content Pages for ProductList, and Basket

In the /Umbraco/Content, create the ProductList, and Basket content pages.

##### Create 3 Content Pages under ProductList for Boots, Hat, and Coat

Create 3 new content pages under the ProductList content page of document type Product. Each content page only needs the page name/title, MerchelloProduct, and ProductDescription properties set.

>| Content Name |
>| --- |
>| Umbraco Boots |
>| Umbraco Hat |
>| Umbraco Coat |

##### Selecting the MerchelloProduct Property

The Product content page should show you a Merchello Product Selector. It appears on the Merchant tab shown below. Select the green "Select Product…" button to choose a product from the Merchello Product Catalog.

!( umbraco-select-merchello-product-property-for-content-page.png)

The webpage will change to show the product list as a fly-in window from the right side.

!( umbraco-select-merchello-product-property-for-content-page-fly-in.png)

Select the product. The fly-in window will disappear and the content page will now show the selected product.

Add a description for each of the 3 products. Save and publish each of the 3 product content pages: Umbraco Boots, Umbraco Hats, Umbraco Coats.

Your /Umbraco/Content tree should look like the picture below for ProductList, Boots, Hats, and Coats.

!( umbraco-content-tree.png)

Notice that the Umbraco content system is managing the relationship of which products are on the product list. The content page controls which Merchello product is displayed. Currently, the product template only shows the page name so nothing else is going to be shown at this point.

#### Test the Content Pages

At the point, the Umbraco backend should be correctly configured to return the /productlist and /basket pages. Both should be empty except to display the page name.

/productlist

!( browser-empty-product-list.png)

/basket

!( browser-empty-basket.png)

#### The Umbraco-Merchello Relationship is now one-sided

The three product pages will look just like these except the title will be Umbraco Boots, Umbraco Hats, and Umbraco Coats. Selecting the Merchello product associated with the content page did NOT put any product information on the page. It did NOT add anything to the template or change the Umbraco content system in any way.

All we have done is build a parent-child relationship between ProductList and Boots, Hat, Coat in the Umbraco content system. This relationship doesn't exist in the Merchello system.

The URLs for these five pages should look like:

>http://localhost/productlist

>http://localhost/basket
	
>http://localhost/umbraco-boots
	
>http://localhost/umbraco-coats
	
>http://localhost/umbraco-hat

### Add Models, Controllers, and Partial Views

Now that the web site is working (although the pages are empty), we need to add the product list and "Add to Basket" buttons to the ProductList template. When the "Add to Basket" button on the ProductList page is selected, the product information is passed to the BasketController in the AddItemModel.

#### In Part 1 of this Example, the Basket is not functional

In this first part of the example, the basket controller will simply redirect to the Basket page. The second part of this example will modify the Basket controller to add the product to the logical Merchello Basket then redirect to a basket page showing the product item.

#### Visual Studio Files

By the end of part 1 of this example, the website will contain several new files:

| File | Purpose |
| --- | --- |
| /app\_code/Controllers/Basket.cs | From MerchelloSurfaceController |
| /app\_code/Controllers/MerchelloSurfaceController.cs | From Umbraco.Web.Mcv.SurfaceController |
| /app\_code/Models/AddItemModel.cs | Relationship between Merchello product and Umbraco content page |
| /Views/Partials/BuyButton.cshtml | Product-specific "Add to Basket" button |

##### Create /app\_code/Controllers folders

Open the website in Visual Studio. In the /app\_code folder, create a new folder called Controllers.
 
In the /app\_code/Controllers folder, create BasketController.cs and MerchelloSurfaceController.cs. The basket controller is specific to this example. The Merchello SurfaceController can be used with other Merchello projects. It is beyond the scope of this example. Just add the MerchelloSurfaceController file as listed below and make sure the BasketController inherits from it.

###### /app\_code/MerchelloSurfaceController.cs 

	namespace Controllers
	{
	  using System;
	  using Merchello.Core;
	  using Merchello.Core.Gateways.Payment;
	  using Merchello.Core.Models;
	  using Merchello.Web;
	  using Merchello.Web.Workflow;
	  using Umbraco.Core.Logging;
	  using Umbraco.Web.Mvc;
	  using Merchello.Core.Services;
	  using Merchello.Core.Gateways.Shipping;
	
	  ///
	  ///
	  ///
	  ///
	  publicabstractclassMerchelloSurfaceContoller : SurfaceController
	  {
	
	    privatereadonlyIBasket _basket;
	
	    privatereadonlyIMerchelloContext _merchelloContext;
	
	    privatereadonlyICustomerBase _currentCustomer;
	
	    protected MerchelloSurfaceContoller(IMerchelloContext merchelloContext)
	    {
	      if (merchelloContext == null)
	      {
	
	        var ex = newArgumentNullException("merchelloContext");
	
	        LogHelper.Error("The MerchelloContext was null upon instantiating the CartController.", ex);
	
	        throw ex;
	      }
	
	      _merchelloContext = merchelloContext;
	
	      var customerContext = newCustomerContext(UmbracoContext); // UmbracoContext is from SurfaceController
	
	      _currentCustomer = customerContext.CurrentCustomer;
	
	      _basket = _currentCustomer.Basket();

	    }
	
	    ///
	    /// Gets the current customer.
	    ///
	    protectedICustomerBase CurrentCustomer
	    {
	      get { return _currentCustomer; }
	    }
	
	    ///
	    /// Gets the Basket for the CurrentCustomer
	    ///
	    protectedIBasket Basket
	    {
	      get { return _basket; }
	    }
	
	    ///
	    /// We are going to hide the Umbraco Service Context here so controller that sub class this controller are "Merchello Surface Controllers"
	    ///
	    protectednewIServiceContext Services
	    {
	      get { return _merchelloContext.Services; }
	    }
	
	    ///
	    /// Exposes the 
	    ///
	    protectedIPaymentContext Payment
	    {

	      get { return _merchelloContext.Gateways.Payment; }
	    }
	
	    ///
	    /// Exposes the 
	    ///
	    protectedIShippingContext Shipping
	    {
	      get { return _merchelloContext.Gateways.Shipping; }
	    }
	  }
	}

Since the MerchelloSurfaceController is a plugin-based controller, we will add the PluginController attribute to the Basket controller with the name "MerchelloProductListExample."

We also need the BuyButton partial view returned when the Display_BuyButton method is called.

###### /app_code/BasketController.cs

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
	    //
	    //
	    //
	    publicActionResult Display_BuyButton(AddItemModel product)
	    {
	      return PartialView("BuyButton", product);
	    }
	
	    [HttpPost]
	    publicActionResult AddToBasket(AddItemModel model)
	    {
	      return RedirectToUmbracoPage(BasketContentId);
	    }
	  }
	}

##### Create /app_code/Models folders

Still in Visual Studio, in the /app_code folder, create a new folder called Models. Add a new class file called AddItemModel.

###### AddItemModel.cs

In order to add a product to the basket, we have to capture the two main pieces of information about the product: the Umbraco content id, and the Merchello Product guid. If the product had options (known as variants in Merchello), we would also need to capture these. For this example, we can assume our products do not have options. We will create a model that contains this information of content id and product guid.

###### /app_code/AddItemModel.cs

	namespace Models
	{
	  using System;
	  using System.Collections.Generic;
	  using System.Linq;
	  using System.Web;
	
	  ///
	  /// Summary description for AddItemModel
	  ///
	  publicclassAddItemModel
	  {
	    ///
	    /// Content Id of the ProductDetail page
	    ///
	    publicint ContentId { get; set; }
	
	    ///
	    /// The Product Key (pk) of the product to be added to the cart.
	    ///
	    publicGuid ProductKey { get; set; }
	  }
	}

##### Create the BuyButton Partial View

In the Umbraco backend, go to /Umbraco/Settings/Partial Views. Create a new partial view named BuyButton.

It should be empty except for inheriting from the Umbraco.Web.Mvc.UmbracoTemplatePage.

##### Test the Web site

At this point, the Visual Studio files are created and shouldn't cause any runtime errors. Test this by viewing the [http://localhost/productlist](http://localhost/productlist) page.

#### Modify ProductList Template

The next change is to make [http://localhost/productlist](http://localhost/productlist) show the 3 child content nodes (boots, hat, coat) along with a "Add to Basket" button for each. This is done in the ProductList template. The code will get all visible children in the Umbraco content pages for the ProductList. For each one product, display a row of information and call BasketController to display the "Add to Basket" button inside an HTML form. Notice the PlugIn Surface Controller name from the BasketController is used in the Html.Action call as an area value.

##### /Views/ProductList.cshtml

	@inherits Merchello.Web.Mvc.MerchelloTemplatePage
	@using Models;
	@using Merchello.Web
	@using Merchello.Web.Models.ContentEditing
	
	@{
	  Layout = null;
	}
	
	@CurrentPage.Name
	
	  @*  for this page, get all visible children in Umbraco content tree *@
	
	  @foreach (IPublishedContent contentProduct in CurrentPage.Children.Where("Visible"))
	  {
	    //@GetProductInfo(contentProduct)
	
	    // info from Umbraco
	    var contentidForChildPage = contentProduct.Id;
	
	    var productname = contentProduct.Name;
	
	    var productdescription = contentProduct.GetPropertyValue("productdescription").ToString();
	
	    var merchelloproductguid = contentProduct.GetPropertyValue("merchelloproduct").ToString();
	
	    // merchello object
	    var merchello = newMerchelloHelper();
	
	    // info from Merchello
	    var merchelloproductobject = merchello.Query.Product.GetByKey(newGuid(merchelloproductguid));
	
	    var productprice = merchelloproductobject.Price.ToString("C");
	
	    // build model to hand to controller used in BuyButton.cshtml
	    var productitemmodel = newAddItemModel()
	
	    {
	      ProductKey = newGuid(merchelloproductguid),
	
	      ContentId = contentidForChildPage
	    };      
	
	  }

      @contentProduct.Name 

      @productdescription 

      @productprice 

      @Html.Action("Display_BuyButton", "Basket", new { area = "MerchelloProductListExample", product = productitemmodel }) 

#### Create BuyButton Partial View

Before we test the website again, we need to make the BuyButton partial page. On the Umbraco backend at /Umbraco/Settings/Partial Views, create BuyButton and add text to show the button inside a form.

##### /Views/Partial/BuyButton.cshtml

	@inherits Merchello.Web.Mvc.MerchelloViewPage
	@using Controllers
	@using Merchello.Web
	
	@{
	  // get Merchello object
	  var merchelloHelper = newMerchelloHelper();
	
	  // build Html form
	  using (Html.BeginUmbracoForm("AddToBasket", Model, new { role = "form" }))
	  {    
	    // Umbraco Content Id
	    @Html.HiddenFor(x => x.ContentId)
	
	    // Merchello Product Guid
	    @Html.HiddenFor(x => x.ProductKey)
	  }  
	}

### Test http://localhost/productlist

All of the files are connected in the website. Test the product list in the browser. Verify that the three child nodes of ProductList show with a "Add to Basket" button for each. If the website doesn't show the web page below, review the steps.

!(browser-productlist-with-buttons.png)

View the source of the page to verify that each Buy button form passed through the child node content id and the product guid.

!( browser-pagesource-shows-product-and-content-ids.png)

If the ProductListing page appears correctly, press one of the "Add to Basket" buttons. You should be taken to a page with the text "Basket" and nothing else. We will develop the basket in part 2 of this example.

!(browser-empty-basket.png)

## Example Website Available for Download

For your convenience, ![you can download a zip file containing this entire example completed](Build-Product-List-With-Buy-Button.zip).

## Summary

In this example, we created a new Umbraco 7.x website using the txt starter kit, installed the Merchello 1.4 package, then created an Umbraco content ProductList page that contains several child nodes. Each child node is a single product. The ProductList used a model to pass the Umbraco content id and the Merchello product guid through the Basket controller to the BuyButton partial view. At this point, you should be able to build your product and product category pages for your website.
