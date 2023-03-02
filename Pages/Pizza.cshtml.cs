using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesPizza.Models;
using RazorPagesPizza.Services;

namespace RazorPagesPizza.Pages;

public class PizzaModel : PageModel
{
  private readonly IPizzaService _pizzaService;

  public List<Pizza> pizzas = new();
  [BindProperty]
  public Pizza NewPizza { get; set; } = new();

  public PizzaModel(IPizzaService pizzaService)
  {
    _pizzaService = pizzaService;
  }

  public void OnGet()
  {
    pizzas = _pizzaService.GetAll();
  }

  public IActionResult OnPost()
  {
    if (!ModelState.IsValid)
    {
      return Page();
    }
    _pizzaService.Add(NewPizza);
    return RedirectToAction("Get");
  }

  public IActionResult OnPostDelete(int id)
  {
    _pizzaService.Delete(id);
    return RedirectToAction("Get");
  }

  public string GlutenFreeText(Pizza pizza) => pizza.IsGlutenFree ? "Gluten Free" : "Not Gluten Free";
}

