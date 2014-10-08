---
layout: default
title: Basket Controller
menu: BasketController
section: Designer
level: 2
permalink: "basket-controller.html"
---
This MVC controller is responsible for managing the form POST and GET behaviors which accompany any standard Umbraco page.   It is not part of Merchello, but is key to maintaining an organized approach to interacting with your Merchello data.

What we're going to do here is show you the relevant portions of a working controller as it pertains to Merchello.   Believe me, you're going to have more in your MVC controller class.  But this is the stuff you must include to offer specific Merchello features in Umbraco pages.
 
Let's start with the exciting part first, assembly references!

	using Merchello.Core;
	using Merchello.Core.Models;
	using Merchello.Example.Umbraco.Models;
	using Merchello.Web;
	using Merchello.Web.Models.ContentEditing;
	using Merchello.Web.Workflow;

As you can see, there are several namespaces involved with the Merchello project.  Each namespace is explained in greater detail over in the [API](/api/) section of this documentation.

Let's cook some code.  For starters, you're going to need some methods to handle the common tasks involved with a product page.  Here is each task and a snippet of code to use in your controller.  We'll go through this code section by section, explaining the details along the way.

## Adding Item To Basket ##

First we have to define our MVC **ActionResult**.  In this example, we're using a view model that includes a few properties needed for MVC postbacks.  Click the link in the sidebar menu if you want to see how the **AddItemModel** is defined.

    [HttpPost]
    public ActionResult AddToBasket(AddItemModel model)
    {

Easy.   With that out of the way, let's get to our first little tip.

        var extendedData = new ExtendedDataCollection();
        extendedData.SetValue("umbracoContentId", model.ContentId.ToString(CultureInfo.InvariantCulture));

**ExtendedData** is a serializable collection of key-dictionary pairs.   It's perfect for storing custom values with your basket item.  In the case above, we've initialized a **ExtendedData** collection and added one custom field with a name of 'umbracoContentId'.  We set the value of this custom field to the contentId of current Umbraco **ContentNodeId**.

What we're doing is storing the Umbraco page (aka product) that added the current basket item to the basket.   Why you may ask?  This lets the basket page know which Umbraco node represents this particular item in the basket.  Since the original Umbraco content node Id is stored with this basket item, now the basket page can easily identify which product (Umbraco page) associates with each basket item.  The basket page will be able to easily render product thumbnails and hyperlinks as necessary.

Next up, let's find the Merchello product for the given Umbraco page (product):
        
        var product = _merchelloContext.Services.ProductService.GetByKey(model.ProductKey);

Every Merchello product has a unique GUID value.  To connect an Umbraco page to a Merchello product, you assign the Merchello GUID as the value in a document type property within Umbraco.  Once that is done, now we can pull that value in (via the view model) and locate the actual Merchello product.

Once we the Merchello product, _we need to do some important checks_ when it comes to product options.  Product options are choices that can be made regarding a specific product.  The most common example is the T-Shirt with it's 3 sizes of Small, Medium and Large.  The "Size" of the shirt would be the option.  "Small", "Medium", "Large" would be the choices for the "Size" option and (you guessed it) are known as "option choices".  

During product setup in Merchello, these options and choices are constructed into separate product records known as variants.   Each variant represents one unique combination of all possible option choices.   That's pretty easy with a Shirt that has only three sizes.   But what if the shirt has five colors?  Now you've got to have 3 size option choices for every color option.  Things can get complicated quickly.

        if (model.OptionChoices != null && model.OptionChoices.Any())
        {
            var variant = _merchelloContext.Services.ProductVariantService.GetProductVariantWithAttributes(product, model.OptionChoices);
            
            var name = variant.Name;
            
            _basket.AddItem(variant, name, model.Quantity, extendedData);
        }

Look closely and you'll see that we first check if the current product has options.  This is important because we want the specific product variant added to the basket, not just the general product.  That way, a "blue" T-Shirt will be treated as a separate from a "Red" T-Shirts.  In order to accomplish this, we ask the **ProductVariantService** to give us the exact product variant record based on the option choices that were made.  Then we just pull in the name and call **AddItem()** passing it all the details including the extended data.

But what if the product had no options?

        else
        {
            _basket.AddItem(product, product.Name, model.Quantity, extendedData);
        }
Well then it's painfully simple.  Call the same (overloaded) **AddItem** method to add your item to the current basket.

        _basket.Save();

Don't forget, adding an item to the basket **does not save the basket**.  You must explicitely call **.Save()** to persist your basket changes to the database.

        return RedirectToUmbracoPage(BasketContentId);
    }

Finally, we use a shortcut trick to redirect to the content node ID that represents the store basket page.  You'll see these defined in the full controller code located in the sidebar menu.  









 

