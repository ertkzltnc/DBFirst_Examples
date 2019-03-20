namespace DBFirst_FaturaIslemleri
{
    internal class MyEntity
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public decimal total { get; set; }
    }
}