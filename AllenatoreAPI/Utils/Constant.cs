namespace AllenatoreAPI.Utils
{
    public static class Constant
    {
        public static readonly int FEET_LEFT = 1;

        public static readonly int FEET_RIGHT = 2;

        public static readonly int CARD_YELLOW = 1;

        public static readonly int CARD_RED = 2;

        public static readonly int TRANSFER_SESSION_SUMMER = 1;

        public static readonly int TRANSFER_SESSION_WINTER = 2;

        public static string FeetToString(int id)
        {
            switch (id)
            {
                case 1:
                    return "Sinistro";
                case 2:
                    return "Destro";
                default:
                    return string.Empty;
            }
        }

        public static string RoleToString(int id)
        {
            switch (id)
            {
                case 1:
                    return "Portiere";
                case 2:
                    return "Terzino Sinistro";
                case 7:
                    return "Esterno Destro";
                default:
                    return string.Empty;
            }            
        }
    }
}
