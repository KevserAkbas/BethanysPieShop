using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; } //sayaç
        public Pie Pie { get; set; } //alışveriş sepetine bulunan Pie
        public int Amount { get; set; } //sepete eklenen Pie Miktarı
        public string ShoppingCartId { get; set; } //Önemlidir
    }
}
