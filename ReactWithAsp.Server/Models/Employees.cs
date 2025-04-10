namespace ReactWithAsp.Server.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Designation {  get; set; }

        public string Address {  get; set; }
        public decimal Salary { get; set; }
    
        public DateOnly Birthdate { get; set; }
        public DateOnly JoiningDate { get; set; }
    }
}
