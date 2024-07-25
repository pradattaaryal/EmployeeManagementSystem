namespace EmployeeManagementSystem.Models;

public class Menu
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}