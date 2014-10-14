---
layout: default
title: AddItemModel
menu: Code - AddItemModel
section: Designer
level: 2
---
	using System;
	using System.ComponentModel.DataAnnotations;
	using Merchello.Web;
	using Merchello.Web.Models.ContentEditing;
	
	namespace Merchello.Example.Umbraco.Models
	{
	    /// <summary>
	    /// Simple Model for the Add To Cart form.
	    /// </summary>
	    public class AddItemModel
	    {
	
	        /// <summary>
	        /// Content Id of the ProductDetail page
	        /// </summary>
	        [Required]
	        public int ContentId { get; set; }
	
	        /// <summary>
	        /// The Product Key (pk) of the product to be added to the cart.
	        /// </summary>
	        /// <remarks>
	        /// 
	        /// NOTE : This is not the ProductVariantKey. The variant will be figured out
	        /// by the product key and the array of Guids (OptionChoices).  These define the ProductVariant 
	        /// 
	        /// </remarks>
	        public Guid ProductKey { get; set; }
	
	        /// <summary>
	        /// The Quantity to be added to a cart
	        /// </summary>
	        [Range(0, 999999, ErrorMessage = "Quantity must be a valid number")]
	        [Required(ErrorMessage = "Quantity is required")]
	        public int Quantity { get; set; }
	
	        /// <summary>
	        /// The option choices (if there are any), used to determine the variant 
	        /// </summary>
	        public Guid[] OptionChoices { get; set; }
	    }
	}