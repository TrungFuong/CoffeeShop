namespace CoffeeShop.Models.Enums
{
    public enum EnumPostition
    {
        PhaChe = 0,
        ThuNgan = 1,
        PhucVu = 2,
    }

    public static class EnumPostitionExtensions
    {
        public static string ToStringValue(this EnumPostition position)
        {
            switch (position)
            {
                case EnumPostition.PhaChe:
                    return "Pha che";
                case EnumPostition.ThuNgan:
                    return "Thu ngan";
                case EnumPostition.PhucVu:
                    return "Phuc vu";
                default:
                    throw new ArgumentOutOfRangeException(nameof(position), position, "Invalid position value.");
            }
        }
    }
}
