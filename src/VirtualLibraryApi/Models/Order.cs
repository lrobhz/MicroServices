using System;
using Newtonsoft.Json;
using src.Models;
using VirtualLibraryApi.Converters;
using VirtualLibraryApi.Models.Enums;

namespace VirtualLibraryApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime OrderDate { get; set; }
        public User User { get; set; }
        public Basket Basket { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}