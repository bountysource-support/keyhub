﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using KeyHub.Data;

namespace KeyHub.Web.ViewModels.SKU
{
    /// <summary>
    /// Viewmodel for index list of SKUs
    /// </summary>
    public class SKUIndexViewModel : RedirectUrlModel
    {
        public SKUIndexViewModel() : base() { }

        /// <summary>
        /// Construct the viewmodel
        /// </summary>
        /// <param name="skuList">List of SKU entities</param>
        public SKUIndexViewModel(List<Model.SKU> skuList)
            : this()
        {
            SKUs = new List<SKUIndexViewItem>();
            foreach (Model.SKU entity in skuList)
            {
                SKUs.Add(new SKUIndexViewItem(entity, entity.Vendor, entity.PrivateKey, 
                    entity.SkuFeatures.OrderBy(x => x.Feature.FeatureName)));
            }
        }

        /// <summary>
        /// List of SKUs
        /// </summary>
        public List<SKUIndexViewItem> SKUs { get; set; }
    }

    /// <summary>
    /// SKUViewModel extension that includes Vendor name, Primary key name, summary of features
    /// </summary>
    public class SKUIndexViewItem : SKUViewModel
    {
        public SKUIndexViewItem(Model.SKU sku, Model.Vendor vendor, Model.PrivateKey privateKey, IEnumerable<Model.SkuFeature> skuFeatures)
            : base(sku)
        {
            VendorName = vendor.Name;
            PrivateKeyName = privateKey.DisplayName;
            FeatureSummary = skuFeatures.ToSummary(x => x.Feature.FeatureName, 3, ", ");
        }

        /// <summary>
        /// OrganisationName of the Vendor
        /// </summary>
        [DisplayName("Vendor")]
        public string VendorName { get; set; }

        /// <summary>
        /// DisplayName of the Private key
        /// </summary>
        [DisplayName("Private key")]
        public string PrivateKeyName { get; set; }

        /// <summary>
        /// Summary of assigned Features
        /// </summary>
        [DisplayName("Features")]
        public string FeatureSummary { get; set; }
    }
}