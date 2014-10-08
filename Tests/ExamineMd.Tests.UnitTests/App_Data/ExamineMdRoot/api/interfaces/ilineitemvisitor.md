---
layout: default
title: ILineItemVisitor
menu: ILineItemVisitor
section: API
level: 2
---
##Description##
Provides a standardized way to implement custom line item filtering routines that can be passed as a parameter to anything that accepts ILineItemVisitor.  This includes the Basket and LineItemCollection classes.

##Dependencies##
ILineItemVisitor

##Usage##

    /// <summary>
    /// Line item visitor intended to filter "Basket" items for shippable products
    /// </summary>
    public class ShippableProductVisitor : ILineItemVisitor
    {
        private readonly List<ILineItem> _lineItems = new List<ILineItem>();
      
        public void Visit(ILineItem lineItem)
        {
            if (lineItem.ExtendedData.ContainsProductVariantKey() && lineItem.ExtendedData.GetShippableValue() && lineItem.LineItemType == LineItemType.Product)
            {                
                _lineItems.Add(lineItem.AsLineItemOf<OrderLineItem>());
            }
        }

        public IEnumerable<ILineItem> ShippableItems 
        {
            get { return _lineItems; }
        } 
    }
