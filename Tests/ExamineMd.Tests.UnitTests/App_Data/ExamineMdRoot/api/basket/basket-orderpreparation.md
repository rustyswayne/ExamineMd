---
layout: default
title: Basket.OrderPreparation
menu: Basket.OrderPreparation
section: API
level: 2
---
##Description##
The process of "checkout" is defined by a series of steps that eventually create an invoice record.  Each step of the checkout process is designed to apply information to the basket that is required for the invoice to be usable.  By using a clearly-defined sequence of steps, the checkout process becomes quite logical in design.

In Merchello, we have organized all of the checkout steps into a class called Basket.OrderPreparation.  Everything you need to successfully generate an invoice from a basket can be found in this class.

###Step 1 - Billing Address###
Every invoice must be associated with the owner responsible for it.  A Merchello invoice has a designated set of fields known as the **Bill To** address fields.  The Bill To fields are used to associate the invoice with the invoice owner.

Every invoice must have an owner, so you must supply a populated IAddress object to the basket like so:

    var destination = new Address()
        {
            Name = "Mindfly Web Design Studio",
            Address1 = "115 W. Magnolia St.",
            Address2 = "Suite 504",
            Locality = "Bellingham",
            Region = "WA",
            PostalCode = "98225",
            CountryCode = "US"
        };

    // Assign customer billing address to the basket
    CurrentCustomer.Basket().OrderPreparation().SaveBillToAddress(destination);

###Step 2 - Package Into Shipments ###
This step is only required if the basket contains shippable items.  The purpose of this step is to group line items by the warehouse they belong to.   This organizes the line items so that shipping and taxation can be applied accurately.  You wouldn't want to charge New York tax on an order that shipped out of the Chicago warehouse.
 
Initiating this step is simple:

    // Package the shipments 
    // This will return a collection containing a single shipment
    var shipments = CurrentCustomer.Basket().PackageBasket(destination).ToArray();

What's happened is a [IShipment](/api/interfaces/ishipment/) record has now been built and stored to the basket.  And each line item in the basket is now assigned to the IShipment record.  This effectively groups your line items by warehouse and makes it possible to accurately calculate shipping and taxes.

###Step 3 - Choose a Ship Method ###
Now that a shipment record has been built, we need to calculate the available shipping methods.  I say "available" because shipping methods have rules.  And those rules may allow or disqualify specific methods from being valid for the shipment and it's line items.

That means we have to ask Merchello what shipping methods are possible.  This is very easy to by calling ShipmentRateQuotes() method on the shipment class like this:

	var shipRateQuotes = shipment.ShipmentRateQuotes().ToArray();

Boy wasn't that convenient?  With one line of code we now have a convenient collection of available shipping methods and their respective shipping rates.  Now we just need to set the desired shipping method to the shipment. 

To tell Merchello which shipping method has been chosen, simply call:

    // Choose the cheapest shipping rate since the collection is returned 
	// in order of least expensive to most expensive shipping method
    var approvedShipRateQuote = shipRateQuotes.FirstOrDefault();

    CurrentCustomer.Basket().OrderPreparation().SaveShipmentRateQuote(approvedShipRateQuote);

Merchello will automatically apply the designated shipping method to the basket shipment and the shipping charge to the basket line items.

###Step 4 - Generate Invoice###
Ah, the final step.   When everything is set in the basket, you are ready to make the magic happen.  And it takes just one line of code:

    var invoice = CurrentCustomer.Basket().OrderPreparation().GenerateInvoice();

There are a number of tasks accomplished by this simple command.  Merchello processes the basket to create an invoice object.  You now have a memory-only version of the invoice.  You are ready to apply any necessary payment processing commands before completing the final checkout process. 

Continue reading to learn more about the inner workings of the Merchello checkout process.

## Minimum Order Preparation Classes##
- [SaveBillToAddress](/api/basket/basket-orderpreparation-savebilltoaddress/)
- [SaveShipToAddress](/api/basket/basket-orderpreparation-saveshiptoaddress/)
- [SaveShipmentRateQuote]()
- [GenerateInvoice]()


## Additional Sub Classes##
- GetBillToAddress
- GetShipToAddress
- ItemCache
- RestartCheckout
 