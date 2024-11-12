namespace EmployeePortal.Models
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }
        public string Name { get; set; }
        public string Team { get; set; }
        public string Project { get; set; }

        public string City {  get; set; }
        public string Phone { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
