using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryLogic.EventBus
{

    public enum ScannerResult
    {
        UnknownSku,
        AddedToStock,
        Scanned,
        UnknownError
    }

    public class ScannerMessage
    {
        public ScannerMessage(string barcode, ScannerResult scannerResult)
        {
            this.ScannerResult = scannerResult;
            this.Barcode = barcode;

        }

        public string Barcode { get; set; }
        public ScannerResult ScannerResult { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProductStock { get; set; }
    }
}
