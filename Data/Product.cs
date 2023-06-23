using ChannelEngineConsoleApp.Data;
using System;

namespace ChannelEngineConsoleApp
{
    internal class Product {
        public string Status { get; set; }
        public bool IsFulfillmentByMarketplace { get; set; }
        public string Gtin { get; set; }
        public string Description { get; set; }
        public Stocklocation StockLocation { get; set; }
        public float UnitVat { get; set; }
        public float LineTotalInclVat { get; set; }
        public float LineVat { get; set; }
        public float OriginalUnitPriceInclVat { get; set; }
        public float OriginalUnitVat { get; set; }
        public float OriginalLineTotalInclVat { get; set; }
        public float OriginalLineVat { get; set; }
        public float OriginalFeeFixed { get; set; }
        public object BundleProductMerchantProductNo { get; set; }
        public object JurisCode { get; set; }
        public object JurisName { get; set; }
        public float VatRate { get; set; }
        public float UnitPriceExclVat { get; set; }
        public float LineTotalExclVat { get; set; }
        public float OriginalUnitPriceExclVat { get; set; }
        public float OriginalLineTotalExclVat { get; set; }
        public object[] ExtraData { get; set; }
        public string ChannelProductNo { get; set; }
        public string MerchantProductNo { get; set; }
        public int Quantity { get; set; }
        public int CancellationRequestedQuantity { get; set; }
        public float UnitPriceInclVat { get; set; }
        public float FeeFixed { get; set; }
        public float FeeRate { get; set; }
        public string Condition { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
    }
}
