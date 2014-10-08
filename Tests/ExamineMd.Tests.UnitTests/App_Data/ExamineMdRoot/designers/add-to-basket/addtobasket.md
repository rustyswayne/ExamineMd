---
layout: default
title: Add To Basket Partial View
menu: Code - AddToBasket
section: Designer
level: 2
---


	@inherits Merchello.Web.Mvc.MerchelloViewPage<AddItemModel>
	@using Merchello.Core
	@using Merchello.Example.Umbraco.Controllers
	@using Merchello.Example.Umbraco.Models
	@{
	
	    var product = Merchello.Product(Model.ProductKey);
	    
	    
	    using(Html.BeginUmbracoForm<BasketController>("AddToBasket", Model))
	    {
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
	       
	          
	        <span class="help-block"><strong>Qty</strong></span>
	        @Html.TextBoxFor(x => x.Quantity, new { @class = "span2", @style="display: block;" })
	        @Html.HiddenFor(x => x.ContentId)
	        @Html.HiddenFor(x => x.ProductKey)        
	        <input type="submit" class="btn btn-large btn-info" value="Add to cart"/>
	    }
	}