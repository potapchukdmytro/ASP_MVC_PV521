using _01_intro.Extensions;
using _01_intro.ViewModels;

namespace _01_intro.Services
{
    public static class CartService
    {
        private static readonly string key = "dcFaYORaMDOAfX5d1E5bGPaze8JMIHQD";

        public static void AddToCart(ISession session, int productId)
        {
            if (!IsInCart(session, productId))
            {
                var items = session.Get<List<CartItemVM>>(key) ?? [];
                var newItem = new CartItemVM
                {
                    ProductId = productId,
                    Count = 1
                };
                items.Add(newItem);
                session.Set(key, items);
            }
        }

        public static void RemoveFromCart(ISession session, int productId)
        {
            var items = session.Get<List<CartItemVM>>(key) ?? [];

            if(items != null)
            {
                items = items.Where(i => i.ProductId != productId).ToList();
                session.Set(key, items);
            }
        }

        public static bool IsInCart(ISession session, int productId)
        {
            var items = session.Get<List<CartItemVM>>(key) ?? [];
            return items.Any(i => i.ProductId == productId);
        }

        public static int Count(ISession session)
        {
            var items = session.Get<List<CartItemVM>>(key) ?? [];
            return items.Sum(i => i.Count);
        }
    }
}
