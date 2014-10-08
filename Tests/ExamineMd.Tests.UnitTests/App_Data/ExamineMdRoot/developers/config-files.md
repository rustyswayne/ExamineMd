---
layout: default
title: Config Files
section: Developer
---

The Merchello config file is a standard XML file which contains several core settings and values that drive the entire Merchello experience.   We will document each of these configuration settings below and include any important notes as required.

###Location###
The config file can be found in two different locations:

1.	In the repository source code, it is found in the **/src/Merchello.Web.UI/App_Plugins/Merchello/Config/** folder.
2.	In the Umbraco package, it is found in **/App_Plugins/Merchello/Config/**

The file name will always be **Merchello.config**

##&lt;merchello&gt; Section##

**Attribute "version":**  represents the application version currently installed in the Umbraco installation.   Must exactly match the version of the Merchello.Core.DLL file found in the /bin/ folder.   When this version value differs from the DLL file, it will trigger an automatic version update immediately upon application start-up.

```
<merchello version="0.9.4">
```

##&lt;settings&gt; Section##
Each setting in the settings section requires it's own XML tag.  The Alias must be a unique alphanumeric value and match what is defined in the Merchello programming.  

When changing a strategy setting, be sure to only specify one for each Alias.  Additional strategy settings for the same Alias will be ignored.

The following settings available:

**DefaultBasketPackagingStrategy**: represents the Merchello class to be called when the shopper basket is packaged in preparation for checkout.  The attribute **value** should be specified in the format of "classname, namespace"

    <setting alias="DefaultBasketPackagingStrategy" value="Merchello.Web.Shipping.DefaultWarehousePackagingStrategy, Merchello.Web"  />

**DefaultSkuSeparator**: Determines what ASCII character is used as a separator value in the SKU field for product variants.  For example, assume a shirt with SKU tshirt having Red, Blue, Green choices.  When variants are generated, the variant SKUs would be "tshirt-1","tshirt-2","tshirt-3".   The separator character can be configured here if something other than a dash is desired.  

	<setting alias="DefaultSkuSeparator" value="-" />

##&lt;typeFieldDefinitions /&gt; Section##
Reserved for future implementation

##&lt;regionalProvinces /&gt; Section##
This section defines what regions and provinces will be populated in the Merchello backend.  A region is defined as a single XML element that contains child elements for every province within that region.

Attribute **code**:  represents the ISO country code for that region.
  
Attribute **requirePostalCode**: can be either "True" or "False".  Assumed to be "True" when not specified.   Used by shipping estimator routines to determine if Postal Code should be required input before attempting a rate estimate.  Not all countries or shipping carriers require a postal code. 

Each individual province must be specified with the attributes **name** and **code**.   

        <region code="US">
            <provinces>
                <province name="Alabama" code="AL" />
                <province name="Alaska" code="AK" />
                <province name="Arizona" code="AZ" />
            </provinces>
        </region>

        <region code="CA" requirePostalCode="True">
            <provinces>
                <province name="Alberta" code="AB" />
                <province name="British Columbia" code="BC" />
                <province name="Manitoba" code="MB" />
            </provinces>
        </region>

