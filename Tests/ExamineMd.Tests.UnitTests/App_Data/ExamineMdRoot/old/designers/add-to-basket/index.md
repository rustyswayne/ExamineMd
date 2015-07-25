---
layout: default
title: Add To Basket Example
menu: Add To Basket
section: Designer
level: 2
permalink: "index.html"
---
The very first Merchello-related task you will have to accomplish in your store is the add-to-basket behavior.  The add-to-basket behavior provides a way for the current shopper to gather desired store items into a single collection unique to them.  

This collection of items is known as the "basket".  The connection between the shopper and his/her basket is persisted via a browser cookie.  The cookie does not have a default expiration.  However shopper baskets are purged after a predetermined period of inactivity.  This expired basket time limit can be configured via the **Merchello Settings** page.

Merchello will automatically handle keeping the basket separate for each shopper.  You don't even have to worry about loading anything beforehand.  From a design standpoint, just tell Merchello what (and how much) you want to add to the current shopper basket.  Merchello handles the rest.

But there is more effort involved when it comes to the UI aspect of add-to-basket behavior.  How do you handle multiple quantities?  What about product with options?  To drive an effective product page, your partial view must also accommodate for these scenarios.

Below you will find a complete walk-through of a fully functional add-to-cart partial view.  This view accommodates for both regular products and products with options.  It also allows for multiple quantities to be added at one time.  We will break the code up into sections and explain each one as we go.  If you're more of the copy/paste/pray designer, use the link on the right sidebar menu to view the entire partial view as a single code example. 
 
Let's start with the includes:

	@inherits Merchello.Web.Mvc.MerchelloViewPage<AddItemModel>
	@using Merchello.Example.Umbraco.Controllers
	@using Merchello.Example.Umbraco.Models

Notice we're not inheriting **UmbracoViewPage**?  We've built our own **MerchelloViewPage** class that inherits from **UmbracoViewPage**.  This exposes several Merchello-specific classes and methods that are necessary when accessing Merchello data within an MVC view.  Since we're inheriting **UmbracoViewPage**, everything you are used to having available with the **UmbracoViewPage** is still accessible.

Also notice our model **AddItemModel**.  It consists of a few strongly typed properties we need to handle passing form data.  You can find the full class definition by clicking the link in the side menu.  

The remaining references simply help wire up our MVC controllers and models.

Next, let's take a look at loading a Merchello product:  

	@{
	
	    var product = Merchello.Product(Model.ProductKey);
	    

It's pretty simple, right?  Note how the **ProductKey** property of the view Model is passed to **Merchello.Product()**.   Every product in Merchello has a unique GUID value.  You "connect" a Merchello product to an Umbraco content node by associating that GUID in a property of the document type used for your product content nodes.  You can examine this further by looking at the document types found in the [Merchello Starter Kit](/starter-kit/).
  
Now that the product is loaded, we can proceed with a nice Umbraco form.  This form is going to handle several things for us.  We'll start with defining the form:
	    
	    using(Html.BeginUmbracoForm<BasketController>("AddToBasket", Model))
	    {

That was easy enough.  Notice how we've wired up this form to a specific **BasketController** class.   Find the specifics about this MVC controller by clicking the link in the sidebar menu.

Now we have to make a decision.  Merchello products can be a single product, or a product which has options.  So the next step in rendering a product is deciding whether any product options should be rendered.   This is quickly accomplished with the following code: 

	        if (product.ProductOptions.Any())
	        {
	            var index = 0;
	            foreach (var option in product.ProductOptions)
	            {
	                var choices = option.Choices.OrderBy(x => x.SortOrder).Select(choice => new SelectListItem()
	                    {
	                        Value = choice.Key.ToString(), Text = choice.Name
	                    }).ToList();
	                
	                <span class="help-block"><strong>@option.Name</strong></span>
	                @Html.DropDownListFor(x => x.OptionChoices[index], choices)
	
	                index = index + 1;
	            }
	        }
	       
Let's walk through what we've done above.  First, we look to see if the product we loaded has any options.  If it does, we know those options have to be rendered to the shopper.  The options are rendered by a simple foreach() loop of **product.ProductOptions**.  Each iteration of this loop represents one option assigned to the product.  For example, let's say you have a product with options for Size, Color and Style.  This loop would run three times, once for each option.

Within the loop, we pull in the choices of the current option being rendered.  So if we're on the Size option, we might have choices "Small", "Medium" and "Large".  We load the choices variable with the options and sort them in one convenient line.
 
Finally, we let MVC define the dropdown and bind it to option choices.

Whew, that wasn't so bad.   It does take more work when dealing with products with options.  But as you can see, it all makes sense and works well with only a few lines of code.

Up next is our Quantity field.  Most shopping experiences offer the shopper a way to buy multiple quantities with a single click.  To accomplish this, just render a standard MVC text box.  Note that a user input for quantity isn't required.  You can always just hard-code a specific quantity in your controller.
	          
	        <span class="help-block"><strong>Qty</strong></span>
	        @Html.TextBoxFor(x => x.Quantity, new { @class = "span2", @style="display: block;" })

Finally, we load a few hidden fields with values we're going to need in the view model during postback: 

	        @Html.HiddenFor(x => x.ContentId)
	        @Html.HiddenFor(x => x.ProductKey)        
	        <input type="submit" class="btn btn-large btn-info" value="Add to cart"/>
	    }
	}  

Specifically, we want to record the **ContentId** of the current Umbraco page being rendered.  We also want to record the **ProductKey** of the current content node.  That way we know what Merchello product is linked to the displayed Umbraco content node.

And that's it!  You've built a fully functional partial view that provides all the necessary information to handle add-to-cart behavior.  Next, go check out the **Basket Controller** for how this partial view is wired up to server-side code.