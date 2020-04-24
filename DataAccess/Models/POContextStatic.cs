namespace DataAccess.Models
{
    public partial class POContext
    {
        readonly string connString;

        public POContext(string connectionString) 
            : base()
        {
            connString = connectionString;
        }
    }
}
