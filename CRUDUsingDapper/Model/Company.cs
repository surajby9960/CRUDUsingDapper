namespace CRUDUsingDapper.Model
{
    public class Company
    {
        public int CId { get; set; }
        public string? CName { get; set; }
        public string? CAddress { get; set; }

        public List<Employee>? emplist { get; set; } 

    }
    public class InsertCompany
    {
        public int CId { get; set; }
        public string? CName { get; set; }
        public string? CAddress { get; set; }
    }
    public class UpdateCompany
    {
        public string? CName { get; set; }
        public string? CAddress { get; set; }
    }
}
