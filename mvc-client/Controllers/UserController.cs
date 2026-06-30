using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvc_client.Models;

public class UserController : Controller
{
    private readonly UserRepository _userRepository;
    private readonly ClientRepository _clientRepository;

    public UserController(UserRepository userRepository, ClientRepository clientRepository)
    {
        _userRepository = userRepository;
        _clientRepository = clientRepository;
    }

    public IActionResult Index()
    {
        var users = _userRepository.GetAll();

        var viewModels = users.Select(user => new UserViewModel
        {
            User = user,
            ClientFullName = $"{user.Client.FirstName} {user.Client.LastName}"
        }).ToList();

        return View(viewModels);
    }

    public IActionResult Details(int id)
    {
        var user = _userRepository.Get(id);

        if (user == null)
            return NotFound();

        var viewModel = new UserViewModel
        {
            User = user,
            ClientFullName = $"{user.Client.FirstName} {user.Client.LastName}"
        };

        return View(viewModel);
    }

    public IActionResult Create()
    {
        PopulateClientsDropdown();
        PopulateRolesDropdown();
        return View();
    }

    [HttpPost]
    public IActionResult Create(User user, int clientId)
    {
        ModelState.Remove("Client");

        if (!ModelState.IsValid)
        {
            PopulateClientsDropdown(clientId);
            PopulateRolesDropdown();
            return View(user);
        }

        var client = _clientRepository.Get(clientId);

        if (client == null)
        {
            ModelState.AddModelError("", "Selected client not found.");
            PopulateClientsDropdown();
            PopulateRolesDropdown();
            return View(user);
        }

        user.Client = client;

        _userRepository.Save(user);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var user = _userRepository.Get(id);

        if (user == null)
            return NotFound();

        PopulateClientsDropdown(user.Client.Id);
        PopulateRolesDropdown(user.Role);
        return View(user);
    }

    [HttpPost]
    public IActionResult Edit(User user, int clientId)
    {
        if (!ModelState.IsValid)
        {
            PopulateClientsDropdown(clientId);
            PopulateRolesDropdown(user.Role);
            return View(user);
        }

        var client = _clientRepository.Get(clientId);

        if (client == null)
        {
            ModelState.AddModelError("", "Selected client not found.");
            PopulateClientsDropdown(clientId);
            PopulateRolesDropdown(user.Role);
            return View(user);
        }

        user.Client = client;

        _userRepository.Update(user);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var user = _userRepository.Get(id);

        if (user == null)
            return NotFound();

        var viewModel = new UserViewModel
        {
            User = user,
            ClientFullName = $"{user.Client.FirstName} {user.Client.LastName}"
        };

        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _userRepository.Delete(id);

        return RedirectToAction(nameof(Index));
    }

    private void PopulateClientsDropdown(int? selectedClientId = null)
    {
        var clients = _clientRepository.GetAll()
            .Select(c => new
            {
                c.Id,
                FullName = $"{c.FirstName} {c.LastName}"
            })
            .ToList();

        ViewBag.Clients = new SelectList(
            clients,
            "Id",
            "FullName",
            selectedClientId
        );
    }

    private void PopulateRolesDropdown(UserRole? selectedRole = null)
    {
        var roles = Enum.GetValues<UserRole>()
            .Select(r => new { Id = (int)r, Name = r.ToString() });

        ViewBag.Roles = new SelectList(
            roles,
            "Id",
            "Name",
            selectedRole.HasValue ? (int)selectedRole.Value : (int?)null
        );
    }
}