namespace EmployeeManagementSystem.Models;
public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Menu> Menus { get; set; }
}